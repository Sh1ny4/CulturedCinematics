using HarmonyLib;
using TaleWorlds.CampaignSystem.SceneInformationPopupTypes;

namespace FullScreenCinematics.Patches.PlayerDeath
{
    [HarmonyPatch(typeof(DeathOldAgeSceneNotificationItem), nameof(DeathOldAgeSceneNotificationItem.SceneID), MethodType.Getter)]
    internal class DeathOldAgeSceneCulturePatch
    {
        [HarmonyPostfix]
        static void Postfix(ref DeathOldAgeSceneNotificationItem __instance, ref string __result)
        {
            string text = string.Concat(new object[] { "scn_cutscene_death_old_age", "_", __instance.DeadHero.Culture.StringId });
            var trySceneExist = new FallbackForSceneMissing();
            text = trySceneExist.TryGetSceneExist(text) ? text : "scn_cutscene_death_old_age";
            __result = text;
        }
    }
}
