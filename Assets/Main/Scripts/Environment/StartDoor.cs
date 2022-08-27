using System.Collections;
using System.Collections.Generic;
using Autohand;
using UnityEngine;
using UnityEngine.Serialization;

namespace Main.Scripts.Environment
{
    public class StartDoor : MonoBehaviour
    {
        [SerializeField] private Animator doorAnimator;
        [SerializeField] private Animator panelAnimator;
        [SerializeField] private List<GameObject> panels;
        [SerializeField] private float changePanelDelay;

        private int _currentPanel;
        private bool _animationIsPlaying;

        private static readonly int IsOpen = Animator.StringToHash("isOpen");
        private static readonly int Open = Animator.StringToHash("open");

        private void OnTriggerStay(Collider other)
        {
            if (!_animationIsPlaying && other.GetComponent<AutoHandPlayer>() != null)
            {
                panelAnimator.SetBool(IsOpen, true);
            }
        }

        private void OnTriggerExit(Collider other)
        {
            if (!_animationIsPlaying && other.GetComponent<AutoHandPlayer>() != null)
            {
                panelAnimator.SetBool(IsOpen, false);
            }
        }

        public void ChangePanel()
        {
            StartCoroutine(ChangePanelCor());
        }

        private IEnumerator ChangePanelCor()
        {
            _animationIsPlaying = true;
            panelAnimator.SetBool(IsOpen, false);

            yield return new WaitForSeconds(1.5f);

            _currentPanel++;

            for (var i = 0; i < panels.Count; i++)
            {
                panels[i].SetActive(_currentPanel == i);
            }

            if (_currentPanel < panels.Count)
            {
                panelAnimator.SetBool(IsOpen, true);
                _animationIsPlaying = false;
            }
            else
            {
                doorAnimator.SetTrigger(Open);
            }
        }
    }
}