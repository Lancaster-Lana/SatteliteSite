using System.Collections.Specialized;
using System.Configuration;

namespace Sattelite.Framework.Configurations
{
    public interface IExConfigurationManager
    {
        object GetSection(string sectionName);

        ConnectionStringSettingsCollection GetConnectionStrings();

        NameValueCollection GetAppSettings();

        string GetAppConfigBy(string appConfigName); 
    }
}