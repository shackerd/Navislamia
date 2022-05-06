﻿using System;
using System.Threading.Tasks;
using System.Diagnostics;

using System.IO;
using System.Linq;
using System.Reflection;

using Configuration;
using Database;
using Navislamia.Data;
using Maps;
using Network;
using Notification;
using Scripting;

using Serilog.Events;
using System.Collections.Generic;
using Spectre.Console;
using Navislamia.World;

namespace Navislamia.Game
{
    public class GameModule : IGameService
    {
        IConfigurationService configSVC;
        IWorldService worldSVC;
        IDatabaseService dbSVC;
        IDataService dataSVC;
        IScriptingService scriptSVC;
        INotificationService notificationSVC;
        IMapService mapSVC;
        INetworkService networkSVC;

        public GameModule() { }

        public GameModule(IConfigurationService configurationService, IWorldService contentService, INotificationService notificationService, IDatabaseService databaseService, IDataService dataService, 
            IScriptingService scriptingService, IMapService mapService, INetworkService networkService)
        {
            configSVC = configurationService;
            worldSVC = contentService;
            notificationSVC = notificationService;
            dbSVC = databaseService;
            dataSVC = dataService;
            scriptSVC = scriptingService;
            mapSVC = mapService;
            networkSVC = networkService;
        }

        public int Start(string ip, int port, int backlog)
        {
            if (!configSVC.Get<bool>("skip_loading", "Scripts", false))
            {
                if (scriptSVC.Init() > 0)
                {
                    notificationSVC.WriteError("Failed to start script service!");

                    return 1;
                }

                notificationSVC.WriteSuccess(new string[] { $"Successfully started the script service!", $"[green]{scriptSVC.ScriptCount}[/] scripts loaded!" }, true);
            }
            else
                notificationSVC.WriteWarning("Script loading disabled!");

            if (!configSVC.Get<bool>("skip_loading", "Maps", false))
            {
                if (!mapSVC.Initialize($"{Directory.GetCurrentDirectory()}\\Maps"))
                {
                    notificationSVC.WriteError("Failed to start the map service!");

                    return 1;
                }

                notificationSVC.WriteSuccess(new string[] { $"Successfully started the map service!", $"[green]{mapSVC.MapCount.CX + mapSVC.MapCount.CY}[/] files loaded!" }, true);
            }
            else
                notificationSVC.WriteWarning("Map loading disabled!");

            if (dbSVC.Init() > 0)
            {
                notificationSVC.WriteError("Failed to start the database service!");

                return 1;
            }

            if (networkSVC.ConnectToAuth() > 0 || networkSVC.ConnectToUpload() > 0)
                return 1;

            return 0;
        }
    }
}
