using System;
using System.Collections.Generic;
using Main.Scripts.ApplicationCore.Views;
using Normal.Realtime;
using UnityEngine;

namespace Main.Scripts.ApplicationCore.Controllers
{
    public class RealtimeMultiplayerController : BaseController
    {
        [SerializeField] private RealtimeMultiplayerView realtimeMultiplayerView;

        private RealtimeMultiplayerView _realtimeMultiplayerView;
        private Transform _vrPlayerRoot;
        private Transform _vrAvatarRoot;
        private Transform _vrPlayerBottomRoot;
        private Transform _vrAvatarBottomRoot;

        private List<Transform> _handChildrenRight = new List<Transform>();
        private List<Transform> _handChildrenAvatarRight = new List<Transform>();
        private List<Transform> _handChildrenLeft = new List<Transform>();
        private List<Transform> _handChildrenAvatarLeft = new List<Transform>();

        public Action Ready;

        public void Init(string roomName, Transform vrPlayerRoot,
            (Transform leftHandRoot, Transform rightHandRoot) handRoots, Transform vrPayerBottomRoot)
        {
            _vrPlayerRoot = vrPlayerRoot;
            _vrPlayerBottomRoot = vrPayerBottomRoot;

            if (_realtimeMultiplayerView == null)
            {
                _realtimeMultiplayerView = Instantiate(realtimeMultiplayerView);
                DontDestroyOnLoad(_realtimeMultiplayerView);

                if (handRoots.leftHandRoot != null)
                    _handChildrenLeft = GetAllChildren(handRoots.leftHandRoot, _handChildrenLeft);

                if (handRoots.rightHandRoot != null)
                    _handChildrenRight = GetAllChildren(handRoots.rightHandRoot, _handChildrenRight);
            }

            _realtimeMultiplayerView.ConnectToRoom(roomName);
        }

        public void Reset()
        {
            _vrPlayerRoot = null;
            _vrAvatarRoot = null;
            _vrPlayerBottomRoot = null;
            _vrAvatarBottomRoot = null;

            _handChildrenRight = new List<Transform>();
            _handChildrenAvatarRight = new List<Transform>();
            _handChildrenLeft = new List<Transform>();
            _handChildrenAvatarLeft = new List<Transform>();
        }

        public void Disconnect()
        {
            if (_realtimeMultiplayerView != null)
            {
                _realtimeMultiplayerView.DisconnectFromRoom();
            }
        }

        public void SetAvatarHands(Transform vrAvatarRoot,
            (Transform leftHandRoot, Transform rightHandRoot) handRootsAvatar, Transform vrAvatarBottomRoot)
        {
            _vrAvatarRoot = vrAvatarRoot;
            _vrAvatarBottomRoot = vrAvatarBottomRoot;
            var (leftHandRoot, rightHandRoot) = handRootsAvatar;
            _handChildrenAvatarLeft = GetAllChildren(leftHandRoot, _handChildrenAvatarLeft);
            _handChildrenAvatarRight = GetAllChildren(rightHandRoot, _handChildrenAvatarRight);

            Ready?.Invoke();
        }

        public RealtimeMultiplayerView GetRealtimeMultiplayerView()
        {
            return _realtimeMultiplayerView;
        }

        private static List<Transform> GetAllChildren(Transform parent, List<Transform> children)
        {
            if (parent.childCount <= 0) return children;

            foreach (Transform child in parent)
            {
                var realtimeViewComponent = child.GetComponent<RealtimeView>();

                if (realtimeViewComponent != null)
                    realtimeViewComponent.RequestOwnership();

                var realtimeTransformComponent = child.GetComponent<RealtimeTransform>();

                if (realtimeTransformComponent != null)
                    realtimeTransformComponent.RequestOwnership();

                children.Add(child);
                children = GetAllChildren(child, children);
            }

            return children;
        }

        private void Update()
        {
            if (_vrPlayerRoot != null && _vrAvatarRoot != null && _vrPlayerBottomRoot != null &&
                _vrAvatarBottomRoot != null)
            {
                _vrAvatarRoot.position = _vrPlayerRoot.position;
                _vrAvatarRoot.rotation = _vrPlayerRoot.rotation;
                _vrAvatarRoot.localScale = _vrPlayerRoot.localScale;
                _vrAvatarBottomRoot.position = _vrPlayerBottomRoot.position;
                _vrAvatarBottomRoot.rotation = _vrPlayerBottomRoot.rotation;
                _vrAvatarBottomRoot.localScale = _vrPlayerBottomRoot.localScale;
            }

            _handChildrenLeft.RemoveAll(item => item == null);
            _handChildrenAvatarLeft.RemoveAll(item => item == null);
            _handChildrenRight.RemoveAll(item => item == null);
            _handChildrenAvatarRight.RemoveAll(item => item == null);


            foreach (var handChild in _handChildrenLeft)
            {
                foreach (var handChildAvatar in _handChildrenAvatarLeft)
                {
                    if (handChild.name == handChildAvatar.name)
                    {
                        handChildAvatar.position = handChild.position;
                        handChildAvatar.rotation = handChild.rotation;
                    }
                }
            }

            foreach (var handChild in _handChildrenRight)
            {
                foreach (var handChildAvatar in _handChildrenAvatarRight)
                {
                    if (handChild.name == handChildAvatar.name)
                    {
                        handChildAvatar.position = handChild.position;
                        handChildAvatar.rotation = handChild.rotation;
                    }
                }
            }
        }
    }
}