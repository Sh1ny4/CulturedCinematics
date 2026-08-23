using HarmonyLib;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TaleWorlds.CampaignSystem;
using TaleWorlds.CampaignSystem.CharacterDevelopment;
using TaleWorlds.CampaignSystem.SceneInformationPopupTypes;
using TaleWorlds.Core;

namespace FullScreenCinematics.Patches.Execution
{
    [HarmonyPatch(typeof(HeroExecutionSceneNotificationData), nameof(HeroExecutionSceneNotificationData.SceneID), MethodType.Getter)]
    internal class ExecutionSceneCulturePatch
    {
        [HarmonyPostfix]
        static void Postfix(ref HeroExecutionSceneNotificationData __instance, ref string __result)
        {
            string text = string.Concat(new object[] { "scn_execution_notification", "_", __instance.Executer.Culture.StringId });
            var trySceneExist = new FallbackForSceneMissing();
            text = trySceneExist.TryGetSceneExist(text) ? text : "scn_execution_notification";
            __result = text;
        }
    }
    [HarmonyPatch(typeof(HeroExecutionSceneNotificationData), nameof(HeroExecutionSceneNotificationData.GetSceneNotificationCharacters))]
    internal class ExecutionSceneNPCAmountPatch
    {

        [HarmonyPostfix]
        static void Postfix(ref HeroExecutionSceneNotificationData __instance, ref SceneNotificationData.SceneNotificationCharacter[] __result)
        {
            Equipment equipment = __instance.Victim.BattleEquipment.Clone(true);
            equipment.AddEquipmentToSlotWithoutAgent(EquipmentIndex.NumAllWeaponSlots, default(EquipmentElement));
            equipment.AddEquipmentToSlotWithoutAgent(EquipmentIndex.WeaponItemBeginSlot, default(EquipmentElement));
            equipment.AddEquipmentToSlotWithoutAgent(EquipmentIndex.Weapon1, default(EquipmentElement));
            equipment.AddEquipmentToSlotWithoutAgent(EquipmentIndex.Weapon2, default(EquipmentElement));
            equipment.AddEquipmentToSlotWithoutAgent(EquipmentIndex.Weapon3, default(EquipmentElement));
            equipment.AddEquipmentToSlotWithoutAgent(EquipmentIndex.ExtraWeaponSlot, default(EquipmentElement));
            CampaignSceneNotificationHelper.RemoveWeaponsFromEquipment(ref equipment, true);
            Equipment equipment2 = __instance.Executer.BattleEquipment.Clone(true);
            equipment2.AddEquipmentToSlotWithoutAgent(EquipmentIndex.WeaponItemBeginSlot, default(EquipmentElement));
            equipment2.AddEquipmentToSlotWithoutAgent(EquipmentIndex.Weapon1, default(EquipmentElement));
            equipment2.AddEquipmentToSlotWithoutAgent(EquipmentIndex.Weapon2, default(EquipmentElement));
            equipment2.AddEquipmentToSlotWithoutAgent(EquipmentIndex.Weapon3, default(EquipmentElement));
            equipment2.AddEquipmentToSlotWithoutAgent(EquipmentIndex.ExtraWeaponSlot, default(EquipmentElement));
            List<SceneNotificationData.SceneNotificationCharacter> list = new List<SceneNotificationData.SceneNotificationCharacter>();
            list.Add(CampaignSceneNotificationHelper.CreateNotificationCharacterFromHero(__instance.Victim, equipment, false, default(BodyProperties), uint.MaxValue, uint.MaxValue, false));
            list.Add(CampaignSceneNotificationHelper.CreateNotificationCharacterFromHero(__instance.Executer, equipment2, false, default(BodyProperties), uint.MaxValue, uint.MaxValue, false));

            var companions = CampaignSceneNotificationHelper.GetMilitaryAudienceForHero(__instance.Executer).OrderBy(hero => hero.GetTraitLevel(DefaultTraits.Mercy)).Take(1);
            list.Add(CampaignSceneNotificationHelper.CreateNotificationCharacterFromHero(companions.ElementAt(0), null, false, default(BodyProperties), uint.MaxValue, uint.MaxValue, false));

            if (__instance.Victim.CharacterObject.StringId != "radagos_henchman")
            {
				if(__instance.Executer.Clan != null){
					for (int i = 0; i < 10; i++)
					{
						BasicCharacterObject npc = CampaignSceneNotificationHelper.GetRandomTroopForCulture(__instance.Executer.Clan.Culture);
						list.Add(new SceneNotificationData.SceneNotificationCharacter(npc));
					}
				} else {
					for (int i = 0; i < 10; i++)
					{
						BasicCharacterObject npc = CampaignSceneNotificationHelper.GetRandomTroopForCulture(__instance.Executer.Culture);
						list.Add(new SceneNotificationData.SceneNotificationCharacter(npc));
					}
				}
            }
            __result = list.ToArray();
        }
    }
}
