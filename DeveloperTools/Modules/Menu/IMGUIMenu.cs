using System;
using MelonLoader;
using SLZ.Marrow.SceneStreaming;
using UnityEngine;

namespace DeveloperTools.Modules.Menu
{
    [RegisterTypeInIl2Cpp]
    internal class IMGUIMenu : MonoBehaviour
    {
        public IMGUIMenu(IntPtr ptr) : base(ptr) { }

        public bool menuEnabled;

        private Rect _previousRect;

        private void OnGUI()
        {
            if (!menuEnabled) return;
            
            BuildPlayerMenu();
            BuildAssetWarehouseMenu();
        }

        // Module Player
        private void BuildPlayerMenu()
        {
            GUI.Box(new Rect(10,10,150,90), "Player");
            
            if(GUI.Button(new Rect(20,40,130,20), "Reset Player Position"))
            {
                Player.ResetPlayerPosition();
            }
        }

        // Module AssetWarehouseManager
        private void BuildAssetWarehouseMenu()
        {
            var root = new Rect(160, 10, 150, 90);
            _previousRect = new Rect(170, 40, 130, 20);
            
            GUI.Box(root, "Asset Warehouse");

            if (GUI.Button(_previousRect, "Reload Level"))
            {
                SceneStreamer.Reload();
            }

            if (AssetWarehouseManager.levelCrates.Count < 0)
            {
                GUI.Label(new Rect(root.x + 10, root.y + 30, root.width - 20, 20), "ERROR: No Level Crates!");
            }
            else
            {
                foreach (var level in AssetWarehouseManager.levelCrates)
                {
                    _previousRect = new Rect(_previousRect.x, _previousRect.y + 30, 130, 20);

                    if(GUI.Button(_previousRect, level.Title))
                    {
                        // Load the level that the pressed button corresponds to
                        SceneStreamer.Load(level.Barcode);
                    }
                }
            }
        }
        
    }
}