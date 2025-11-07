namespace CurrencyApp.Infrastructure.Configuration;
public sealed class ApiOptions
{
    public const string SectionName = "Apis";
    public string Default { get; set; } = "nbp";
    public Dictionary<string, ApiItem> Items { get; set; } = new();
    
    public sealed class ApiItem
    {
        public string? DisplayName { get; set; }
        public bool Enabled { get; set; } = true;
    }
}
