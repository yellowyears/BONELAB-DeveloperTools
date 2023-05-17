using HarmonyLib;
using SLZ.Data;
using SLZ.Player;
using SLZ.Rig;
using UnityEngine;

namespace DeveloperTools.Modules
{
    internal static class Player
    {

        // Home Variables
        
        private static Vector3 _initialPlayerPosition;
        public static Vector3 playerHomePosition;
        
        // Ammo Variables
        
        private static AmmoInventory ammoInventory => AmmoInventory.Instance;
        public static AmmoGroup lightGroup => ammoInventory.lightAmmoGroup;
        public static AmmoGroup mediumGroup => ammoInventory.mediumAmmoGroup;
        public static AmmoGroup heavyGroup => ammoInventory.heavyAmmoGroup;

        // Home
        
        public static void TeleportPlayerToHome()
        {
            // Teleports the player to the home position
            BoneLib.Player.rigManager.Teleport(playerHomePosition);
        }

        public static void SetPlayerHome()
        {
            playerHomePosition = BoneLib.Player.rigManager.physicsRig.feet.transform.position;
        }

        public static void ResetPlayerHome()
        {
            playerHomePosition = _initialPlayerPosition;
        }
        
        // Ammo

        public static void GiveAmmo(AmmoGroup ammoType, int count)
        {
            if (ammoType == lightGroup) ammoInventory.AddCartridge(lightGroup, count);
            else if (ammoType == mediumGroup) ammoInventory.AddCartridge(mediumGroup, count);
            else if (ammoType == heavyGroup) ammoInventory.AddCartridge(heavyGroup, count);
        }
        
        // Patching

        [HarmonyPatch(typeof(RigManager), "Awake")]
        private static class RigManagerAwakePatch
        {
            public static void Postfix(RigManager __instance)
            {
                _initialPlayerPosition = __instance.physicsRig.feet.transform.position;
                playerHomePosition = _initialPlayerPosition;
            }
        }
        
    }
}