﻿using Any.Core.Infrastructure;
using Microsoft.AspNetCore.Builder;
using System;
using System.Collections.Generic;
using System.Text;

namespace Any.Api.Framework.Infrastructure
{
   public static class AnyCommonStartup
    {
        /// <summary>
        /// Configure the application HTTP request pipeline
        /// </summary>
        /// <param name="application">Builder for configuring an application's request pipeline</param>
        public static void ConfigureRequestPipeline(this IApplicationBuilder application)
        {
            EngineContext.Current.ConfigureRequestPipeline(application);
        }

        public static void StartEngine(this IApplicationBuilder application)
        {
            var engine = EngineContext.Current;

            ////further actions are performed only when the database is installed
            //if (DataSettingsManager.DatabaseIsInstalled)
            //{
            //    //initialize and start schedule tasks
            //    Services.Tasks.TaskManager.Instance.Initialize();
            //    Services.Tasks.TaskManager.Instance.Start();

            //    //log application start
            //    engine.Resolve<ILogger>().Information("Application started");

            //    var pluginService = engine.Resolve<IPluginService>();

            //    //install plugins
            //    pluginService.InstallPlugins();

            //    //update plugins
            //    pluginService.UpdatePlugins();
            //}
        }
    }
}
