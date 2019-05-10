namespace Linn.Authorisation.Service.Host.Negotiators
{
    using System.Collections.Generic;
    using System.IO;

    public class ViewLoader : IViewLoader
    {
        private readonly Dictionary<string, string> loadedViews = new Dictionary<string, string>();
        private static readonly object key = new object();

        public string Load(string viewName)
        {
            lock (key)
            {
                if (!this.loadedViews.ContainsKey(viewName))
                {
                    var viewPath = $"./Views/{viewName}";

                    if (!File.Exists(viewPath))
                    {
                        return null;
                    }

                    var view = File.ReadAllText(viewPath);
                    this.loadedViews.Add(viewName, view);
                }

                return this.loadedViews[viewName];
            }
        }
    }
}
