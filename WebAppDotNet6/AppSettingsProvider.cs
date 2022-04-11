namespace WebAppDotNet6
{
    public class AppSettingsProvider
    {
        private readonly IConfiguration _configuration;

        public AppSettingsProvider(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public IDictionary<string, string> GetAppSettings()
        {
            Dictionary<string, string> appSettings = new();         

            // Read the existig configuration if any
            for (int i = 1; i <= 100; ++i)
            {
                // read the heirarchical configs
                string hierarchyConfigKey = $"CustomAppSettings:key_{i}";
                var appSettingVal = _configuration[hierarchyConfigKey];
                if (!string.IsNullOrEmpty(appSettingVal))
                {
                    appSettings.Add(hierarchyConfigKey, appSettingVal);
                }

                string key = $"APP_SETTING_KEY_{i}";
                appSettingVal = _configuration[key];
                if (!string.IsNullOrEmpty(appSettingVal))
                {
                    appSettings.Add(key, appSettingVal);
                }
            }
           
            var connectionStringVal = _configuration.GetConnectionString("SQLConnStrKey");

            if (connectionStringVal != null)
            {
                appSettings.Add("SQLConnStrKey", connectionStringVal);
            }

            // Sort the appSettings dictionary on key, so that all APP_SETTING_KEY and CustomAppSettings key values are together
            IDictionary<string, string> orderedByKey = appSettings.OrderBy(i => i.Key).ToDictionary(x => x.Key, x => x.Value);

            return orderedByKey;
        }
    }
}
