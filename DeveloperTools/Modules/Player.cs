using HarmonyLib;
using SLZ.Rig;
using UnityEngine;

namespace DeveloperTools.Modules
{
    internal static class Player
    {

        private static Vector3 _initialPlayerPosition;
        public static Vector3 playerHomePosition;

        public static void TeleportPlayerToHome()
        {
            // Teleports the player to the home position
            BoneLib.Player.rigManager.Teleport(playerHomePosition);
        }

        public static void SetPlayerHome()
        {
            playerHomePosition = BoneLib.Player.rigManager.transform.position;
        }

        public static void ResetPlayerHome()
        {
            playerHomePosition = _initialPlayerPosition;
        }
        
        // Patching

        [HarmonyPatch(typeof(RigManager), "Awake")]
        private static class RigManagerAwakePatch
        {
            public static void Postfix(RigManager __instance)
            {
                _initialPlayerPosition = __instance.transform.position;
                playerHomePosition = __instance.transform.position;
            }
        }
        
    }
}