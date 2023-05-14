using System;
using MelonLoader;
using UnityEngine;

namespace DeveloperTools.Modules.Menu
{
    [RegisterTypeInIl2Cpp]
    internal class IMGUIMenu : MonoBehaviour
    {
        public IMGUIMenu(IntPtr ptr) : base(ptr) { }

        private bool menuEnabled = true;
        
        private void Update()
        {
            if (Input.GetKeyDown(Preferences.menuToggleKey.Value))
            {
                menuEnabled = !menuEnabled;
            }
        }

        private void OnGUI()
        {
            if (!menuEnabled) return;
            
            GUI.Box(new Rect(10,10,100,90), "Developer Tools");
        }

    }
}