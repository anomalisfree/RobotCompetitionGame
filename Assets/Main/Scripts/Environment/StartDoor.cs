using Autohand;
using UnityEngine;

namespace Main.Scripts.Environment
{
    public class StartDoor : MonoBehaviour
    {
        [SerializeField] private Animator animator;
        private static readonly int IsOpen = Animator.StringToHash("isOpen");

        private void OnTriggerStay(Collider other)
        {
            if (other.GetComponent<AutoHandPlayer>() != null)
            {
                animator.SetBool(IsOpen, true);
            }
        }

        private void OnTriggerExit(Collider other)
        {
            if (other.GetComponent<AutoHandPlayer>() != null)
            {
                animator.SetBool(IsOpen, false);
            }
        }
    }
}
