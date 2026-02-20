using AmnesiaTools;
using AmnesiaTools.Core;
using Life;
using Life.Network;
using System.Threading.Tasks;
using UnityEngine;

namespace NPCAction
{
    public class NPCAction : AmnesiaSystem
    {
        PluginInformations pluginInformation;
        public NPCAction(IGameAPI aPI) : base(aPI)
        {
            pluginInformation = new PluginInformations("NPCAction", "V.1.0.0", "Zerox");
        }
        public override async void OnPlayerInput(Player player, KeyCode keyCode, bool onUI)
        {
            base.OnPlayerInput(player, keyCode, onUI);
            if (keyCode == KeyCode.F8 && !onUI) await NPCUI.NPCPanel(player); 
        }
    }
}
