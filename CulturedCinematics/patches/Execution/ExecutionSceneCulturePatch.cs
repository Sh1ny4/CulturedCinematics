using HarmonyLib;
using Helpers;
using TaleWorlds.CampaignSystem.SceneInformationPopupTypes;

namespace FullScreenCinematics.Patches.Execution
{
    [HarmonyPatch(typeof(HeroExecutionSceneNotificationData), nameof(HeroExecutionSceneNotificationData.SceneID), MethodType.Getter)]
    internal class ExecutionSceneCulturePatch
    {
        [HarmonyPostfix]
        static void Postfix(ref HeroExecutionSceneNotificationData __instance, ref string __result)
        {
            string text = string.Concat(new object[] { "scn_execution_notification", "_", __instance.Executer.Culture.StringId });
            var trySceneExist = new FallbackForSceneMissing();
            text = trySceneExist.TryGetSceneExist(text) ? text : "scn_execution_notification";
            __result = text;
        }
    }
}
