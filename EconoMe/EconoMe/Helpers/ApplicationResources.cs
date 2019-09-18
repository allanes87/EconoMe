using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms;

namespace EconoMe.Helpers
{
    public static class ApplicationResources
    {
        public static object GetResource(string resourceKey)
        {
            if (Application.Current.Resources.TryGetValue(resourceKey, out object resourceValue))
            {
                return resourceValue;
            }

            throw new Exception($"Resource key {resourceKey} not found");
        }
    }
}
