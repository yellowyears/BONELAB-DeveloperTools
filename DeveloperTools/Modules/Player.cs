using HarmonyLib;
using SLZ.Rig;
using UnityEngine;

namespace DeveloperTools.Modules
{
    internal static class Player
    {

        private static Vector3 _rigHomePosition;

        public static void ResetPlayerPosition()
        {
            BoneLib.Player.rigManager.transform.position = _rigHomePosition;
        }
        
        // Patching

        [HarmonyPatch(typeof(RigManager), "Awake")]
        private static class RigManagerAwakePatch
        {
            public static void Postfix(RigManager __instance)
            {
                _rigHomePosition = __instance.transform.position;
            }
        }
        
    }
}