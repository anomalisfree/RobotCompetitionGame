using UnityEngine;

namespace Main.Scripts.VR.Avatar
{
    public class Body : MonoBehaviour
    {
        [SerializeField] private Transform root;
        [SerializeField] private Transform head;
        [SerializeField] private Transform neck;
        [SerializeField] private Transform hip;
        [SerializeField] private Transform bottom;

        private const float NeckVerticalShift = 0.092f;
        private const float HipVerticalShift = 0.13f;

        private void Update()
        {
            neck.position = head.position + Vector3.down * NeckVerticalShift;
            neck.localRotation = Quaternion.Euler(0, head.localRotation.eulerAngles.y, 0);

            hip.position = bottom.position + Vector3.up * HipVerticalShift;
            hip.localRotation = neck.localRotation;
        }
    }
}
