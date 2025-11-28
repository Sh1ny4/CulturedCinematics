using System;
using System.Runtime.ExceptionServices;
using System.Security;
using TaleWorlds.Engine;

namespace FullScreenCinematics.Patches
{
    internal class FallbackForSceneMissing
    {
        //honestly I would recommend this, it would be better to make sure every culture has a map and removing this code, since it's a security risk
        [HandleProcessCorruptedStateExceptions]
        [SecurityCritical]
        public bool TryGetSceneExist(string text)
        {
            try
            {
                SceneInitializationData initData = new SceneInitializationData(initializeWithDefaults: true);
                Scene scene = Scene.CreateNewScene(initialize_physics: true, enable_decals: true, DecalAtlasGroup.Battle);
                scene.Read(text, ref initData);
                return true;
            }
            catch(Exception ex)
            {
                return false;
            }
        }
    }
}
