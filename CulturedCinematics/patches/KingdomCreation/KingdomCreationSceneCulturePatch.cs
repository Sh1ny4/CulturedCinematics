using HarmonyLib;
using TaleWorlds.CampaignSystem;
using TaleWorlds.CampaignSystem.SceneInformationPopupTypes;

namespace FullScreenCinematics.Patches.KingdomCreation
{
    [HarmonyPatch(typeof(KingdomCreatedSceneNotificationItem), nameof(KingdomCreatedSceneNotificationItem.SceneID), MethodType.Getter)]
    internal class KingdomCreationSceneCulturePatch : KingdomCreatedSceneNotificationItem
    {
        public KingdomCreationSceneCulturePatch(Kingdom newKingdom) : base(newKingdom)
        {
        }

        [HarmonyPostfix]
        static void Postfix(ref KingdomCreatedSceneNotificationItem __instance, ref string __result)
        {
            string text = string.Concat(new object[] { "scn_kingdom_made", "_", __instance.NewKingdom.Culture.StringId });
            var trySceneExist = new FallbackForSceneMissing();
            text = trySceneExist.TryGetSceneExist(text) ? text : "scn_kingdom_made";
            __result = text;
        }
    }
}
