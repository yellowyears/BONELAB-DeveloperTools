using HarmonyLib;
using MelonLoader;
using UnityEngine;

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
            
            // Debug.Log
            instance.Patch(typeof(Debug).GetMethod("Log", new [] { typeof(Il2CppSystem.Object) }), 
                postfix: new HarmonyMethod(typeof(LogPatch).GetMethod("Postfix")));
            instance.Patch(typeof(Debug).GetMethod("Log", new[] { typeof(Il2CppSystem.Object), typeof(Object) }), 
                postfix: new HarmonyMethod(typeof(LogContextPatch).GetMethod("Postfix")));
            
            // Debug.LogWarning
            instance.Patch(typeof(Debug).GetMethod("LogWarning", new[] { typeof(Il2CppSystem.Object) }),
                postfix: new HarmonyMethod(typeof(LogWarningPatch).GetMethod("Postfix")));
            instance.Patch(typeof(Debug).GetMethod("LogWarning", new[] { typeof(Il2CppSystem.Object), typeof(Object) }),
                postfix: new HarmonyMethod(typeof(LogWarningContextPatch).GetMethod("Postfix")));
            
            // Debug.LogError
            instance.Patch(typeof(Debug).GetMethod("LogError", new[] { typeof(Il2CppSystem.Object) }),
                postfix: new HarmonyMethod(typeof(LogErrorPatch).GetMethod("Postfix")));
            instance.Patch(typeof(Debug).GetMethod("LogError", new[] { typeof(Il2CppSystem.Object), typeof(Object) }),
                postfix: new HarmonyMethod(typeof(LogErrorContextPatch).GetMethod("Postfix")));
            
            // Debug.LogException
            instance.Patch(typeof(Debug).GetMethod("LogException", new[] { typeof(Il2CppSystem.Object) }),
                postfix: new HarmonyMethod(typeof(LogExceptionPatch).GetMethod("Postfix")));
            instance.Patch(typeof(Debug).GetMethod("LogException", new[] { typeof(Il2CppSystem.Object), typeof(Object) }),
                postfix: new HarmonyMethod(typeof(LogExceptionContextPatch).GetMethod("Postfix")));
            
            // Debug.LogAssertion
            instance.Patch(typeof(Debug).GetMethod("LogAssertion", new[] { typeof(Il2CppSystem.Object) }),
                postfix: new HarmonyMethod(typeof(LogAssertionPatch).GetMethod("Postfix")));
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

        
        private static class LogWarningPatch
        {
            public static void Postfix(Il2CppSystem.Object message)
            {
                WriteLog($"{message}\n");
            }
        }

        private static class LogWarningContextPatch
        {
            public static void Postfix(Il2CppSystem.Object message, Object context)
            {
                WriteLog($"{message} {context}\n");
            }
        }

        
        private static class LogErrorPatch
        {
            public static void Postfix(Il2CppSystem.Object message)
            {
                WriteLog($"{message}");
            }
        }     
        
        private static class LogErrorContextPatch
        {
            public static void Postfix(Il2CppSystem.Object message, Object context)
            {
                WriteLog($"{message} {context}");
            }
        }

        
        private static class LogExceptionPatch
        {
            public static void Postfix(Il2CppSystem.Object message)
            {
                WriteLog($"{message}");
            }
        }
        
        private static class LogExceptionContextPatch
        {
            public static void Postfix(Il2CppSystem.Object message, Object context)
            {
                WriteLog($"{message} {context}");
            }
        }
        
        
        private static class LogAssertionPatch
        {
            public static void Postfix(Il2CppSystem.Object message)
            {
                WriteLog($"{message}");
            }
        }
        
    }
}