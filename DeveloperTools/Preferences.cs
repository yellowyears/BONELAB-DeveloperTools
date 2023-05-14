using MelonLoader;

namespace DeveloperTools
{
    internal static class Preferences
    {
        
        // Category
        private static MelonPreferences_Category _category = MelonPreferences.CreateCategory("DeveloperTools");

        // Preferences
        public static MelonPreferences_Entry<bool> createErrorLog;

        public static void InitialisePreferences()
        {
            createErrorLog = _category.CreateEntry("createErrorLog", false, description: "Decides " +
                "whether or not an identical Player log file is produced (mainly for quest as there is no player log " +
                "for that platform)");

            MelonPreferences.Save();
        }
    }
}