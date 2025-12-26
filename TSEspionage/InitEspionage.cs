/*
 * This Source Code Form is subject to the terms of the Mozilla Public
 * License, v. 2.0. If a copy of the MPL was not distributed with this
 * file, You can obtain one at https://mozilla.org/MPL/2.0/.
 */

using HarmonyLib;
using BepInEx;
using BepInEx.Logging;
using BepInEx.Unity.IL2CPP;

namespace TSEspionage
{
    /**
     * Patches the Twilight Struggle code.
     */
    [BepInPlugin(MyPluginInfo.PLUGIN_GUID, MyPluginInfo.PLUGIN_NAME, MyPluginInfo.PLUGIN_VERSION)]
    public class InitEspionage : BasePlugin
    {
        internal static new ManualLogSource Log;

        public override void Load()
        {
            var gameLogWriter = new GameLogWriter("");
            var gameEventHandler = new GameEventHandler(gameLogWriter);
            Log = base.Log;

            // Initialize the patch classes
            GameLogPatches.Init(gameLogWriter, Log);
            LoadLevelSplashScreenPatches.Init();
            TwilightStrugglePatches.Init(gameEventHandler);
            UI_SettingsMenuPatches.Init();

            // Patch the TS assembly
            new Harmony("com.twilight-struggle.TSEspionage").PatchAll();
        }
    }
}