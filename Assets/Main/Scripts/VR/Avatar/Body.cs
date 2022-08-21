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
        [SerializeField] private Transform spines;

        [SerializeField] private HandWaypoint handWaypoint;
        [SerializeField] private Transform[] leftHandWaypoints;
        [SerializeField] private Transform[] rightHandWaypoints;

        private const float NeckVerticalShift = 0.12f;
        // private const float HipVerticalShift = 0.13f;
        private const float HipVerticalShift = 0.3f;
        private const float HideBodyDistance = 0.36f;


        private void Start()
        {
            var leftHandWaypoint = Instantiate(handWaypoint);
            leftHandWaypoint.SetHandPoints(leftHandWaypoints);

            var rightHandWaypoint = Instantiate(handWaypoint);
            rightHandWaypoint.SetHandPoints(rightHandWaypoints);
        }

        private void Update()
        {
            neck.position = head.position + Vector3.down * NeckVerticalShift;
            neck.localRotation = Quaternion.Euler(0, head.localRotation.eulerAngles.y, 0);

            //hip.position = bottom.position + Vector3.up * HipVerticalShift;

            hip.position = new Vector3(head.position.x, bottom.position.y + HipVerticalShift, head.position.z);
            hip.localRotation = neck.localRotation;

            spines.transform.LookAt(hip.position);
            var bodyDistance = Vector3.Distance(spines.position + Vector3.down * 0.025f, hip.position);
            spines.transform.localScale = new Vector3(2, 2, bodyDistance / 0.2f);

            transform.localScale = Vector3.Distance(head.position, bottom.position) < HideBodyDistance
                ? Vector3.zero
                : Vector3.one;
        }
    }
}