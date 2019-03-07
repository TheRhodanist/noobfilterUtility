using System;
using Terraria;
using Terraria.GameInput;
using Terraria.ModLoader;

namespace NoobfilterUtility
{
    public class NoobfilterUtilityPlayer : ModPlayer
    {

        private const int VANITY_SLOT_OFFSET            = 10;
        private const int ARMOR_SLOT_INDEX_START        = 0;
        private const int ARMOR_SLOT_INDEX_END          = 2;
        private const int ACCESSORY_SLOT_INDEX_START    = 3;
        private const int ACCESSORY_SLOT_INDEX_END      = 7;
        private const int AMMUNITION_SLOT_INDEX_START   = 54;
        private const int AMMUNITION_SLOT_INDEX_END     = 57;

        public override void ProcessTriggers(TriggersSet triggersSet)
        {
            if (NoobfilterUtility.swapArmorKey.JustPressed) { swapArmor(); }
            if (NoobfilterUtility.swapAccessoriesKey.JustPressed) { swapAccessories(); }
            if (NoobfilterUtility.cycleAmmunitionKey.JustPressed) { cycleAmmunition(); }
            if (NoobfilterUtility.swapArmorAndAccessroriesKey.JustPressed) {
                swapArmor();
                swapAccessories();
            }
        }

        private void swapArmor()
        {
            for (int slot = ARMOR_SLOT_INDEX_START; slot <= ARMOR_SLOT_INDEX_END; slot++) {
                if((player.armor[slot].type != 0 && 
                    player.armor[slot].stack != 0) 
                    &&
                    (player.armor[slot + VANITY_SLOT_OFFSET].type != 0 &&
                    player.armor[slot + VANITY_SLOT_OFFSET].stack != 0 &&
                    !player.armor[slot + VANITY_SLOT_OFFSET].vanity))
                {
                    Utils.Swap(ref player.armor[slot + VANITY_SLOT_OFFSET], ref player.armor[slot]);
                }
            }

            // TODO: add some feedback that a swap has happened
        }

        private void swapAccessories()
        {
            for (int slot = ACCESSORY_SLOT_INDEX_START; slot <= ACCESSORY_SLOT_INDEX_END + player.extraAccessorySlots; slot++) {
                /*
                 * there are a couple of different use-cases that come down to preference for wings:
                 * 1. swap wings only if another pair of wings is equipped in a vanity slot
                 * 2. swap wings for a different accessory for whatever reason
                 * 3. don't even consider wings due to e.g. a seperate wingslot 
                 * 
                 * maybe figure out how to allow the mod to have user-settings and implement 
                 * features for every use-case listed (or more)
                 */
                if (!player.armor[slot + VANITY_SLOT_OFFSET].vanity) {
                    Utils.Swap(ref player.armor[slot + VANITY_SLOT_OFFSET], ref player.armor[slot]);
                }
            }

            // TODO: again...feedback
        }

        private void cycleAmmunition()
        {
            int firstUsedSlot = -1;
            for (int slot = AMMUNITION_SLOT_INDEX_START; slot <= AMMUNITION_SLOT_INDEX_END; slot++) {
                if (player.inventory[slot].type != 0) { firstUsedSlot = slot; break; }
            }
            if (firstUsedSlot != -1) {
                Item bufferItem = player.inventory[firstUsedSlot];
                for (int slot = firstUsedSlot; slot < AMMUNITION_SLOT_INDEX_END; slot++) {
                    player.inventory[slot] = player.inventory[slot + 1];
                }
                player.inventory[AMMUNITION_SLOT_INDEX_END] = bufferItem;
            }
        }
    }
}
