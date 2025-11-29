using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
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
                (str.Executer?.Father != null) ? str.Executer.Father.ClanBanner : str.Executer.ClanBanner,
                (str.Victim?.Father != null) ? str.Victim.Father.ClanBanner : str.Victim.ClanBanner,
                (str.Executer?.Father != null) ? str.Executer.Father.ClanBanner : str.Executer.ClanBanner,
                (str.Victim?.Father != null) ? str.Victim.Father.ClanBanner : str.Victim.ClanBanner
            };
        }
    }
}
