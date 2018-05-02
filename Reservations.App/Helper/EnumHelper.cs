using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Web.Mvc;
using System.Web.Services.Description;
using Reservations.Core;

namespace Reservations.App.Helper
{
    public class EnumHelper
    {
        public static List<SelectListItem> ToLocalizationListSelectListItem<T>()
        {
            var t = typeof(T);

            if (!t.IsEnum)
            {
                throw new ApplicationException("Type to enum");
            }

            var members = t.GetFields(BindingFlags.Public | BindingFlags.Static);

            var result = new List<SelectListItem>();

            foreach (var member in members)
            {
                var attributeDescription =
                    member.GetCustomAttributes(typeof(System.ComponentModel.DescriptionAttribute), false);

                //var description = member.Name;

                var description = Localization.ResourceManager.GetString(member.Name);

                var value = ((int) Enum.Parse(t, member.Name));
                result.Add(new SelectListItem
                {
                    Text = description,
                    Value = value.ToString()
                });
            }

            return result;
        }


        public static List<SelectListItem> ToListSelectListItem<T>()
        {
            var t = typeof(T);

            if (!t.IsEnum)
            {
                throw new ApplicationException("Type to enum");
            }

            var members = t.GetFields(BindingFlags.Public | BindingFlags.Static);

            var result = new List<SelectListItem>();

            foreach (var member in members)
            {
                var attributeDescription =
                    member.GetCustomAttributes(typeof(System.ComponentModel.DescriptionAttribute), false);

                var description = member.Name;

                if (attributeDescription.Any())
                {
                    description = ((System.ComponentModel.DescriptionAttribute)attributeDescription[0]).Description;
                }

                var value = ((int)Enum.Parse(t, member.Name));
                result.Add(new SelectListItem
                {
                    Text = description,
                    Value = value.ToString()
                });
            }

            return result;
        }
    }
}