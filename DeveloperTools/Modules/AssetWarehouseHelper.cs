using SLZ.Marrow.Warehouse;
using UnhollowerRuntimeLib;
using Il2CppSystem.Collections.Generic;


namespace DeveloperTools.Modules
{
    internal static class AssetWarehouseHelper
    {

        public static List<Crate> levelCrates = new List<Crate>();

        public static void GetLevelCrates()
        {
            var crates = AssetWarehouse.Instance.GetCrates();
            
            // Clear the list to prevent duplicates being added
            levelCrates.Clear();
            
            foreach (var crate in crates)
            {
                // Check whether the crate is a level crate
                if (crate.GetIl2CppType() == Il2CppType.Of<LevelCrate>() && !crate.Redacted && crate.Pallet.Author != "SLZ")
                {
                    // Default sorted by author
                    levelCrates.Add(crate); 
                }
            }
        }
    }
}