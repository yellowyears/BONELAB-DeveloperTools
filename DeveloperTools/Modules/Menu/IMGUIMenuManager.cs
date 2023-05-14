using UnityEngine;

namespace DeveloperTools.Modules.Menu
{
    internal class IMGUIMenuManager
    {
        public static void InitialiseGUI()
        {
            var test = new GameObject("Developer Tools Menu");
            test.AddComponent<IMGUIMenu>();
        }
    }
}