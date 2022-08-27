using Main.Scripts.ApplicationCore.Clients;
using Main.Scripts.ApplicationCore.Controllers;
using UnityEngine;

namespace Main.Scripts.Environment
{
    public class ColorPanel : MonoBehaviour
    {
        [SerializeField] private StartDoor startDoor;
        

        public void PressMaterialBtn(int materialNum)
        {
            ClientBase.Instance.GetController<VrPlayerController>().SetPlayerColor(materialNum);
            startDoor.ChangePanel();
        }
    }
}