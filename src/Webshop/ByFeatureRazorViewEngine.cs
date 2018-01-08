using System.Web.Mvc;

namespace Webshop
{
    public class ByFeatureRazorViewEngine : RazorViewEngine
    {
        public ByFeatureRazorViewEngine()
        {
            var viewLocationFormats = new[]
            {
                "~/Features/{1}/{0}.cshtml",
                "~/Features/Shared/{0}.cshtml"
            };

            ViewLocationFormats = viewLocationFormats;
            MasterLocationFormats = viewLocationFormats;
            PartialViewLocationFormats = viewLocationFormats;
        }
    }
}