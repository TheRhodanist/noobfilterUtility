using Terraria.ModLoader;

namespace NoobfilterUtility
{
	public class NoobfilterUtility : Mod
	{

        internal static ModHotKey swapArmorKey;
        internal static ModHotKey swapAccessoriesKey;
        internal static ModHotKey cycleAmmunitionKey;
        internal static ModHotKey swapArmorAndAccessroriesKey;

        

        public override void Load()
        {
            swapAccessoriesKey          = RegisterHotKey("Swap Accessories", "");
            swapArmorKey                = RegisterHotKey("Swap Armor", "");
            swapArmorAndAccessroriesKey = RegisterHotKey("Swap Armor and Accessories", "");
            cycleAmmunitionKey          = RegisterHotKey("Cycle through ammunition", "");
        }

        public override void Unload()
        {
            swapArmorAndAccessroriesKey =
            swapArmorKey                =
            swapAccessoriesKey          =
            cycleAmmunitionKey          = null;
        }

    }
}
