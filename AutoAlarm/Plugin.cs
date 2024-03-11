using BepInEx;

namespace AutoAlarm
{
    [BepInPlugin(__NAME__, __GUID__, "1.0.0")]
    public class Plugin : BaseUnityPlugin
    {
        public const string __NAME__ = "AutoAlarm";
        public const string __GUID__ = "com.geeksville.dsp." + __NAME__;

        private void Awake()
        {
            // Plugin startup logic
            Logger.LogInfo($"Plugin {PluginInfo.PLUGIN_GUID} is loaded!");
        }
    }
}
