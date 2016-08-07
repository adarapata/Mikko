using UnityEngine;
using System.Collections;

namespace GuP {
    public class MikkoConfig : ScriptableObject {
        [Tooltip("タップを受け付ける時間")]
        public float tapInterval = 0.4F;
    }
}