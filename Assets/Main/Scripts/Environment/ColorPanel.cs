using UnityEngine;

namespace Main.Scripts.Environment
{
    public class ColorPanel : MonoBehaviour
    {
        [SerializeField] private Material[] materials;
        [SerializeField] private StartDoor startDoor;
        

        public void PressMaterialBtn(int materialNum)
        {
            startDoor.ChangePanel();
        }
    }
}