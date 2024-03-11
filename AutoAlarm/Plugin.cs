using BepInEx;
using BepInEx.Configuration;
using BepInEx.Logging;
using HarmonyLib;

namespace AutoAlarm
{
    [BepInPlugin(__NAME__, __GUID__, "0.1.0")]
    public class Plugin : BaseUnityPlugin
    {
        public const string __NAME__ = "AutoAlarm";
        public const string __GUID__ = "com.geeksville.dsp." + __NAME__;

        public static ConfigEntry<bool> autoExtractors;

        private static ManualLogSource logger = null;

        private void Awake()
        {
            // Plugin startup logic
            Logger.LogInfo($"Plugin {PluginInfo.PLUGIN_GUID} is loaded!");
            logger = Logger;

            autoExtractors = Config.Bind("DefaultAlarms", "autoExtractor", true,
                "Default to having alarm on");

            Harmony.CreateAndPatchAll(typeof(Plugin));
        }

        [HarmonyPostfix, HarmonyPatch(typeof(PlanetFactory), nameof(PlanetFactory.BuildFinally))]
        public static void BuildFinally_PostFix(PlanetFactory __instance, Player player, int prebuildId, bool autoRefresh = true, bool flattenTerrain = true)
        {
            var prebuildData = __instance.GetPrebuildData(prebuildId);
            logger.LogInfo($"BuildFinally postfix {prebuildId} data: {prebuildData}");


            // how to call PrebuildData GetPrebuildData(int id)? (I assume the tasty looking data inside PrebuildData will 
            // let me find the just created miner/assembler/missle-launcher/whatever)
            // i.e. how do I find 'this' for the patched PlanetFactory object?  Does Harmony have a way to pass that in
            // or is there some other recommended best practice?
        }

        // NOTE: this is not called if you shift-click to copy an existing building
        [HarmonyPostfix, HarmonyPatch(typeof(GameScenarioLogic), nameof(GameScenarioLogic.NotifyOnBuild))]
        public static void OnBuild_PostFix(int planetId, int itemId, int entityId)
        {
            logger.LogInfo($"OnBuild_PostFix {planetId}/{itemId}/{entityId}");
            var planet = GameMain.localPlanet;
            if (planet == null)
            {
                logger.LogError("No local planet!");
                return;
            }
            var factory = planet.factory;
            var entity = factory.entityPool[entityId];
            if(entity.assemblerId != 0) {
                logger.LogInfo($"Found an assembler {entity.assemblerId}");
                factory.EnableEntityWarning(entityId);
            }
        }

        // NOTE: this is not called if you shift-click to copy an existing building
        [HarmonyPostfix, HarmonyPatch(typeof(PlanetFactory), nameof(PlanetFactory.CopyBuildingSetting))]
        public static void CopyBuilding_PostFix(int objectId)
        {
            logger.LogInfo($"CopyBuilding_PostFix postfix {objectId}");
        }

        /*
        // this is called before buildfinally
        [HarmonyPostfix, HarmonyPatch(typeof(PlanetFactory), nameof(PlanetFactory.AddEntityData))]
        public static void AddEntityData_PostFix(EntityData entity)
        {
            logger.LogInfo($"AddEntityData_PostFix postfix {entity}");
        } */

        [HarmonyPostfix, HarmonyPatch(typeof(PlanetFactory), nameof(PlanetFactory.EnableEntityWarning))]
        public static void EnableEntityWarning_PostFix(int entityId)
        {
            logger.LogInfo($"EnableEntityWarning_PostFix postfix {entityId}");
        }        
    }
}
