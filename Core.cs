using MelonLoader;
using UnityEngine;

namespace HYPERGUN_mods
{
    public static class BuildInfo
    {
        public const string Name = "HYPERGUN_mods";
        public const string Author = "TheOriginalGhost";
        public const string Company = null;
        public const string Version = "1.0.0";
        public const string DownloadLink = null;
    }

    public class HYPERGUN_mods : MelonMod
    {
        public override void OnApplicationStart()
        {
            MelonLogger.Msg("OnApplicationStart");
            // Load persistent values – existing and new freeze ones.
            PlayerMods.attachmentChance = PlayerPrefs.GetFloat("AttachmentChance", 1f);
            PlayerMods.bitsChance = PlayerPrefs.GetFloat("BitsChance", 0.5f);
            PlayerMods.heartsChance = PlayerPrefs.GetFloat("HeartsChance", 0.65f);
            PlayerMods.itemSpawnChance = PlayerPrefs.GetFloat("ItemSpawnChance", 0.11f);
            PlayerMods.keysChance = PlayerPrefs.GetFloat("KeysChance", 0.8f);
            PlayerMods.playerBits = PlayerPrefs.GetInt("PlayerBits", 0);
            PlayerPrefs.GetFloat("PlayerAmmo", 100f);
            // (Other persistent loads for ammo StatPool are done in the mod menu Start() method.)
        }

        public override void OnSceneWasInitialized(int buildIndex, string sceneName)
        {
            MelonLogger.Msg("OnSceneWasInitialized: " + buildIndex + " | " + sceneName);

            // Ensure there's only one instance of the ModMenuManager.
            if (GameObject.Find("ModMenu") == null)
            {
                GameObject menuObj = new GameObject("ModMenu");
                menuObj.AddComponent<ModMenuManager>();
                UnityEngine.Object.DontDestroyOnLoad(menuObj);
            }
        }

        public override void OnUpdate()
        {
            // Toggle the mod menu with the "M" key.
            if (Input.GetKeyDown(KeyCode.M))
            {
                MelonLogger.Msg("Toggling Mod Menu");
                ModMenuManager.ToggleMenu();
            }
            // Enforce freeze values each frame.
            PlayerMods.ApplyFreeze();
        }
    }
}