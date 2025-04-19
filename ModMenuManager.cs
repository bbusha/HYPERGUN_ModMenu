using UnityEngine;

namespace HYPERGUN_mods
{
    public class ModMenuManager : MonoBehaviour
    {
        // Controls whether the mod menu is visible.
        private static bool isMenuOpen = false;

        // Window properties.
        private Rect windowRect = new Rect(10, 10, 420, 700);

        // Tab system variables.
        private int selectedTab = 0;
        private string[] tabTitles = new string[] { "Loot", "Player", "Ammo", "Freeze" };

        // --- Loot Tab Variables ---
        private float attachmentChance, bitsChance, heartsChance, itemSpawnChance, keysChance;

        // --- Player Tab Variables ---
        private float playerBitsSlider;

        // --- Ammo Tab Variables ---
        private float playerAmmoSlider;
        // Ammo StatPool values:
        private float ammoMissingSlider;
        private float ammoPercentSlider;
        private float ammoCurrentSlider;
        private float ammoMaxSlider;

        void Start()
        {
            // Initialize Loot tab values from PlayerMods.
            attachmentChance = PlayerMods.attachmentChance;
            bitsChance = PlayerMods.bitsChance;
            heartsChance = PlayerMods.heartsChance;
            itemSpawnChance = PlayerMods.itemSpawnChance;
            keysChance = PlayerMods.keysChance;

            // Initialize Player tab value.
            playerBitsSlider = PlayerMods.playerBits;

            // Initialize Ammo tab values.
            playerAmmoSlider = PlayerPrefs.GetFloat("PlayerAmmo", 100f);
            ammoMissingSlider = PlayerPrefs.GetFloat("PlayerAmmoMissing", 0f);
            ammoPercentSlider = PlayerPrefs.GetFloat("PlayerAmmoPercent", 1f);
            ammoCurrentSlider = PlayerPrefs.GetFloat("PlayerAmmoCurrent", 100f);
            ammoMaxSlider = PlayerPrefs.GetFloat("PlayerAmmoMax", 200f);
        }

        public static void ToggleMenu()
        {
            isMenuOpen = !isMenuOpen;
        }

        private void OnGUI()
        {
            if (!isMenuOpen)
                return;

            // Draw the window with our content.
            windowRect = GUI.Window(0, windowRect, DrawMenu, "HyperGun Mods");
        }

        private void DrawMenu(int windowID)
        {
            // Draw a toolbar to act as our tab selector.
            selectedTab = GUI.Toolbar(new Rect(10, 30, windowRect.width - 20, 30), selectedTab, tabTitles);

            // Draw a thin separating line.
            GUI.Box(new Rect(10, 65, windowRect.width - 20, 2), "");

            // Based on the selected tab, draw that tab's content.
            switch (selectedTab)
            {
                case 0: DrawLootTab(); break;
                case 1: DrawPlayerTab(); break;
                case 2: DrawAmmoTab(); break;
                case 3: DrawFreezeTab(); break;
            }

            GUI.DragWindow();
        }

        // --- Tab: Loot ---
        private void DrawLootTab()
        {
            float y = 75;
            GUI.Label(new Rect(10, y, 400, 20), "Attachment Chance: " + attachmentChance.ToString("F2"));
            y += 20;
            attachmentChance = GUI.HorizontalSlider(new Rect(10, y, 400, 20), attachmentChance, 0f, 2f);
            y += 30;

            GUI.Label(new Rect(10, y, 400, 20), "Bits Chance: " + bitsChance.ToString("F2"));
            y += 20;
            bitsChance = GUI.HorizontalSlider(new Rect(10, y, 400, 20), bitsChance, 0f, 2f);
            y += 30;

            GUI.Label(new Rect(10, y, 400, 20), "Hearts Chance: " + heartsChance.ToString("F2"));
            y += 20;
            heartsChance = GUI.HorizontalSlider(new Rect(10, y, 400, 20), heartsChance, 0f, 2f);
            y += 30;

            GUI.Label(new Rect(10, y, 400, 20), "Item Spawn Chance: " + itemSpawnChance.ToString("F2"));
            y += 20;
            itemSpawnChance = GUI.HorizontalSlider(new Rect(10, y, 400, 20), itemSpawnChance, 0f, 2f);
            y += 30;

            GUI.Label(new Rect(10, y, 400, 20), "Keys Chance: " + keysChance.ToString("F2"));
            y += 20;
            keysChance = GUI.HorizontalSlider(new Rect(10, y, 400, 20), keysChance, 0f, 2f);
            y += 40;

            if (GUI.Button(new Rect(10, y, 400, 30), "Apply Loot Chances"))
            {
                PlayerMods.SetAllLootChanceValues(attachmentChance, bitsChance, heartsChance, itemSpawnChance, keysChance);
            }
        }

