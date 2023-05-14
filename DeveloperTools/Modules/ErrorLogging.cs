using HarmonyLib;
using MelonLoader;
using UnhollowerBaseLib;
using UnityEngine;
using System.IO;

namespace DeveloperTools.Modules
{
    internal class ErrorLogging
    {

        private static string _logDirectory = MelonUtils.MelonLoaderDirectory + @"\ErrorLog.txt";
        private static readonly string _melonLoaderWarning = "-------------------------------------------------------" +
                                                             "------------------------------------------------\n~   " +
                                                            "This Game has been MODIFIED using MelonLoader. DO NOT " +
                                                            "report any issues to the Game Developers!   ~\n---------" +
                                                            "--------------------------------------------------------" +
                                                            "--------------------------------------\n";
        
        public static void SetupLogging(HarmonyLib.Harmony instance)
        {
            // Check for a previous log and renames it to ErrorLog-prev.txt
            CheckForLog();
            
            // Write the ML warning first to the text file
            // This has to be done as the melon will be initialised after the original ML warning has been logged
            File.WriteAllText(_logDirectory, _melonLoaderWarning);
            
            // Manually patch the methods so they are not patched when Preferences.createErrorLog is false
            PatchLogMethods(instance);
        }

        private static void CheckForLog()
        {
            // Check for any existing log before proceeding
            if (!File.Exists(_logDirectory)) return;

            // Create the -prev filepath from the _logDirectory
            var prevLogPath = MelonUtils.MelonLoaderDirectory + @"\ErrorLog-prev.txt";
            
            // Copy the existing log into the new prev log
            File.Copy(_logDirectory, prevLogPath);
            
            // Delete the original log
            File.Delete(_logDirectory);
        }
        
        private static void WriteLog(string message)
        {
            // Validate whether or not the message ends with a newline to prevent the logs from being on the same line
            if (!message.EndsWith("\n")) message += "\n";
            
            File.AppendAllText(_logDirectory, message);
        }

        private static void PatchLogMethods(HarmonyLib.Harmony instance)
        {
            // Debug.Log
            instance.Patch(typeof(Debug).GetMethod("Log", new[] { typeof(Il2CppSystem.Object) }),
                postfix: new HarmonyMethod(typeof(LogPatch).GetMethod("Postfix")));
            instance.Patch(typeof(Debug).GetMethod("Log", new[] { typeof(Il2CppSystem.Object), typeof(Object) }),
                postfix: new HarmonyMethod(typeof(LogContextPatch).GetMethod("Postfix")));
            instance.Patch(typeof(Debug).GetMethod("LogFormat", new[] { typeof(string), typeof(Il2CppReferenceArray<Il2CppSystem.Object>) }),
                postfix: new HarmonyMethod(typeof(LogFormatPatch).GetMethod("Postfix")));

            // Debug.LogWarning
            instance.Patch(typeof(Debug).GetMethod("LogWarning", new[] { typeof(Il2CppSystem.Object) }),
                postfix: new HarmonyMethod(typeof(LogWarningPatch).GetMethod("Postfix")));
            instance.Patch(typeof(Debug).GetMethod("LogWarning", new[] { typeof(Il2CppSystem.Object), typeof(Object) }),
                postfix: new HarmonyMethod(typeof(LogWarningContextPatch).GetMethod("Postfix")));
            instance.Patch(typeof(Debug).GetMethod("LogWarningFormat", new[] { typeof(string), typeof(Il2CppReferenceArray<Il2CppSystem.Object>) }),
                postfix: new HarmonyMethod(typeof(LogWarningFormatPatch).GetMethod("Postfix")));

            // Debug.LogError
            instance.Patch(typeof(Debug).GetMethod("LogError", new[] { typeof(Il2CppSystem.Object) }),
                postfix: new HarmonyMethod(typeof(LogErrorPatch).GetMethod("Postfix")));
            instance.Patch(typeof(Debug).GetMethod("LogError", new[] { typeof(Il2CppSystem.Object), typeof(Object) }),
                postfix: new HarmonyMethod(typeof(LogErrorContextPatch).GetMethod("Postfix")));
            instance.Patch(typeof(Debug).GetMethod("LogErrorFormat", new[] { typeof(string), typeof(Il2CppReferenceArray<Il2CppSystem.Object>) }),
                postfix: new HarmonyMethod(typeof(LogErrorFormatPatch).GetMethod("Postfix")));
            instance.Patch(typeof(Debug).GetMethod("LogErrorFormat", new[] { typeof(Object), typeof(string), typeof(Il2CppReferenceArray<Il2CppSystem.Object>) }),
                postfix: new HarmonyMethod(typeof(LogErrorFormatContextPatch).GetMethod("Postfix")));

            // Debug.LogException
            instance.Patch(typeof(Debug).GetMethod("LogException", new[] { typeof(Il2CppSystem.Object) }),
                postfix: new HarmonyMethod(typeof(LogExceptionPatch).GetMethod("Postfix")));
            instance.Patch(typeof(Debug).GetMethod("LogException", new[] { typeof(Il2CppSystem.Object), typeof(Object) }),
                postfix: new HarmonyMethod(typeof(LogExceptionContextPatch).GetMethod("Postfix")));
            
            // Debug.LogAssertion
            instance.Patch(typeof(Debug).GetMethod("LogAssertion", new[] { typeof(Il2CppSystem.Object) }),
                postfix: new HarmonyMethod(typeof(LogAssertionPatch).GetMethod("Postfix")));
            instance.Patch(typeof(Debug).GetMethod("LogAssertionFormat", new[] { typeof(string), typeof(Il2CppReferenceArray<Il2CppSystem.Object>) }),
                postfix: new HarmonyMethod(typeof(LogAssertionFormatPatch).GetMethod("Postfix")));
        }
        
