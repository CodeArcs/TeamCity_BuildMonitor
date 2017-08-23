﻿using System.Configuration;
using System.IO;
using System.Net;
using System.Web.Helpers;
using System.Web.Mvc;
using BuildMonitor.Models.Home;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace BuildMonitor.Helpers
{
    public class OctopusHandler : IOctopusHandler
    {
        private dynamic json;
        public string GetJson()
        {
            return HttpGet();
        }

        public OctopusMonitorViewModel GetModel()
        {
            json = JsonConvert.DeserializeObject<dynamic>(GetJson());

            /*var a = json.Projects[0].Name;
            var b = json.bananasplit;*/
            var model = new OctopusMonitorViewModel();

            return model;
        }

        public string HttpGet()
        {
            var octopusKey = ConfigurationManager.AppSettings["octopus_api_key"];
            var octopusUrl = ConfigurationManager.AppSettings["octopus_api_url"];

            WebClient client = new WebClient();
            client.Headers.Add("X-Octopus-ApiKey", octopusKey);
            Stream data = client.OpenRead(octopusUrl);
            if (data == null) return" ";
            StreamReader reader = new StreamReader(data);
            string s = reader.ReadToEnd();
            data.Close();
            reader.Close();

            return s;
        }
    }

    public interface IOctopusHandler
    {
        string GetJson();
        OctopusMonitorViewModel GetModel();
    }
}