using UnityEngine;
using System.Collections;
using UniRx;
using UniRx.Triggers;
using System.Linq;

namespace GuP {
    public partial class Mikko : MonoBehaviour {
        public const string VERSION = "0.2.1";

        private static Mikko instance;
        public static Mikko input {
            get {
                if(instance == null)
                {
                    instance = FindObjectOfType<Mikko>();
                    if(instance == null) {
                        var config = Resources.Load("MikkoConfig") as MikkoConfig;
                        var obj = new GameObject("Mikko",typeof(Mikko));
                        DontDestroyOnLoad(obj);
                        instance = obj.GetComponent<Mikko>();
                        instance.config = config;
                    }
                }
                return instance;
            }        
        }
        #if UNITY_EDITOR || UNITY_STANDALONE
        private Vector2 deltaPosition;
        #endif

        private Subject<ITouch> onTapStream = new Subject<ITouch>();
        public IObservable<ITouch> onTapAsObservable { get { return onTapStream.AsObservable(); } }

        public MikkoConfig config;

        public ITouch touch {
            get {
                #if UNITY_EDITOR || UNITY_STANDALONE
                return new MouseTouch(deltaPosition);
                #elif UNITY_IOS || UNITY_ANDROID
                var t = Input.touchCount == 0 ? new Touch() : Input.GetTouch(0);
                return new SmartPhoneTouch(t);
                #endif
                return null;
            }
        }

        void Start()
        {
            #if UNITY_EDITOR || UNITY_STANDALONE
            var mousePositionAsObservable = this.UpdateAsObservable().Select(_ => Input.mousePosition);
            mousePositionAsObservable.Buffer(2,1)
                .Select(mousePosition => mousePosition.Last() - mousePosition.First())
                .Subscribe(pos => deltaPosition = pos);
            #endif

            this.UpdateAsObservable()
                .Where(_ => touch.info == TouchInfo.Began)
                .Subscribe(_ => {
                    TouchEndAdObservable()
                        .Where(t => t < config.tapInterval)
                        .Subscribe(t => {
                            onTapStream.OnNext(touch);
                        });              
                });

        }

        private IObservable<float> TouchEndAdObservable()
        {
            return this.UpdateAsObservable()
                .Select(_ => Time.deltaTime)
                .Scan((acc, current) => acc += current)
                .TakeWhile(_ => touch.info != TouchInfo.Ended)
                .TakeLast(1);
        }
            
    }
}