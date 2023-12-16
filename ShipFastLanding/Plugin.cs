using BepInEx;
using BepInEx.Configuration;
using HarmonyLib;

namespace ShipFastLanding
{
    [BepInPlugin(GUID, NAME, VERSION)]
    public class Plugin : BaseUnityPlugin
    {
        private readonly Harmony harmony = new Harmony(GUID);

        private const string GUID = "oe.tweaks.qol.faster_company_landing";
        private const string NAME = "Faster Company Landing";
        private const string VERSION = "1.0.0";

        internal static Plugin instance;

        internal static BepInEx.Logging.ManualLogSource log;

        internal static ConfigEntry<bool> config_fastTravelToAllPlanets; // Fast travel to all planets

        private void Awake()
        {
            log = this.Logger;
            log.LogInfo($"'{name}' is loading...");

            if (instance == null)
                instance = this;

            config_fastTravelToAllPlanets = Config.Bind("General", "FastTravelToAll", false, "Allows you to fast travel to all planets, not just the company.");

            harmony.PatchAll();

            log.LogInfo($"'{NAME}' loaded!");
        }
    }
}
