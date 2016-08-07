using UnityEngine;
using System.Collections;

namespace GuP
{
    public partial class Mikko : MonoBehaviour {
        public enum TouchInfo
        {
            Began,
            Canceled,
            Ended,
            Moved,
            Stationary,
            None
        }
    }
}