using System;
using MelonLoader;
using UnityEngine;

namespace DeveloperTools.Modules.Menu
{
    [RegisterTypeInIl2Cpp]
    internal class IMGUIMenu : MonoBehaviour
    {
        public IMGUIMenu(IntPtr ptr) : base(ptr) { }

        public bool menuEnabled;

        private void OnGUI()
        {
            if (!menuEnabled) return;
            
            GUI.Box(new Rect(10,10,150,90), "Developer Tools");
            
            if(GUI.Button(new Rect(20,40,130,20), "Reset Player Position"))
            {
                Player.ResetPlayerPosition();
            }
        }

    }
}