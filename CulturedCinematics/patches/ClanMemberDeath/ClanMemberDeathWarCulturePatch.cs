using HarmonyLib;
using TaleWorlds.CampaignSystem.SceneInformationPopupTypes;

namespace FullScreenCinematics.Patches.ClanMemberDeath
{
    [HarmonyPatch(typeof(ClanMemberWarDeathSceneNotificationItem), nameof(ClanMemberWarDeathSceneNotificationItem.SceneID), MethodType.Getter)]
    internal class ClanMemberDeathWarCulturePatch
    {
        [HarmonyPostfix]
        static void Postfix(ref ClanMemberWarDeathSceneNotificationItem __instance, ref string __result)
        {
            string text = string.Concat(new object[] { "scn_cutscene_family_member_death_war", "_", __instance.DeadHero.Clan.Kingdom.Culture.StringId });
            var trySceneExist = new FallbackForSceneMissing();
            text = trySceneExist.TryGetSceneExist(text) ? text : "scn_cutscene_family_member_death_war";
            __result = text;
        }
    }
}
