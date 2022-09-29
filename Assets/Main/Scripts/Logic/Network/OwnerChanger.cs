using Main.Scripts.ApplicationCore.RealtimeModels;
using Normal.Realtime;
using UnityEngine;

namespace Main.Scripts.Logic.Network
{
    public class OwnerChanger : MonoBehaviour
    {
        [SerializeField] private LayerMask playerLayerMask;

        public InteractableObjectSender interactableObjectSender;
        public RealtimeView realtimeView;
        public RealtimeTransform realtimeTransform;


        private void Update()
        {
            if (realtimeView.isOwnedLocallySelf)
            {
                interactableObjectSender.SetReleaseTimer(interactableObjectSender.GetTimerValue() + Time.deltaTime);
            }
        }

        private void OnCollisionEnter(Collision other)
        {
            if (!realtimeView.isOwnedLocallySelf)
            {
                var otherInteractableObjectSender = other.gameObject.GetComponent<InteractableObjectSender>();


                if (playerLayerMask == (playerLayerMask | (1 << other.gameObject.layer)))
                {
                    if (!interactableObjectSender.GetIsGrabbed())
                    {
                        realtimeView.RequestOwnership();
                        realtimeTransform.RequestOwnership();
                        interactableObjectSender.SetReleaseTimer(0);
                    }
                }
                else if (otherInteractableObjectSender != null)
                {
                    if (otherInteractableObjectSender.realtimeView.isOwnedLocallySelf)
                    {
                        if (otherInteractableObjectSender.GetTimerValue() <= interactableObjectSender.GetTimerValue())
                        {
                            if (!interactableObjectSender.GetIsGrabbed())
                            {
                                realtimeView.RequestOwnership();
                                realtimeTransform.RequestOwnership();
                                interactableObjectSender.SetReleaseTimer(0);
                            }
                        }
                    }
                }
            }
        }
    }
}