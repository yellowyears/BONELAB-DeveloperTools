using DeveloperTools.Modules;
using MelonLoader;
using UnityEngine;
using DeveloperTools.Modules.Menu;

namespace DeveloperTools
{
    public static class BuildInfo
    {
        public const string Name = "DeveloperTools"; // Name of the Mod.  (MUST BE SET)
        public const string Author = "yellowyears"; // Author of the Mod.  (Set as null if none)
        public const string Company = null; // Company that made the Mod.  (Set as null if none)
        public const string Version = "1.0.0"; // Version of the Mod.  (MUST BE SET)
        public const string DownloadLink = null; // Download Link for the Mod.  (Set as null if none)
    }

    internal class Main : MelonMod
    {
        
        // Melon Callbacks
        public override void OnInitializeMelon()
        {
            Preferences.InitialisePreferences();

            if (Preferences.createErrorLog.Value)
            {
                ErrorLogging.SetupLogging(HarmonyInstance);
            }

            // Return at this point as nothing else is useful for quest
            if (Application.platform == RuntimePlatform.Android) return;

            // Initialise the menu when the current level is fully initialised
            BoneLib.Hooking.OnLevelInitialized += info => IMGUIMenuManager.InitialiseGUI();
            MelonEvents.OnUpdate.Subscribe(IMGUIMenuManager.ListenForInput);
        }

    }
}