using UnityEngine;
using System.Collections;

namespace GuP {
    public class MouseTouch : ITouch {
        public Vector2 position {
            get {
                return Input.mousePosition;
            }
        }

        public Vector2 deltaPosition {
            get;
            private set;
        }

        public TouchInfo info {
            get;
            private set;
        }

        public MouseTouch(Vector2 delta){
            deltaPosition = delta;
            info = GetInfo();
        }

        private TouchInfo GetInfo()
        {
            if(Input.GetMouseButtonDown(0))
                return TouchInfo.Began;
            else if(Input.GetMouseButtonUp(0))
                return TouchInfo.Ended;
            else if(Input.GetMouseButton(0))
                return deltaPosition.sqrMagnitude < 0.01F ? TouchInfo.Stationary : TouchInfo.Moved;
            else
                return TouchInfo.None;            
        }
    }
}