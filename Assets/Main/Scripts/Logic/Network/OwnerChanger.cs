using System;
using System.Collections;
using System.Collections.Generic;
using Main.Scripts.ApplicationCore.RealtimeModels;
using Normal.Realtime;
using Oculus.Voice.Core.Utilities;
using UnityEngine;

namespace Main.Scripts.Logic.Network
{
    public class OwnerChanger : MonoBehaviour
    {
        [SerializeField] private LayerMask playerLayerMask;

        public InteractableObjectSender interactableObjectSender;
        public RealtimeView realtimeView;
        public RealtimeTransform realtimeTransform;

        [SerializeField] private List<OwnerChanger> connectedObjects;

        private float _timer;
        
        private IEnumerator Timer()
        {
            _timer = 0;

            while (true)
            {
                interactableObjectSender.AddTimeToTimer(_timer);
                _timer += 0.02f;
                yield return new WaitForSeconds(_timer);
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
                        RequestOwnership();
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
                                RequestOwnership();
                            }
                        }
                    }
                }
            }
        }

        public void RequestOwnership(bool secondWave = false)
        {
            realtimeView.RequestOwnership();
            realtimeTransform.RequestOwnership();
            StopAllCoroutines();
            StartCoroutine(Timer());

            if (secondWave) return;
            
            foreach (var connectedObject in connectedObjects)
            {
                connectedObject.RequestOwnership(true);
            }
        }

        public void SetIsGrabbed(bool isGrabbed)
        {
            interactableObjectSender.SetIsGrabbed(isGrabbed);
        }
    }
}