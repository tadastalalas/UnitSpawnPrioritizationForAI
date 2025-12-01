using HarmonyLib;
using TaleWorlds.CampaignSystem.Party;
using TaleWorlds.CampaignSystem.Encounters;
using TaleWorlds.CampaignSystem.MapEvents;
using TaleWorlds.Core;
using TaleWorlds.MountAndBlade;


namespace UnitSpawnPrioritizationForAI
{
    public class SubModule : MBSubModuleBase
    {
        private const string ModuleId = "UnitSpawnPrioritizationForAI";
        private readonly Harmony _harmony = new Harmony(ModuleId);

        protected override void OnSubModuleLoad()
        {
            base.OnSubModuleLoad();
            _harmony.PatchAll();
        }

        protected override void OnSubModuleUnloaded() => base.OnSubModuleUnloaded();

        protected override void OnBeforeInitialModuleScreenSetAsRoot() => base.OnBeforeInitialModuleScreenSetAsRoot();
    }
}