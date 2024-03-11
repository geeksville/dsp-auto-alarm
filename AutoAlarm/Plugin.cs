using BepInEx;
using BepInEx.Configuration;
using HarmonyLib;

namespace AutoAlarm
{
    [BepInPlugin(__NAME__, __GUID__, "0.1.0")]
    public class Plugin : BaseUnityPlugin
    {
        public const string __NAME__ = "AutoAlarm";
        public const string __GUID__ = "com.geeksville.dsp." + __NAME__;

        public static ConfigEntry<bool> autoExtractors;

        private void Awake()
        {
            // Plugin startup logic
            Logger.LogInfo($"Plugin {PluginInfo.PLUGIN_GUID} is loaded!");

            autoExtractors = Config.Bind("DefaultAlarms", "autoExtractor", true,
                "Default to having alarm on");

            Harmony.CreateAndPatchAll(typeof(Plugin));
        }

        [HarmonyPostfix, HarmonyPatch(typeof(PlanetTransport), nameof(PlanetTransport.NewStationComponent))]
        public static void NewStationComponent_PostFix(PlanetTransport __instance, int _entityId, int _pcId, PrefabDesc _desc)
        {
        }
    }
}
