using System.Collections;
using System.Collections.Generic;
using Main.Scripts.ApplicationCore.Clients;
using Main.Scripts.ApplicationCore.Controllers;
using Main.Scripts.ApplicationCore.RealtimeModels;
using Normal.Realtime;
using TMPro;
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
        [SerializeField] private PlayerDataSender playerDataSender;
        [SerializeField] private List<MeshRenderer> meshRenderersWithMainColor;
        [SerializeField] private List<SkinnedMeshRenderer> skinnedMeshRenderersWithMainColor;
        [SerializeField] private TextMeshPro nameText;

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

                playerDataSender.SetPlayerData(ClientBase.Instance.GetController<VrPlayerController>().PlayerData);
            }

            StartCoroutine(WaitForPlayerData());
        }

        private IEnumerator WaitForPlayerData()
        {
            while (string.IsNullOrEmpty(playerDataSender.GetPlayerName()))
            {
                yield return null;
            }

            InitializeAvatar();
        }

        private void InitializeAvatar()
        {
            foreach (var meshRenderer in meshRenderersWithMainColor)
            {
                meshRenderer.material = ClientBase.Instance.GetController<VrPlayerController>()
                    .mainMaterials[playerDataSender.GetPlayerColorNum()];
            }
            
            foreach (var meshRenderer in skinnedMeshRenderersWithMainColor)
            {
                meshRenderer.material = ClientBase.Instance.GetController<VrPlayerController>()
                    .mainMaterials[playerDataSender.GetPlayerColorNum()];
            }

            nameText.text = playerDataSender.GetPlayerName();
        }
    }
}