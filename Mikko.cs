using UnityEngine;
using System.Collections;
using UniRx;
using UniRx.Triggers;
using System.Linq;

namespace GuP {
    public class Mikko : MonoBehaviour {
        private static Mikko instance;
        public static Mikko input {
            get {
                if(instance == null)
                {
                    instance = FindObjectOfType<Mikko>();
                    if(instance == null) {
                        var obj = new GameObject("Mikko",typeof(Mikko));
                        DontDestroyOnLoad(obj);
                        instance = obj.GetComponent<Mikko>();
                    }
                }
                return instance;
            }        
        }
        #if UNITY_EDITOR || UNITY_STANDALONE
        private Vector2 deltaPosition;
        #endif

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
        }
            
    }
}