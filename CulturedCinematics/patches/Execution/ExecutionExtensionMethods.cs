using TaleWorlds.CampaignSystem.SceneInformationPopupTypes;
using TaleWorlds.Core;

namespace CulturedCinematics.patches.Execution
{
    public static class ExecutionExtensionMethods
    {
        public static Banner[] GetBanners(this HeroExecutionSceneNotificationData str)
        {
            return new Banner[]
            {
                str.Executer.ClanBanner,
                str.Victim.ClanBanner
            };
        }
    }
}
