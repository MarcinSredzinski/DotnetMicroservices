using System.Text.Json.Serialization;

namespace Domain.Library.Models
{
    public record GitHubBranch(
      [property: JsonPropertyName("name")] string Name);
}
