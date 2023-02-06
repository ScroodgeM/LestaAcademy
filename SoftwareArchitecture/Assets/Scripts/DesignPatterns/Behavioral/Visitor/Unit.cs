//this empty line for UTF-8 BOM header
using UnityEngine;

namespace LestaAcademyDemo.DesignPatterns.Behavioral.Visitor
{
    public abstract class Unit : MonoBehaviour
    {
        public void SetSpeed(float speed)
        {
            // apply speed here
        }

        public void ApplyDamage(int damagePoints)
        {
            // apply damage here
        }

        public abstract void Accept(IVisitor visitor);
    }
}
