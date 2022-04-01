﻿using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using Autofac;

using Newtonsoft.Json;

namespace Configuration
{
    public class ConfigurationModule : Autofac.Module, IConfigurationService, IEnumerable<ConfigurationCollection>
    {
        readonly string configName = "Configuration.json";

        public ConfigurationCollection Configurations = new ConfigurationCollection();

        public int Count => Configurations.Count;

        public int TotalCount => Configurations.TotalCount;

        public void CreateDefault(string path = null)
        {
            Configurations.Clear();

            Configurations.AddParent("Server");

            // Server
            Configurations.AddChild("name", "Navislamia");
            Configurations.AddChild("index", "0");
            Configurations.AddChild("screenshort.url", "about:new");
            Configurations.AddChild("adult", false);


            Configurations.AddParent("Logs");

            // logs

            Configurations.AddChild("minimum-level", 0);
            Configurations.AddChild("packet.debug", true); // TODO: this should be false in production


            Configurations.AddParent("Network");

            // network
            Configurations.AddChild("io.auth.ip", "127.0.0.1");
            Configurations.AddChild("io.auth.port", 4502);
            Configurations.AddChild("auth.server_idx", 1);
            Configurations.AddChild("io.upload.ip", "127.0.0.1");
            Configurations.AddChild("io.upload.port", 4616);
            Configurations.AddChild("io.ip", "127.0.0.1");
            Configurations.AddChild("io.port", 4515);
            Configurations.AddChild("io.backlog", 100);
            Configurations.AddChild("io.buffer_size", 32768);
            Configurations.AddChild("cipher.key", "}h79q~B%al;k'y $E");


            // TODO: sql

            // scripts

            Configurations.AddParent("Scripts");

            Configurations.AddChild("directory", ".\\Scripts");


            // maps

            Configurations.AddParent("Maps");

            Configurations.AddChild("directory", ".\\Maps");
            Configurations.AddChild("width", 700000);
            Configurations.AddChild("height", 1000000);
            Configurations.AddChild("max_layer", 256);
            Configurations.AddChild("skip_loading", false);
            Configurations.AddChild("skip_loading_nfa", false);
            Configurations.AddChild("no_collision_check", false);

            // TODO: game

            Save();
        }

        public T Get<T>(string key, string parent, object defaultValue = null)
        {
            var config = Configurations.Find(c => c.Category == parent);

            if (config == null)
                return default(T);

            var valIdx = config.Configurations.FindIndex(c => c.Name == key);

            if (valIdx == -1)
                return default(T);

            return (T)Convert.ChangeType(config.Configurations[valIdx].Value, typeof(T));

        }

        public dynamic Get(string key, string parent, object defaultValue = null)
        {
            throw new NotImplementedException();
        }

        public void Set(string key, string parent = null, object value = null)
        {
            throw new NotImplementedException();

            // TODO: register log through subscribed log module
        }

        protected override void Load(ContainerBuilder builder)
        {
            var configServiceTypes = Directory.EnumerateFiles(Environment.CurrentDirectory)
                .Where(filename => filename.Contains("Modules") && filename.EndsWith("Configuration.dll"))
                .Select(filepath => Assembly.LoadFrom(filepath))
                .SelectMany(assembly => assembly.GetTypes()
                .Where(type => typeof(IConfigurationService).IsAssignableFrom(type) && type.IsClass));

            foreach (var configServiceType in configServiceTypes)
                builder.RegisterType(configServiceType).As<IConfigurationService>();
        }

        public bool Load(string path = null)
        {
            string filename = path ?? $"{Directory.GetCurrentDirectory()}\\{configName}";

            if (!File.Exists(filename))
            {
                // TODO: register log through subscribed log module
            }

            try
            {
                string jsonStr = File.ReadAllText(filename);

                if (!string.IsNullOrEmpty(jsonStr))
                {
                    Configurations = JsonConvert.DeserializeObject<ConfigurationCollection>(jsonStr);

                    return Configurations.Count > 0;                    
                }

                // TODO: register log through subscribed log module
            }
            catch (Exception ex)
            {
                // TODO: register log through subscribed log module
            }

            return false;
        }

        public void Save(string path = null)
        {
            string filename = path ?? $"{Directory.GetCurrentDirectory()}\\Configuration.json";

            var serializer = new JsonSerializer();

            serializer.Formatting = Formatting.Indented;

            try
            {
                using (StreamWriter sw = new StreamWriter(filename)) { 
                    using (JsonWriter jw = new JsonTextWriter(sw))
                        serializer.Serialize(jw, Configurations);
                    }
            }
            catch (Exception ex)
            {
                //LogUtility.MessageBoxAndLog(ex, "serializing provided object", "Json Serialize Exception", LogEventLevel.Error);
                return;
            }
        }

        public IEnumerator<ConfigurationCollection> GetEnumerator()
        {
            throw new NotImplementedException();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            throw new NotImplementedException();
        }
    }
}