
using UnityEngine;

namespace LestaAcademyDemo.DesignPatterns.Behavioral.ChainOfResponsibility
{
    public struct ScreenInputEvent
    {
        public int fingerId;
        public bool isDragEvent;
        public bool longTap;
        public Vector2 position;
        public Vector2 positionDelta;
    }
}
