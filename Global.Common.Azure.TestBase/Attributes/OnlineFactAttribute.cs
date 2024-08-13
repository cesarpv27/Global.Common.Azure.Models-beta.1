
namespace Global.Common.Azure.TestBase
{
    /// <summary>
    /// An attribute that is used to mark a test as an online fact. 
    /// This attribute checks a configuration setting to determine if online tests should be run.
    /// If the setting indicates that online tests should not be run, the test will be skipped with a specified message.
    /// </summary>
    public class OnlineFactAttribute : FactAttribute
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="OnlineFactAttribute"/> class.
        /// Checks the configuration to determine if online tests should be run.
        /// If online tests are not enabled in the configuration, the test is marked to be skipped.
        /// </summary>
        public OnlineFactAttribute()
        {
            var configuration = new ConfigurationBuilder()
                .AddJsonFile(SettingConstants.DefaultAppSettingsPath, optional: false, reloadOnChange: true)
                .Build();

            var runOnlineTests = configuration.GetValue<bool>("RunOnlineTests");

            if (!runOnlineTests)
                Skip = AppConstants.FactSkipMessage;
        }
    }
}
