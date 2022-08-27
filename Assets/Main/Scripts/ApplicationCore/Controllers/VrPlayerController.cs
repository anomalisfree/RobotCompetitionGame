using System;
using Main.Scripts.ApplicationCore.Data;
using Main.Scripts.ApplicationCore.Views;
using UnityEngine;

namespace Main.Scripts.ApplicationCore.Controllers
{
    public class VrPlayerController : BaseController
    {
        [SerializeField] private VrPlayerView vrPlayerView;
        [SerializeField] private Material[] mainMaterials;

        public Action<Transform, (Transform leftHandRoot, Transform rightHandRoot), Transform> Ready;
        public PlayerData PlayerData = new PlayerData();

        private VrPlayerView _vrPlayerView;

        public void Init()
        {
            _vrPlayerView = Instantiate(vrPlayerView);
                Ready?.Invoke(_vrPlayerView.GetBodyRoot(), _vrPlayerView.GetHandRoots(), _vrPlayerView.GetBottomRoot());
        }

        public void ResetPosition()
        {
            if (_vrPlayerView != null)
            {
                _vrPlayerView.ResetPose();
            }
        }

        public void SetPlayerName(string playerName)
        {
            PlayerData.Name = playerName;
        }

        public void SetPlayerColor(int colorNum)
        {
            PlayerData.ColorNum = colorNum;
            _vrPlayerView.SetMainMaterial(mainMaterials[colorNum]);
        }
    }
}