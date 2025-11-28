using HarmonyLib;
using TaleWorlds.CampaignSystem.SceneInformationPopupTypes;

namespace FullScreenCinematics.Patches.PlayerDeath
{
    [HarmonyPatch(typeof(MainHeroBattleDeathNotificationItem), nameof(MainHeroBattleDeathNotificationItem.SceneID), MethodType.Getter)]
    internal class BattleDeathDefeatCulturePatch
    {
        [HarmonyPostfix]
        static void Postfix(ref MainHeroBattleDeathNotificationItem __instance, ref string __result)
        {
            string text = string.Concat(new object[] { "scn_cutscene_main_hero_battle_death", "_", __instance.DeadHero.Culture.StringId });
            var trySceneExist = new FallbackForSceneMissing();
            text = trySceneExist.TryGetSceneExist(text) ? text : "scn_cutscene_main_hero_battle_death";
            __result = text;
        }
    }
}
