using HarmonyLib;
using TaleWorlds.CampaignSystem.SceneInformationPopupTypes;

namespace FullScreenCinematics.Patches.BecomeKing
{
    [HarmonyPatch(typeof(BecomeKingSceneNotificationItem), nameof(BecomeKingSceneNotificationItem.SceneID), MethodType.Getter)]
    internal class BecomeKingSceneCulturePatch
    {
        [HarmonyPostfix]
        static void Postfix(ref BecomeKingSceneNotificationItem __instance, ref string __result)
        {
            string text = string.Concat(new object[] { "scn_become_king_notification", "_", __instance.NewLeaderHero.Clan.Kingdom.Culture.StringId });
            var trySceneExist = new FallbackForSceneMissing();
            text = trySceneExist.TryGetSceneExist(text) ? text : "scn_become_king_notification";
            __result = text;
        }
    }
}
