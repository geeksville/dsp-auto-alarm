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

        public static ConfigEntry<bool> autoMiners, autoAssemblers, autoTurrets, autoBABs,
        autoLabs, autoStations, autoFieldGens,
        autoConstructionModules;

        private static ManualLogSource logger = null;

        private void Awake()
        {
            // Plugin startup logic
            Logger.LogInfo($"Plugin {PluginInfo.PLUGIN_GUID} is loaded!");
            logger = Logger;

            autoMiners = Config.Bind("Defaults", "autoMiners", true,
                "Default to alarm-on for miners");
            autoAssemblers = Config.Bind("Defaults", "autoAssemblers", false,
                "Default to alarm-on for assemblers");
            autoTurrets = Config.Bind("Defaults", "autoTurrets", true,
                "Default to alarm-on for turrets");
            autoBABs = Config.Bind("Defaults", "autoBABs", true,
                "Default to alarm-on for BABs");
            autoLabs = Config.Bind("Defaults", "autoLabs", false,
                "Default to alarm-on for labs");
            autoStations = Config.Bind("Defaults", "autoStations", true,
                "Default to alarm-on for stations");
            autoFieldGens = Config.Bind("Defaults", "autoFieldGens", true,
                "Default to alarm-on for field generators");
            autoConstructionModules = Config.Bind("Defaults", "autoSmelters", true,
                "Default to alarm-on for smelters"); // these are smelters

            Harmony.CreateAndPatchAll(typeof(Plugin));
        }

        /// <summary>
        /// Sets alarms for the specified entity based on its type and user settings.
        /// </summary>
        /// <param name="entityId">The ID of the entity to set alarms for.</param>
        private static void SetAlarms(int entityId)
        {
            logger.LogInfo($"SetAlarms {entityId}");
            var planet = GameMain.localPlanet;
            if (planet == null)
            {
                logger.LogError("No local planet!");
                return;
            }
            var factory = planet.factory;
            var entity = factory.entityPool[entityId];
            var setId = 0;

            if (entity.assemblerId != 0 && autoAssemblers.Value)
                setId = entity.assemblerId;
            if (entity.minerId != 0 && autoMiners.Value)
                setId = entity.minerId;
            if (entity.turretId != 0 && autoTurrets.Value)
                setId = entity.turretId;
            if (entity.battleBaseId != 0 && autoBABs.Value)
                setId = entity.battleBaseId;
            if (entity.labId != 0 && autoLabs.Value)
                setId = entity.labId;
            if (entity.stationId != 0 && autoStations.Value)
                setId = entity.stationId;
            if (entity.fieldGenId != 0 && autoFieldGens.Value)
                setId = entity.fieldGenId;
            if (entity.constructionModuleId != 0 && autoConstructionModules.Value)
                setId = entity.constructionModuleId;

            if (setId != 0)
            {
                logger.LogInfo($"Auto alarm enable for {setId}");
                factory.EnableEntityWarning(entityId);
            }
            else
            {
                logger.LogDebug($"No auto alarm for {entityId}");
            }
        }



        /// <summary>
        /// Hooks into game build events to set alarms for newly built entities.
        /// </summary>
        /// <param name="entityId">The ID of the entity that was built.</param>
        [HarmonyPostfix, HarmonyPatch(typeof(GameScenarioLogic), nameof(GameScenarioLogic.NotifyOnBuild))]
        public static void OnBuild_PostFix(int entityId) // int planetId, int itemId
        {
            // logger.LogInfo($"OnBuild_PostFix {entityId}");
            SetAlarms(entityId);
        }

        // The following experiments in patching are not used / will be deleted eventually

        /*
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
        [HarmonyPostfix, HarmonyPatch(typeof(PlanetFactory), nameof(PlanetFactory.CopyBuildingSetting))]
        public static void CopyBuilding_PostFix(int objectId)
        {
            logger.LogInfo($"CopyBuilding_PostFix postfix {objectId}");
        } */

        /*
        // this is called before buildfinally
        [HarmonyPostfix, HarmonyPatch(typeof(PlanetFactory), nameof(PlanetFactory.AddEntityData))]
        public static void AddEntityData_PostFix(EntityData entity)
        {
            logger.LogInfo($"AddEntityData_PostFix postfix {entity}");
        } */

        /*
        [HarmonyPostfix, HarmonyPatch(typeof(PlanetFactory), nameof(PlanetFactory.EnableEntityWarning))]
        public static void EnableEntityWarning_PostFix(int entityId)
        {
            logger.LogInfo($"EnableEntityWarning_PostFix postfix {entityId}");
        }
        */
    }
}
