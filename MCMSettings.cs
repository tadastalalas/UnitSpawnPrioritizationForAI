using System;
using MCM.Abstractions.Attributes;
using MCM.Abstractions.Attributes.v2;
using MCM.Abstractions.Base.Global;
using MCM.Common;
using TaleWorlds.Localization;

namespace UnitSpawnPrioritizationForAI
{
    public class MCMSettings : AttributeGlobalSettings<MCMSettings>
    {
        public override string Id => "UnitSpawnPrioritizationForAISettings";
        public override string DisplayName => new TextObject("Unit Spawn Prioritization For AI").ToString();
        public override string FolderName => "UnitSpawnPrioritizationForAI";
        public override string FormatType => "json2";

        [SettingPropertyBool("Enable This Modification", Order = 0, RequireRestart = false, HintText = "Enable this modification. [Default: true]")]
        [SettingPropertyGroup("Main Settings", GroupOrder = 0)]
        public bool EnableAISpawnPrioritization { get; set; } = true;

        [SettingPropertyDropdown("AI Spawn Prioritization Mode", Order = 1, RequireRestart = false, HintText = "Default: Based on party roster order | High Level: High tier troops spawn first | Low Level: Low tier troops spawn first | Homogeneous: Troops are spawned based on troop type instead of troop level to preserve the troop ratio on the field. [Game default: High Level]")]
        [SettingPropertyGroup("Main Settings", GroupOrder = 0)]
        public Dropdown<string> AIMode { get; set; } = new Dropdown<string>(
            new string[]
            {
                "Default",
                "High Level",
                "Low Level",
                "Homogeneous"
            },
        selectedIndex: 1);
    }
}