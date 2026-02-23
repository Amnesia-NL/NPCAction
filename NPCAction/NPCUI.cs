using AmnesiaTools.Core.UI.UIHelper;
using AmnesiaTools.Core.Utils;
using AmnesiaTools.Core.Utils.Entity;
using AmnesiaTools.Core.Utils.Entity.Builder;
using AmnesiaTools.Core.Utils.Json;
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
            PanelUI panel = new PanelUI("PNJ", UIPanel.PanelType.TabPrice, player, async () => await NPCPanel(player));
            List<NPC> NearNPC = new List<NPC>();
            foreach (Collider collider in Physics.OverlapSphere(player.setup.transform.position, 2f))
            {
                NpcComponent component = collider.gameObject.GetComponent<NpcComponent>();
                if (component != null)
                {
                    if (NPCBuilder.npcs.Count == 0)
                    {
                        break;
                    }
                    NPC npc = null;
                    foreach (NPC instance in NPCBuilder.npcs)
                    {
                        if ((instance.Position - collider.gameObject.transform.position).sqrMagnitude < 4f)
                        {
                            if (NearNPC.Contains(instance))
                            {
                                continue;
                            }
                            NearNPC.Add(instance);
                            npc = instance;
                            break;
                        }
                    }
                    if (npc == null) continue;
                    panel.AddTabLine(Text.ApplyAlign(Text.Gradient.ApplyGradient(npc.Name, Text.Colors.Red, Text.Colors.Cyan), Text.Align.Center), "", npc.IconId > 0 ? PlayerUtils.Items.GetItemIconById(npc.IconId) : PlayerUtils.Items.GetItemIconById(1127), ui => { panel.PanelClose(); npc.Action?.Invoke(player); });
                }
            }
            if(NearNPC.Count == 0)
            {
                panel.TabLineNameColor("Aucun PNJ à proximité", () => player.Notify(Text.Orange("PNJ"),"Aucun pnj à proximité"), Text.Colors.Red);

            }
            panel.CloseButtonColor();
            panel.SelectButtonNameColor("Parler");
            panel.ShowPanel();
        }
    }
}
