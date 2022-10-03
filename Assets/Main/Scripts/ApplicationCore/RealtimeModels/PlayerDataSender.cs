using Main.Scripts.ApplicationCore.Data;
using Normal.Realtime;

namespace Main.Scripts.ApplicationCore.RealtimeModels
{
    public class PlayerDataSender : RealtimeComponent<PlayerDataModel>
    {
        public void SetPlayerData(PlayerData  playerData)
        {
            model.name = playerData.Name;
            model.colorNum = playerData.ColorNum;
        }

        public string GetPlayerName()
        {
            return model.name;
        }

        public int GetPlayerColorNum()
        {
            return model.colorNum;
        }
    }
}