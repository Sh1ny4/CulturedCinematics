using HarmonyLib;
using Helpers;
using TaleWorlds.CampaignSystem;
using TaleWorlds.CampaignSystem.SceneInformationPopupTypes;

namespace FullScreenCinematics.Patches.Marriage
{
    [HarmonyPatch(typeof(MarriageSceneNotificationItem), nameof(MarriageSceneNotificationItem.SceneID), MethodType.Getter)]
    internal class MarriageSceneCulturePatch
    {
        [HarmonyPostfix]
        static void Postfix(ref MarriageSceneNotificationItem __instance, ref string __result)
        {
            string text = string.Concat(new object[] { "scn_cutscene_wedding", "_" , (SettlementHelper.FindNearestSettlementToPoint(__instance.GroomHero.GetCampaignPosition())).Culture.StringId });
            var trySceneExist = new FallbackForSceneMissing();
            text = trySceneExist.TryGetSceneExist(text) ? text : "scn_cutscene_wedding";
            __result = text;
        }
    }
}
