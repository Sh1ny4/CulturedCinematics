using HarmonyLib;
using System.Collections.Generic;
using System.Linq;
using TaleWorlds.CampaignSystem;
using TaleWorlds.CampaignSystem.SceneInformationPopupTypes;
using TaleWorlds.Core;

namespace FullScreenCinematics.Patches.KingdomJoin
{
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
            foreach (Hero hero in CampaignSceneNotificationHelper.GetMilitaryAudienceForKingdom(__instance.KingdomToUse, true).Take(10))
            {
                Equipment overridenEquipment2 = hero.CivilianEquipment.Clone(false);
                CampaignSceneNotificationHelper.RemoveWeaponsFromEquipment(ref overridenEquipment2, true, false);
                list.Add(CampaignSceneNotificationHelper.CreateNotificationCharacterFromHero(hero, overridenEquipment2, false, default(BodyProperties), uint.MaxValue, uint.MaxValue, false));
            }
            for (int i = 0; i < 6; i++)
            {
                list.AddItem(CampaignSceneNotificationHelper.GetBodyguardOfCulture(__instance.KingdomToUse.Culture));
            }
            __result = list.ToArray();
        }
    }
}
