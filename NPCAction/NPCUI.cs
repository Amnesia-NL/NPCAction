using AmnesiaTools.Core.UI.UIHelper;
using AmnesiaTools.Core.Utils;
using AmnesiaTools.Core.Utils.Entity;
using CMF;
using Life.Network;
using Life.UI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace NPCAction
{
    public class NPCUI
    {
        public static async Task NPCPanel(Player player)
        {
            PanelUI panel = new PanelUI("PNJ", UIPanel.PanelType.TabPrice, player, () => NPCPanel(player));
            foreach(Collider collider in Physics.OverlapSphere(player.setup.transform.position,5f))
            {
                if(collider.gameObject.CompareTag("Amnesia_NPC"))
                {
                    List<NPC> npcs = await NPC.Query(x => Vector3.Distance(x.Position, collider.transform.position) < 0.1f);
                    if (!npcs.Any()) continue;
                    NPC npc = npcs.FirstOrDefault();
                    panel.AddTabLine(Text.Gradient.ApplyGrandient(npc.Name, Color.red, Color.cyan), ui => { panel.PanelClose(); npc.Action?.Invoke(); });
                }
            }
            panel.CloseButtonColor();
            panel.SelectButtonNameColor("Parler");
            panel.ShowPanel();
        }
    }
}
