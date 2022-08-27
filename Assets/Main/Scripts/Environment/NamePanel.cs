using Main.Scripts.ApplicationCore.Clients;
using Main.Scripts.ApplicationCore.Controllers;
using TMPro;
using UnityEngine;

namespace Main.Scripts.Environment
{
    public class NamePanel : MonoBehaviour
    {
        [SerializeField] private TextMeshPro textOnDisplay;
        [SerializeField] private int maxDisplayTextLength;
        [SerializeField] private StartDoor startDoor;

        public void PressLetterBtn(string letter)
        {
            if (textOnDisplay.text.Length < maxDisplayTextLength)
            {
                textOnDisplay.text += letter;
            }
        }

        public void PressBackspaceBtn()
        {
            if (textOnDisplay.text.Length > 0)
            {
                textOnDisplay.text = textOnDisplay.text[..^1];
            }
        }

        public void PressEnterBtn()
        {
            if (textOnDisplay.text.Length > 0)
            {
                ClientBase.Instance.GetController<VrPlayerController>().SetPlayerName(textOnDisplay.text);
                startDoor.ChangePanel();
            }
        }
    }
}