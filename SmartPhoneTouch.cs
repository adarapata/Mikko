﻿using UnityEngine;
using System.Collections;
using GuP;

namespace GuP
{
    public partial class Mikko : MonoBehaviour
    {
        public class SmartPhoneTouch : ITouch
        {
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

            public SmartPhoneTouch (Touch t)
            {
                touch = t;
                info = t.tapCount == 0 ? TouchInfo.None : touch.phase.ToTouchInfo ();
            }
        }          
    }       
}

public static class TouchExtensions
{
    static public Mikko.TouchInfo ToTouchInfo (this TouchPhase self)
    {
        switch (self) {
        case TouchPhase.Began:
            return Mikko.TouchInfo.Began;
        case TouchPhase.Moved:
            return Mikko.TouchInfo.Moved;
        case TouchPhase.Ended:
            return Mikko.TouchInfo.Ended;
        case TouchPhase.Stationary:
            return Mikko.TouchInfo.Stationary;
        default:
            return Mikko.TouchInfo.None;
        }
    }
}
