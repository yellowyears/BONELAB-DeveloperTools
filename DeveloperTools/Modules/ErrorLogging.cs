using HarmonyLib;
using MelonLoader;
using UnityEngine;
using Object = UnityEngine.Object;

namespace DeveloperTools.Modules
{
    internal class ErrorLogging
    {

        private static string _logDirectory = null;
        private static readonly string _melonLoaderWarning = "-------------------------------------------------------" +
                                                             "------------------------------------------------\n~   " +
                                                            "This Game has been MODIFIED using MelonLoader. DO NOT " +
                                                            "report any issues to the Game Developers!   ~\n---------" +
                                                            "--------------------------------------------------------" +
                                                            "--------------------------------------\n";
        
        public static void SetupLogging(HarmonyLib.Harmony instance)
        {
            // TODO: Improve validation here
            if (Preferences.errorLogDirectory.Value != null)
            {
                if (Preferences.errorLogDirectory.Value.StartsWith(@"\"))
                {
                    _logDirectory = MelonUtils.MelonLoaderDirectory + Preferences.errorLogDirectory.Value;
                }
                else
                {
                    _logDirectory = MelonUtils.MelonLoaderDirectory + @"\" + Preferences.errorLogDirectory.Value;
                }
            }
            else
            {
                _logDirectory = MelonUtils.MelonLoaderDirectory + @"\ErrorLog.txt";
            }
            
            // Write the ML warning first to the text file
            // This has to be done as the melon will be initialised after the original ML warning has been logged
            System.IO.File.WriteAllText(_logDirectory, _melonLoaderWarning);
            
            PatchLogMethods(instance);
        }
        
        private static void WriteLog(string message)
        {
            System.IO.File.AppendAllText(_logDirectory, message);
        }
        
        private static void PatchLogMethods(HarmonyLib.Harmony instance)
        {
            // Manually patch the methods so they are not patched despite createErrorLog preference being false
            instance.Patch(typeof(Debug).GetMethod("Log", new [] { typeof(Il2CppSystem.Object) }), 
                postfix: new HarmonyMethod(typeof(LogPatch).GetMethod("Postfix")));
            instance.Patch(typeof(Debug).GetMethod("Log", new[] { typeof(Il2CppSystem.Object), typeof(Object) }), 
                postfix: new HarmonyMethod(typeof(LogContextPatch).GetMethod("Postfix")));
        }
        
        private static class LogPatch
        {
            public static void Postfix(Il2CppSystem.Object message)
            {
                WriteLog($"{message}\n");
            }
        }        
        
        private static class LogContextPatch
        {
            public static void Postfix(Il2CppSystem.Object message, Object context)
            {
                WriteLog($"{message} {context}\n");
            }
        }
    }
}