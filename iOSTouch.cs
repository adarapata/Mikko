using UnityEngine;
using System.Collections;
using GuP;

namespace GuP {
    public class iOSTouch : ITouch {
        private Touch touch;

        public Vector2 position {
            get {
                return touch.position;
            }
        }

        public Vector2 deltaPosition {
            get {
                return touch.deltaPosition;
            }
        }

        public TouchInfo info {
            get;
            private set;
        }

        public iOSTouch(Touch t){
            touch = t;
            info = t.tapCount == 0 ? TouchInfo.None : touch.phase.ToTouchInfo();
        }
    }
}

public static class TouchExtensions {
    static public TouchInfo ToTouchInfo(this TouchPhase self)
    {
        switch(self) {
        case TouchPhase.Began: return TouchInfo.Began;
        case TouchPhase.Moved: return TouchInfo.Moved;
        case TouchPhase.Ended: return TouchInfo.Ended;
        case TouchPhase.Stationary: return TouchInfo.Stationary;
        default: return TouchInfo.None;
        }
    }
}