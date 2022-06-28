using System.Collections.Generic;
using Main.Scripts.ApplicationCore.Clients;
using Main.Scripts.ApplicationCore.Controllers;
using Normal.Realtime;
using UnityEngine;

namespace Main.Scripts.ApplicationCore.Views
{
    public class RealtimeAvatarView : MonoBehaviour
    {
        [SerializeField] private RealtimeView realtimeView;
        [SerializeField] private List<GameObject> localHideObjects;
        [SerializeField] private Transform leftHandRoot;
        [SerializeField] private Transform rightHandRoot;
        [SerializeField] private Transform bottomRoot;

        private RealtimeMultiplayerController _realtimeMultiplayerController;
        private bool _avatarLoaded;

        private void Start()
        {
            if (realtimeView.isOwnedLocallySelf)
            {
                _realtimeMultiplayerController = ClientBase.Instance.GetController<RealtimeMultiplayerController>();
                _realtimeMultiplayerController.SetAvatarHands(transform, (leftHandRoot, rightHandRoot), bottomRoot);
                
                foreach (var localHideObject in localHideObjects)
                {
                    localHideObject.SetActive(false);
                }
            }
        }
    }
}