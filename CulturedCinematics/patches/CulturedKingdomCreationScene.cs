using HarmonyLib;
using System.Collections.Generic;
using System.Linq;
using TaleWorlds.CampaignSystem;
using TaleWorlds.CampaignSystem.SceneInformationPopupTypes;
using TaleWorlds.Core;

namespace FullScreenCinematics.Patches.KingdomCreation
{
    [HarmonyPatch(typeof(KingdomCreatedSceneNotificationItem), nameof(KingdomCreatedSceneNotificationItem.SceneID), MethodType.Getter)]
    internal class KingdomCreationSceneCulturePatch : KingdomCreatedSceneNotificationItem
    {
        public KingdomCreationSceneCulturePatch(Kingdom newKingdom) : base(newKingdom)
        {
        }

        [HarmonyPostfix]
        static void Postfix(ref KingdomCreatedSceneNotificationItem __instance, ref string __result)
        {
            string text = string.Concat(new object[] { "scn_kingdom_made", "_", __instance.NewKingdom.Culture.StringId });
            var trySceneExist = new FallbackForSceneMissing();
            text = trySceneExist.TryGetSceneExist(text) ? text : "scn_kingdom_made";
            __result = text;
        }
    }
    [HarmonyPatch(typeof(KingdomCreatedSceneNotificationItem), nameof(KingdomCreatedSceneNotificationItem.GetSceneNotificationCharacters))]
    internal class KingdomCreationSceneNPCAmountPatch
    {

        [HarmonyPostfix]
        static void Postfix(ref KingdomCreatedSceneNotificationItem __instance, ref SceneNotificationData.SceneNotificationCharacter[] __result)
        {
            Hero leader = __instance.NewKingdom.Leader;
            Equipment overridenEquipment = leader.BattleEquipment.Clone(false);
            CampaignSceneNotificationHelper.RemoveWeaponsFromEquipment(ref overridenEquipment, true, false);
            List<SceneNotificationData.SceneNotificationCharacter> list = new List<SceneNotificationData.SceneNotificationCharacter>();
            list.Add(CampaignSceneNotificationHelper.CreateNotificationCharacterFromHero(leader, overridenEquipment, false, default(BodyProperties), uint.MaxValue, uint.MaxValue, false));
            foreach (Hero hero in CampaignSceneNotificationHelper.GetMilitaryAudienceForKingdom(__instance.NewKingdom, false).Take(12))  //changed from take(5)
            {
                Equipment overridenEquipment2 = hero.CivilianEquipment.Clone(false);
                CampaignSceneNotificationHelper.RemoveWeaponsFromEquipment(ref overridenEquipment2, true, false);
                list.Add(CampaignSceneNotificationHelper.CreateNotificationCharacterFromHero(hero, overridenEquipment2, false, default(BodyProperties), uint.MaxValue, uint.MaxValue, false));
            }
            for (int i = 0; i < 10; i++)
            {
                BasicCharacterObject npc = CampaignSceneNotificationHelper.GetRandomTroopForCulture(__instance.NewKingdom.Culture);
                list.Add(new SceneNotificationData.SceneNotificationCharacter(npc));
            }
            __result = list.ToArray();
        }
    }
}
