namespace FinanceApp.Models;

public class ApiInfoOptions
{
    public const string SectionName = "ApiInfo";

    public string Name { get; set; } = string.Empty;
    public string Version { get; set; } = string.Empty;
    public string Environment { get; set; } = string.Empty;
}