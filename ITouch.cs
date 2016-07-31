using System.Collections;
using UnityEngine;

namespace GuP
{
    public interface ITouch {
        Vector2 position { get; }
        Vector2 deltaPosition { get; }
        TouchInfo info { get; }
    }
}