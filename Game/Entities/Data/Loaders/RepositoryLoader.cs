﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Navislamia.Notification;
using Navislamia.Data.Interfaces;

namespace Navislamia.Data.Loaders;

public interface IRepositoryLoader
{
    public List<IRepository> Init() => null;

    public List<IRepository> Repositories { get; set; }
}

public class RepositoryLoader : IRepositoryLoader
{
    INotificationModule notificationSVC;

    public List<Task<IRepository>> Tasks = new List<Task<IRepository>>();

    public List<IRepository> Repositories { get; set; } = new List<IRepository>();

    public RepositoryLoader(INotificationModule notificationModule)
    {
        notificationSVC = notificationModule;
    }

    public bool Execute()
    {
        for (int i = 0; i < Tasks.Count; i++)
        {
            var task = Tasks[i];

            task.Wait();

            if (!task.IsCompletedSuccessfully)
            {
                notificationSVC.WriteError($"An exception occured trying to execute one more Tasks in the {this.GetType().Name}!");
                notificationSVC.WriteException(task.Exception);

                return false;
            }

            IRepository repo = task.Result;

            Repositories.Add(repo);

            notificationSVC.WriteMarkup($"[yellow]{GetType().Name}:[/] [orange3]{repo.Count}[/] rows loaded from {repo.Name}");
        }

        Tasks.Clear();

        return true;
    }
}
