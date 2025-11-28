using HarmonyLib;
using TaleWorlds.CampaignSystem.SceneInformationPopupTypes;

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
}
