namespace WebAppDotNet6.Models
{
    public class AppSettingsModel
    {
        public IDictionary<string, string> AppSettings { get; private set; }

        public AppSettingsModel(IDictionary<string, string> appSettings)
        {
            AppSettings = appSettings;
        }
    }
}
