using UnityEngine;

namespace Main.Scripts.Logic
{
    public class CollisionTriggerDetector : MonoBehaviour
    {
        private void OnTriggerEnter(Collider other)
        {
            var trigger = other.gameObject.GetComponent<CollisionTrigger>();
            
            if (trigger != null)
            {
                trigger.onCollisionEnter.Invoke();
            }
        }
    }
}