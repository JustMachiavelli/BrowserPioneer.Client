using System.ComponentModel.DataAnnotations;

namespace BrowserPioneer.Client.Configurations;

public class BrowserPioneerSettings
{
    [Required]
    public string BaseUrl { get; set; } = default!;
}