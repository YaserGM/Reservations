using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Newtonsoft.Json;

namespace Reservations.App.Helper
{
    public class JsonHelper
    {
        public static string ToJson(Object obj)
        {
            if (obj == null) throw new ArgumentNullException(nameof(obj));
            var settings = new JsonSerializerSettings
            {
                ReferenceLoopHandling = ReferenceLoopHandling.Ignore,
                Formatting = Formatting.Indented
            };

            return JsonConvert.SerializeObject(obj, settings);
        }

        public static JsonResult ToJsonResult(Object obj)
        {
            var json = ToJson(obj);

            return new JsonResult
            {
                JsonRequestBehavior = JsonRequestBehavior.AllowGet,
                Data = json
            };
        }
    }
}