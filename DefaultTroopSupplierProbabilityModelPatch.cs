using HarmonyLib;
using TaleWorlds.CampaignSystem.GameComponents;
using TaleWorlds.CampaignSystem.Party;
using TaleWorlds.CampaignSystem.Encounters;
using TaleWorlds.CampaignSystem.MapEvents;
using TaleWorlds.Core;
using TaleWorlds.MountAndBlade;
using System.Collections.Generic;
using System.Reflection;
using System.Reflection.Emit;
using TaleWorlds.Library;

namespace UnitSpawnPrioritizationForAI
{
    [HarmonyPatch(typeof(DefaultTroopSupplierProbabilityModel))]
    [HarmonyPatch("EnqueueTroopSpawnProbabilitiesAccordingToUnitSpawnPrioritization")]
    public class DefaultTroopSupplierProbabilityModelPatch
    {
        [HarmonyTranspiler]
        static IEnumerable<CodeInstruction> Transpiler(IEnumerable<CodeInstruction> instructions, ILGenerator il)
        {
            var codes = new List<CodeInstruction>(instructions);
            
            bool patched = false;
            for (int i = 0; i < codes.Count; i++)
            {
                if (!patched && codes[i].opcode == OpCodes.Ldc_I4_1 && i + 1 < codes.Count && codes[i + 1].opcode == OpCodes.Stloc_0)
                {
                    codes[i] = new CodeInstruction(OpCodes.Call,
                        AccessTools.Method(
                            typeof(DefaultTroopSupplierProbabilityModelPatch),
                            nameof(GetDefaultUnitSpawnPrioritization))
                        );
                    patched = true;
                }
            }
            return codes;
        }

        private static int GetDefaultUnitSpawnPrioritization()
        {
            try
            {
                var settings = MCMSettings.Instance;
                if (settings == null || !settings.EnableAISpawnPrioritization)
                    return 1;

                int aiPrioritization;

                switch (settings.AIMode.SelectedValue)
                {
                    case "Default":
                        aiPrioritization = 0;
                        break;
                    case "High Level":
                        aiPrioritization = 1;
                        break;
                    case "Low Level":
                        aiPrioritization = 2;
                        break;
                    case "Homogeneous":
                        aiPrioritization = 3;
                        break;
                    default:
                        aiPrioritization = 1;
                        break;
                }
                return aiPrioritization;
            }
            catch (System.Exception ex)
            {
                InformationManager.DisplayMessage(new InformationMessage($"[UnitSpawn] ERROR: {ex.Message}"));
                return 1;
            }
        }
    }
}