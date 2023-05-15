using BoneLib;
using HarmonyLib;
using SLZ.Rig;
using UnityEngine;

namespace DeveloperTools.Modules
{
    internal class Player
    {

        private static Vector3 _rigHomePosition;
        
        public static void InitialisePlayer(HarmonyLib.Harmony instance)
        {
            instance.Patch(typeof(RigManager).GetMethod("Awake"),
                postfix: new HarmonyMethod(typeof(RigManagerAwakePatch).GetMethod("Postfix")));
        }
        
        public static void ResetPlayerPosition()
        {
            BoneLib.Player.rigManager.transform.position = _rigHomePosition;
        }
        
        // Patching

        private static class RigManagerAwakePatch
        {
            public static void Postfix(RigManager __instance)
            {
                _rigHomePosition = __instance.transform.position;
            }
        }
        
    }
}