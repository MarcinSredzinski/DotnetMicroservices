using System.ComponentModel.DataAnnotations;

namespace CommandsService.Options;

public class PlatformServiceOptions
{
    public const string PlatformService = "PlatformService";
    [Required]
    public string BaseUrl { get; set; } = String.Empty;
    [Required]
    public string ApiKey { get; set; } = String.Empty;
    [Range(1, 100, ErrorMessage = "Value for {0} must be between {1} and {2}.")]
    public int Timeout { get; set; } = 0;
}
