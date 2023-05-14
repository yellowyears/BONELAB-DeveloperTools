using MelonLoader;
using UnityEngine;

namespace DeveloperTools
{
    internal static class Preferences
    {
        
        // Categories //
        
        private static MelonPreferences_Category _category = MelonPreferences.CreateCategory("DeveloperTools");

        // Preferences // 
        
        // ErrorLogging
        public static MelonPreferences_Entry<bool> createErrorLog;
        
        // IMGUIMenu
        public static MelonPreferences_Entry<bool> enableMenuOnStartup;
        public static MelonPreferences_Entry<KeyCode> menuToggleKey;

        public static void InitialisePreferences()
        {
            // ErrorLogging
            createErrorLog = _category.CreateEntry("createErrorLog", false, description: "Decides " +
                "whether or not an identical Player log file is produced (mainly for quest as there is no player log " +
                "for that platform)");
            
            // IMGUIMenu
            enableMenuOnStartup = _category.CreateEntry("enableMenuOnStartup", true,
                description: "Decides whether or not the GUI is enabled when the mod is initialised");
            menuToggleKey = _category.CreateEntry("menuToggleKey", KeyCode.F5,
                description: "Decides the key that is used to toggle the IMGUI menu, uses Unity KeyCodes");

            MelonPreferences.Save();
        }
    }
}