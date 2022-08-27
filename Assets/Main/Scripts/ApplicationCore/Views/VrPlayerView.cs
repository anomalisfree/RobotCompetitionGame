using System.Collections.Generic;
using Autohand;
using UnityEngine;

namespace Main.Scripts.ApplicationCore.Views
{
    public class VrPlayerView : MonoBehaviour
    {
        [SerializeField] private Transform leftHand;
        [SerializeField] private Transform rightHand;
        [SerializeField] private Transform bodyRoot;
        [SerializeField] private AutoHandPlayer autoHandPlayer;
        [SerializeField] private List<SkinnedMeshRenderer> skinnedMeshRenderersWithMainColor;

        private void Awake()
        {
            DontDestroyOnLoad(gameObject);
        }

        public (Transform left, Transform right) GetHandRoots()
        {
            return (leftHand, rightHand);
        }

        public Transform GetBodyRoot()
        {
            return bodyRoot;
        }
        
        public Transform GetBottomRoot()
        {
            return autoHandPlayer.transform;
        }

        public void ResetPose()
        {
            autoHandPlayer.SetPosition(Vector3.zero, Quaternion.identity);
        }

        public void SetMainMaterial(Material material)
        {
            foreach (var skinnedMeshRenderer in skinnedMeshRenderersWithMainColor)
            {
                skinnedMeshRenderer.material = material;
            }
        }
    }
}