        // --- Tab: Player ---
        private void DrawPlayerTab()
        {
            float y = 75;
            // Example buttons.
            if (GUI.Button(new Rect(10, y, 400, 30), "Add 100 Coins"))
            {
                PlayerMods.AddCoins(100);
            }
            y += 40;

            if (GUI.Button(new Rect(10, y, 400, 30), "Toggle God Mode"))
            {
                PlayerMods.ToggleGodMode();
            }
            y += 40;

            GUI.Label(new Rect(10, y, 400, 20), "Player Bits: " + ((int)playerBitsSlider).ToString());
            y += 20;
            playerBitsSlider = GUI.HorizontalSlider(new Rect(10, y, 400, 20), playerBitsSlider, 0f, 1000f);
            y += 30;

            if (GUI.Button(new Rect(10, y, 400, 30), "Apply Player Bits"))
            {
                PlayerMods.SetPlayerBits((int)playerBitsSlider);
            }
        }

        // --- Tab: Ammo ---
        private void DrawAmmoTab()
        {
            float y = 75;
            GUI.Label(new Rect(10, y, 400, 20), "Player Ammo (current): " + playerAmmoSlider.ToString("F0"));
            y += 20;
            playerAmmoSlider = GUI.HorizontalSlider(new Rect(10, y, 400, 20), playerAmmoSlider, 0f, 500f);
            y += 30;

            if (GUI.Button(new Rect(10, y, 400, 30), "Apply Player Ammo"))
            {
                PlayerMods.SetPlayerAmmo(playerAmmoSlider);
            }
            y += 40;

            // Ammo StatPool adjustments.
            GUI.Label(new Rect(10, y, 400, 20), "Ammo Missing: " + ammoMissingSlider.ToString("F2"));
            y += 20;
            ammoMissingSlider = GUI.HorizontalSlider(new Rect(10, y, 400, 20), ammoMissingSlider, 0f, 100f);
            y += 30;

            GUI.Label(new Rect(10, y, 400, 20), "Ammo Percent: " + ammoPercentSlider.ToString("F2"));
            y += 20;
            ammoPercentSlider = GUI.HorizontalSlider(new Rect(10, y, 400, 20), ammoPercentSlider, 0f, 1f);
            y += 30;

            GUI.Label(new Rect(10, y, 400, 20), "Ammo Current: " + ammoCurrentSlider.ToString("F2"));
            y += 20;
            ammoCurrentSlider = GUI.HorizontalSlider(new Rect(10, y, 400, 20), ammoCurrentSlider, 0f, 500f);
            y += 30;

            GUI.Label(new Rect(10, y, 400, 20), "Ammo Max: " + ammoMaxSlider.ToString("F2"));
            y += 20;
            ammoMaxSlider = GUI.HorizontalSlider(new Rect(10, y, 400, 20), ammoMaxSlider, 0f, 500f);
            y += 30;

            if (GUI.Button(new Rect(10, y, 400, 30), "Apply Ammo StatPool Values"))
            {
                PlayerMods.SetPlayerAmmoStatPool(ammoMissingSlider, ammoPercentSlider, ammoCurrentSlider, ammoMaxSlider);
            }
        }

        // --- Tab: Freeze ---
        private void DrawFreezeTab()
        {
            float y = 75;
            // Freeze Health
            PlayerMods.freezeHealth = GUI.Toggle(new Rect(10, y, 400, 25), PlayerMods.freezeHealth, "Freeze Health (Infinite)");
            y += 30;
            GUI.Label(new Rect(10, y, 400, 20), "Infinite Health Value: " + PlayerMods.freezeHealthValue.ToString("F0"));
            y += 20;
            PlayerMods.freezeHealthValue = GUI.HorizontalSlider(new Rect(10, y, 400, 20), PlayerMods.freezeHealthValue, 100f, 10000f);
            y += 40;

            // Freeze Bits
            PlayerMods.freezeBits = GUI.Toggle(new Rect(10, y, 400, 25), PlayerMods.freezeBits, "Freeze Bits (Infinite)");
            y += 30;
            GUI.Label(new Rect(10, y, 400, 20), "Infinite Bits Value: " + PlayerMods.freezeBitsValue.ToString());
            y += 20;
            PlayerMods.freezeBitsValue = (int)GUI.HorizontalSlider(new Rect(10, y, 400, 20), PlayerMods.freezeBitsValue, 100, 1000000);
        }
    }
}