using PathCreation;
using UnityEngine;

namespace Main.Scripts.VR.Avatar
{
    public class HandWaypoint : MonoBehaviour
    {
        [SerializeField] private PathCreator pathCreator;
        
        private Transform[] _leftHandWaypoints;
        private BezierPath _leftHandBezierPath;

        public void SetHandPoints(Transform[] leftHandWaypoints)
        {
            _leftHandWaypoints = leftHandWaypoints;
        }
        
        private void Update()
        {
            _leftHandBezierPath = new BezierPath (_leftHandWaypoints, false, PathSpace.xyz);
            pathCreator.bezierPath = _leftHandBezierPath;
        }
    }
}