        // Debug.Log
        
        private static class LogPatch
        {
            public static void Postfix(Il2CppSystem.Object message)
            {
                WriteLog($"{message}");
            }
        }        
        
        private static class LogContextPatch
        {
            public static void Postfix(Il2CppSystem.Object message, Object context)
            {
                WriteLog($"{message} {context}");
            }
        }

        private static class LogFormatPatch
        {
            public static void Postfix(string format, Il2CppReferenceArray<Il2CppSystem.Object> args)
            {
                WriteLog($"{format}");
            }
        }

        // Debug.LogWarning
        
        private static class LogWarningPatch
        {
            public static void Postfix(Il2CppSystem.Object message)
            {
                WriteLog($"{message}");
            }
        }

        private static class LogWarningContextPatch
        {
            public static void Postfix(Il2CppSystem.Object message, Object context)
            {
                WriteLog($"{message} {context}");
            }
        }

        private static class LogWarningFormatPatch
        {
            public static void Postfix(string format, Il2CppReferenceArray<Il2CppSystem.Object> args)
            {
                WriteLog(format);
            }
        }
        
        // Debug.LogError
        
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

        private static class LogErrorFormatPatch
        {
            public static void Postfix(string format, Il2CppReferenceArray<Il2CppSystem.Object> args)
            {
                WriteLog(format);
            }
        }

        private static class LogErrorFormatContextPatch
        {
            public static void Postfix(Object context, string format, Il2CppReferenceArray<Il2CppSystem.Object> args)
            {
                WriteLog($"{format} {context}");
            }
        }
        
        // Debug.LogException
        
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

        // Debug.LogAssertion
        
        private static class LogAssertionPatch
        {
            public static void Postfix(Il2CppSystem.Object message)
            {
                WriteLog($"{message}");
            }
        }

        private static class LogAssertionFormatPatch
        {
            public static void Postfix(string format, Il2CppReferenceArray<Il2CppSystem.Object> args)
            {
                WriteLog(format);
            }
        }
        
    }
}