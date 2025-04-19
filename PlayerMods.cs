using UnityEngine;
using MelonLoader;

namespace HYPERGUN_mods
{
    public static class PlayerMods
    {
        // --- Existing persistent values ---
        public static float attachmentChance = 1f;
        public static float bitsChance = 0.5f;
        public static float heartsChance = 0.65f;
        public static float itemSpawnChance = 0.11f;
        public static float keysChance = 0.8f;
        public static int playerBits = 0;
        public static float playerAmmo = 100f;
        // Ammo StatPool values:
        public static float ammoMissing = 0f;
        public static float ammoPercent = 1f;
        public static float ammoCurrent = 100f;
        public static float ammoMax = 200f;

        // --- NEW: Freeze Flags and Values ---
        public static bool freezeHealth = false;
        public static float freezeHealthValue = 9999f;
        public static bool freezeBits = false;
        public static int freezeBitsValue = 999999;

        public static void AddCoins(int amount)
        {
            int coins = PlayerPrefs.GetInt("Coins", 0);
            coins += amount;
            PlayerPrefs.SetInt("Coins", coins);
            MelonLogger.Msg("Added " + amount + " coins. New total: " + coins);
        }

        public static void ToggleGodMode()
        {
            // Your existing implementation.
            MelonLogger.Msg("God Mode toggled (implementation not detailed here).");
        }

        public static void SetAllLootChanceValues(float att, float bits, float hearts, float item, float keys)
        {
            attachmentChance = att;
            bitsChance = bits;
            heartsChance = hearts;
            itemSpawnChance = item;
            keysChance = keys;

            PlayerPrefs.SetFloat("AttachmentChance", attachmentChance);
            PlayerPrefs.SetFloat("BitsChance", bitsChance);
            PlayerPrefs.SetFloat("HeartsChance", heartsChance);
            PlayerPrefs.SetFloat("ItemSpawnChance", itemSpawnChance);
            PlayerPrefs.SetFloat("KeysChance", keysChance);

            MelonLogger.Msg("Set Loot Chances: Attachment: " + att + ", Bits: " + bits +
                            ", Hearts: " + hearts + ", Item: " + item + ", Keys: " + keys);

            GameObject lootManagerObj = GameObject.Find("_Loot Manager");
            if (lootManagerObj != null)
            {
                HYPERGUN.LootManager lootManager = lootManagerObj.GetComponent<HYPERGUN.LootManager>();
                if (lootManager != null)
                {
                    lootManager.attachmentChance = attachmentChance;
                    lootManager.bitsChance = bitsChance;
                    lootManager.heartsChance = heartsChance;
                    lootManager.itemSpawnChance = itemSpawnChance;
                    lootManager.keysChance = keysChance;
                    MelonLogger.Msg("Applied loot chance values to Loot Manager.");
                }
                else
                {
                    MelonLogger.Warning("Loot Manager component not found on _Loot Manager.");
                }
            }
            else
            {
                MelonLogger.Warning("_Loot Manager object not found in the scene.");
            }
        }

        public static void SetPlayerBits(int bits)
        {
            playerBits = bits;
            PlayerPrefs.SetInt("PlayerBits", playerBits);
            MelonLogger.Msg("Player bits set to: " + playerBits);

            GameObject playerObj = GameObject.Find("_Player");
            if (playerObj != null)
            {
                HYPERGUN.Player playerComponent = playerObj.GetComponent<HYPERGUN.Player>();
                if (playerComponent != null)
                {
                    playerComponent.bits = playerBits;
                    MelonLogger.Msg("Applied player bits to HYPERGUN.Player component.");
                }
                else
                {
                    MelonLogger.Warning("HYPERGUN.Player component not found on _Player object.");
                }
            }
            else
            {
                MelonLogger.Warning("_Player object not found in the scene.");
            }
        }

        public static void SetPlayerAmmo(float ammo)
        {
            playerAmmo = ammo;
            PlayerPrefs.SetFloat("PlayerAmmo", playerAmmo);
            MelonLogger.Msg("Player Ammo set to: " + playerAmmo);

            GameObject playerObj = GameObject.Find("_Player");
            if (playerObj != null)
            {
                HYPERGUN.Player playerComponent = playerObj.GetComponent<HYPERGUN.Player>();
                if (playerComponent != null)
                {
                    NVYVEStudios.StatPool ammoPool = playerComponent.ammo;
                    if (ammoPool != null)
                    {
                        ammoPool.current = ammo;
                        MelonLogger.Msg("Applied player ammo (" + ammo + ") to the StatPool component.");
                    }
                    else
                    {
                        MelonLogger.Warning("Ammo StatPool not found on HYPERGUN.Player component.");
                    }
                }
                else
                {
                    MelonLogger.Warning("HYPERGUN.Player component not found on _Player object.");
                }
            }
            else
            {
                MelonLogger.Warning("_Player object not found in the scene.");
            }
        }

        public static void SetPlayerAmmoStatPool(float missing, float percent, float current, float max)
        {
            ammoMissing = missing;
            ammoPercent = percent;
            ammoCurrent = current;
            ammoMax = max;

            PlayerPrefs.SetFloat("PlayerAmmoMissing", ammoMissing);
            PlayerPrefs.SetFloat("PlayerAmmoPercent", ammoPercent);
            PlayerPrefs.SetFloat("PlayerAmmoCurrent", ammoCurrent);
            PlayerPrefs.SetFloat("PlayerAmmoMax", ammoMax);

            MelonLogger.Msg("Ammo StatPool set to -> Missing: " + ammoMissing + ", Percent: " + ammoPercent +
                            ", Current: " + ammoCurrent + ", Max: " + ammoMax);

            GameObject playerObj = GameObject.Find("_Player");
            if (playerObj != null)
            {
                HYPERGUN.Player playerComponent = playerObj.GetComponent<HYPERGUN.Player>();
                if (playerComponent != null)
                {
                    NVYVEStudios.StatPool ammoPool = playerComponent.ammo;
                    if (ammoPool != null)
                    {
                        ammoPool.current = ammoCurrent;
                        ammoPool.max = ammoMax;
                        MelonLogger.Msg("Applied Ammo StatPool values to the StatPool component.");
                    }
                    else
                    {
                        MelonLogger.Warning("Ammo StatPool component not found on HYPERGUN.Player.");
                    }
                }
                else
                {
                    MelonLogger.Warning("HYPERGUN.Player component not found on _Player object.");
                }
            }
            else
            {
                MelonLogger.Warning("_Player object not found in the scene.");
            }
        }

        public static void ApplyFreeze()
        {
            GameObject playerObj = GameObject.Find("_Player");
            if (playerObj != null)
            {
                HYPERGUN.Player playerComponent = playerObj.GetComponent<HYPERGUN.Player>();
                if (playerComponent != null)
                {
                    if (freezeHealth)
                    {
                        // Assuming health is a StatPool, update its current value.
                        playerComponent.health.current = freezeHealthValue;
                    }
                    if (freezeBits)
                    {
                        playerComponent.bits = freezeBitsValue;
                    }
                }
                else
                {
                    MelonLogger.Warning("HYPERGUN.Player component not found on _Player.");
                }
            }
            else
            {
                MelonLogger.Warning("_Player object not found in the scene.");
            }
        }
    }
}