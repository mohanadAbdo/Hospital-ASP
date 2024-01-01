using Microsoft.Extensions.Localization;
using System.Reflection;

namespace WebProje.Services
{
    public class LanguageService
    {
        public class SharedResource { }
        private readonly IStringLocalizer _Localizer;

        public LanguageService(IStringLocalizerFactory factory)
        {
            var type = typeof(SharedResource);
            var assemblyName = new AssemblyName(type.GetTypeInfo().Assembly.FullName);

            _Localizer = factory.Create(nameof(SharedResource), assemblyName.Name);
        }

        public LocalizedString Getkey(string key)
        {
            return _Localizer[key];
        }
    }
}
