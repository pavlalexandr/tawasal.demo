namespace Domain.Models.Settings
{
    public class ApplicationSettingsModel
    {
        public string Login { get; set; }
        public string Password { get; set; }
        public int LocalStorageCacheTimeoutInMinutes { get; set; }
        public DummyApiSettingsModel DummyApiSettings { get; init; }
    }
}
