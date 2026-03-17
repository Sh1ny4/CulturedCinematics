using HarmonyLib;
using System.Collections.Generic;
using System.Linq;
using TaleWorlds.CampaignSystem;
using TaleWorlds.CampaignSystem.SceneInformationPopupTypes;
using TaleWorlds.Core;
using TaleWorlds.LinQuick;

namespace FullScreenCinematics.Patches.KingdomJoin
{
    [HarmonyPatch(typeof(JoinKingdomSceneNotificationItem), nameof(JoinKingdomSceneNotificationItem.SceneID), MethodType.Getter)]
    internal class KingdomJoinSceneCulturePatch
    {
        [HarmonyPostfix]
        static void Postfix(ref JoinKingdomSceneNotificationItem __instance, ref string __result)
        {

            string text = string.Concat(new object[] { "scn_cutscene_factionjoin", "_", __instance.KingdomToUse.Culture.StringId });
            var trySceneExist = new FallbackForSceneMissing();
            text = trySceneExist.TryGetSceneExist(text) ? text : "scn_cutscene_factionjoin";
            __result = text;
        }
    }
    [HarmonyPatch(typeof(JoinKingdomSceneNotificationItem), nameof(JoinKingdomSceneNotificationItem.GetSceneNotificationCharacters))]
    internal class KingdomJoinSceneNPCAmountPatch
    {

        [HarmonyPostfix]
        static void Postfix(ref JoinKingdomSceneNotificationItem __instance, ref SceneNotificationData.SceneNotificationCharacter[] __result)
        {
            List<SceneNotificationData.SceneNotificationCharacter> list = new List<SceneNotificationData.SceneNotificationCharacter>();

            Hero leader = __instance.NewMemberClan.Leader;
            Equipment overridenEquipment = leader.BattleEquipment.Clone(false);
            CampaignSceneNotificationHelper.RemoveWeaponsFromEquipment(ref overridenEquipment, true, false);
            list.Add(CampaignSceneNotificationHelper.CreateNotificationCharacterFromHero(leader, overridenEquipment, false, default(BodyProperties), uint.MaxValue, uint.MaxValue, false));

            Hero king = __instance.KingdomToUse.Leader;
            Equipment overridenEquipment2 = king.BattleEquipment.Clone(false);
            CampaignSceneNotificationHelper.RemoveWeaponsFromEquipment(ref overridenEquipment2, true, false);
            list.Add(CampaignSceneNotificationHelper.CreateNotificationCharacterFromHero(king, overridenEquipment2, false, default(BodyProperties), uint.MaxValue, uint.MaxValue, false));

            foreach (Hero hero in GetAudienceForKingdomJoin(__instance.KingdomToUse, __instance.NewMemberClan).Take(10))
            {
                Equipment overridenEquipment3 = hero.CivilianEquipment.Clone(false);
                CampaignSceneNotificationHelper.RemoveWeaponsFromEquipment(ref overridenEquipment3, true, false);
                list.Add(CampaignSceneNotificationHelper.CreateNotificationCharacterFromHero(hero, overridenEquipment3, false, default(BodyProperties), uint.MaxValue, uint.MaxValue, false));
            }
            for (int i = 0; i < 6; i++)
            {
                list.Add(CampaignSceneNotificationHelper.GetBodyguardOfCulture(__instance.KingdomToUse.Culture));
            }
            __result = list.ToArray();
        }

        public static IEnumerable<Hero> GetAudienceForKingdomJoin(Kingdom kingdom, Clan NewMemberClan)
        {
            IOrderedEnumerable<Hero> orderedEnumerable = (from h in kingdom.Heroes.WhereQ((Hero h) => h != h.Clan.Kingdom.Leader)
                                                          orderby h.GetRelationWithPlayer()
                                                          select h);
            foreach (Hero item in orderedEnumerable)
            {
                if (!item.IsChild && item != Hero.MainHero && item.IsAlive && !item.IsFactionLeader)
                {
                    yield return item;
                }
            }
        }
    }
}
