using System.ComponentModel.DataAnnotations;

namespace PlatformService.DTOs;

public class PlatformCreateDto
{
    [Required]
    [StringLength(100)]
    public string Name { get; set; }
    [Required]
    [StringLength(100)]
    public string Publisher { get; set; }
    [Required]
    [StringLength(40)]
    public string Cost { get; set; }
}
