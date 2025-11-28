using HarmonyLib;
using TaleWorlds.CampaignSystem.SceneInformationPopupTypes;

namespace FullScreenCinematics.Patches.ClanMemberDeath
{
    [HarmonyPatch(typeof(ClanMemberPeaceDeathSceneNotificationItem), nameof(ClanMemberPeaceDeathSceneNotificationItem.SceneID), MethodType.Getter)]
    internal class ClanMemberDeathPeaceCulturePatch
    {
        [HarmonyPostfix]
        static void Postfix(ref ClanMemberPeaceDeathSceneNotificationItem __instance, ref string __result)
        {
            string text = string.Concat(new object[] { "scn_cutscene_family_member_death", "_", __instance.DeadHero.Clan.Kingdom.Culture.StringId });
            var trySceneExist = new FallbackForSceneMissing();
            text = trySceneExist.TryGetSceneExist(text) ? text : "scn_cutscene_family_member_death";
            __result = text;
        }
    }
}
