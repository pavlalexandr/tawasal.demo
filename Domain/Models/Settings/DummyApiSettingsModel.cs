namespace Domain.Models.Settings
{
    public record DummyApiSettingsModel
    {
        public string BaseUri { get; init; }
        public string ApiKey { get; init; }
        public string ApiKeyHeader { get; init; }
    }
}
