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
            foreach (Collider collider in Physics.OverlapSphere(player.setup.transform.position, 5f))
            {
                Console.WriteLine("Collider hit: " + collider.name);
                NpcComponent component = collider.gameObject.GetComponent<NpcComponent>();
                if (component != null)
                { 
                    Console.WriteLine("Found");
                    if (NPCBuilder.npcs.Count == 0)
                    {
                        break;
                    }
                    Console.WriteLine("Continue");
                    NPC npc = null;
                    Console.WriteLine("1");
                    foreach(NPC instance in NPCBuilder.npcs)
                    {
                        Console.WriteLine("2");
                        if((instance.Position - collider.gameObject.transform.position).sqrMagnitude < 4f)
                        {
                            if(NearNPC.Contains(instance))
                            {
                                continue;
                            }
                            NearNPC.Add(instance);
                            Console.WriteLine("Good");
                            npc = instance;
                            break;
                        }
                        Console.WriteLine("3");
                    }
                    Console.WriteLine("4");
                    if (npc == null) continue;
                    Console.WriteLine("SO good");
                    panel.AddTabLine(Text.ApplyAlign(Text.Gradient.ApplyGradient(npc.Name, Text.Colors.Red, Text.Colors.Cyan), Text.Align.Center), "", PlayerUtils.Items.GetItemIconById(1127), ui => { panel.PanelClose(); npc.Action?.Invoke(player); });
                }
            }
            panel.CloseButtonColor();
            panel.SelectButtonNameColor("Parler");
            panel.ShowPanel();
        }
    }
}
