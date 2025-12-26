/*
 * This Source Code Form is subject to the terms of the Mozilla Public
 * License, v. 2.0. If a copy of the MPL was not distributed with this
 * file, You can obtain one at https://mozilla.org/MPL/2.0/.
 */

using System;
using System.Collections.Generic;
using HarmonyLib;
using BepInEx.Logging;

namespace TSEspionage
{
    /**
     * Ways to export the Game Log from the UI.
     */
    public static class GameLogPatches
    {
        private static ManualLogSource _log;
        private static GameLogWriter _gameLogWriter;

        public static void Init(GameLogWriter gameLogWriter, ManualLogSource log)
        {
            _gameLogWriter = gameLogWriter;
            _log = log;
        }

        [HarmonyPatch(typeof(GameLog), nameof(GameLog.UpdateGameLog))]
        public static class UpdateGameLogPatch
        {
            public static void Postfix(GameLog __instance)
            {
                var entry_list = new List<GameLogEntry>();
                for(int i = 0; i < __instance.m_logItemList.Count; i++) {
                    entry_list.Add(new GameLogEntry {
                        name = __instance.m_logItemList[i].m_LogItemName.text,
                        desc = __instance.m_logItemList[i].m_LogItemDesc.text,
                        detail = __instance.m_logItemList[i].m_LogItemDetail.text
                    });
                }

                var entries = entry_list.ToArray();

                if (entries.Length == 0)
                {
                    return;
                }

                var gameId = TwilightLibWrapper.GetCurrentGameId();
                try
                {
                    _gameLogWriter.Write(gameId, entries);
                }
                catch (Exception e)
                {
                    _log.LogError($"Failed writing game log for game {gameId}: {e}");
                }
            }
        }
    }
}
