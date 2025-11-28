using HarmonyLib;
using TaleWorlds.CampaignSystem.SceneInformationPopupTypes;

namespace FullScreenCinematics.Patches.PlayerDeath
{
    [HarmonyPatch(typeof(NavalDeathSceneNotificationItem), nameof(NavalDeathSceneNotificationItem.SceneID), MethodType.Getter)]
    internal class NavalDeathSceneCulturePatch
    {
        [HarmonyPostfix]
        static void Postfix(ref NavalDeathSceneNotificationItem __instance, ref string __result)
        {
            string text = string.Concat(new object[] { "scn_cutscene_main_hero_naval_battle_death", "_", __instance.DeadHero.Culture.StringId });
            var trySceneExist = new FallbackForSceneMissing();
            text = trySceneExist.TryGetSceneExist(text) ? text : "scn_cutscene_main_hero_naval_battle_death";
            __result = text;
        }
    }
}
