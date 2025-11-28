using HarmonyLib;
using TaleWorlds.CampaignSystem.SceneInformationPopupTypes;

namespace FullScreenCinematics.Patches.PlayerDeath
{
    [HarmonyPatch(typeof(MainHeroBattleVictoryDeathNotificationItem), nameof(MainHeroBattleVictoryDeathNotificationItem.SceneID), MethodType.Getter)]
    internal class BattleDeathVictoryCulturePatch
    {
        [HarmonyPostfix]
        static void Postfix(ref MainHeroBattleVictoryDeathNotificationItem __instance, ref string __result)
        {
            string text = string.Concat(new object[] { "scn_cutscene_main_hero_battle_victory_death", "_", __instance.DeadHero.Culture.StringId });
            var trySceneExist = new FallbackForSceneMissing();
            text = trySceneExist.TryGetSceneExist(text) ? text : "scn_cutscene_main_hero_battle_victory_death";
            __result = text;
        }
    }
}
