using BepInEx;
using BepInEx.Configuration;
using HarmonyLib;
using System.Collections.Generic;
using UnityEngine;

namespace StatsViewer
{
    [BepInPlugin("radsi.statsviewer", "StatsViewer", "1.0.0")]
    public class Plugin : BaseUnityPlugin
    {
        public static ConfigEntry<bool> FPS;
        public static ConfigEntry<bool> RAM;
        public static ConfigEntry<bool> AUDIO;
        public static ConfigEntry<bool> SPECS;

        public static Dictionary<string, ConfigEntry<bool>> ConfigDictionary;

        public static GameObject graphy;

        public static bool isEnabled = false;

        private void Awake()
        {
            Logger.LogInfo($"Plugin StatsViewer is loaded!");
            new Harmony("radsi.statsviewer").PatchAll();

            FPS = Config.Bind("Settings", "Show_FPS", false);
            RAM = Config.Bind("Settings", "Show_RAM", false);
            AUDIO = Config.Bind("Settings", "Show_audio", false);
            SPECS = Config.Bind("Settings", "Show_PC_specs", false);

            ConfigDictionary = new Dictionary<string, ConfigEntry<bool>>
            {
                { "FPS", FPS },
                { "RAM", RAM },
                { "AUDIO", AUDIO },
                { "ADVANCED", SPECS }
            };
        }

        private void Update()
        {
            if (Input.GetKeyDown(KeyCode.F1))
            {
                if (graphy == null) { graphy = GameObject.Find("CoreObject/[Graphy]/"); }
                foreach (Transform child in graphy.transform)
                {
                    foreach (var entry in ConfigDictionary)
                    {
                        if (!isEnabled && child.gameObject.name.Contains(entry.Key))
                        {
                            Logger.LogInfo(entry.Key + "   " + entry.Value.Value);
                            child.gameObject.SetActive(entry.Value.Value);
                            break;
                        }

                        child.gameObject.SetActive(false);
                    }
                }

                isEnabled = !isEnabled;
            }
        }
    }
}
