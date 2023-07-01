using System.ComponentModel.DataAnnotations;

namespace PlatformService.Models;

/// <summary>
/// Internal representation of our platform
/// </summary>
public class Platform
{
    [Key]
    [Required]
    public int Id { get; set; }
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