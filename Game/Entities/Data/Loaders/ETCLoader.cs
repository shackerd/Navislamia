﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Navislamia.Database;
using Navislamia.Notification;
using Navislamia.Data.Interfaces;
using Navislamia.Data.Repositories;
using System.Data;

namespace Navislamia.Data.Loaders;

public class ETCLoader : RepositoryLoader, IRepositoryLoader
{
    DbConnectionManager _dbConnectionManager;

    public ETCLoader(INotificationModule notificationModule, DbConnectionManager dbConnectionManager) : base(notificationModule)
    {
        _dbConnectionManager = dbConnectionManager;
    }

    public List<IRepository> Init()
    {

        // TODO: MarketData
        Tasks.Add(Task.Run(() => new LevelExpRepository(_dbConnectionManager.WorldConnection).Load()));
        // TODO: SummonLevelUpTable
        // TODO: Job
        // TODO: State
        // TODO: BannedWord
        // TODO: Enhance
        // TODO: AutoAccount
        // TODO: GlobalVariable

        if (!Execute())
            return null;

        return Repositories;
    }
}
