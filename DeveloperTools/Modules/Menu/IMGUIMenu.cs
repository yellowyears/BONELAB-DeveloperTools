﻿using System;
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
        }

    }
}