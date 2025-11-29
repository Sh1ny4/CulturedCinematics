using HarmonyLib;
using TaleWorlds.MountAndBlade;

namespace CulturedCinematics
{
    public class SubModule : MBSubModuleBase
    {
        protected override void OnSubModuleLoad()
        {
            base.OnSubModuleLoad();
            new Harmony("CulturedCinematics.CulturedCinematics").PatchAll();
        }
    }
}
