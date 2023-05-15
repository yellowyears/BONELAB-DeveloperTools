﻿using UnityEngine;

namespace DeveloperTools.Modules.Menu
{
    internal class IMGUIMenuManager
    {

        private static IMGUIMenu _instance;
        
        public static void InitialiseGUI()
        {
            var menuObject = new GameObject("Developer Tools Menu");
            _instance = menuObject.AddComponent<IMGUIMenu>();

            _instance.menuEnabled = Preferences.enableMenuOnStartup.Value;
        }

        // TODO: add method to add menu items from inside other classes?
        public static void AddMenuOption()
        {
            
        }

        private static void ToggleMenu()
        {
            _instance.menuEnabled = !_instance.menuEnabled;
        }

        public static void ListenForInput()
        {
            if (Input.GetKeyDown(Preferences.menuToggleKey.Value)) ToggleMenu();
        }
        
    }
}