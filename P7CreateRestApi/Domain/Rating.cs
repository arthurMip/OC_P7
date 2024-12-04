using System.Text.Json.Serialization;

namespace Dot.Net.WebApi.Domain;

public class Rating
{
    public int Id { get; set; }
    public string MoodysRating { get; set; } = string.Empty;
    public string SandPRating { get; set; } = string.Empty;
    public string FitchRating { get; set; } = string.Empty;

    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public byte? OrderNumber { get; set; } 
}