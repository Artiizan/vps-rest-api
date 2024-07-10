using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;
using System.Text.Json;

namespace Models;

[Table("DriverStandings")]
public class DriverStanding
{
    [Key, DatabaseGenerated(DatabaseGeneratedOption.None)]
    public int driverStandingsId { get; set; }

    public Race? Race { get; set; }
    [ForeignKey("Race")]
    public int? raceId { get; set; }

    public Driver? Driver { get; set; }
    [ForeignKey("Driver")]
    public int? driverId { get; set; }

    public float? points { get; set; }

    public int? position { get; set; }

    [JsonConverter(typeof(PositionTextJsonConverter))]
    public string? positionText { get; set; }

    public int? wins { get; set; }
}

// PositionText should be a string as defined in the CSV file, but for some reason it is an integer in the JSON file
// This custom converter will handle the conversion
public class PositionTextJsonConverter : JsonConverter<string>
{
    public override string Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
    {
        if (reader.TokenType == JsonTokenType.Number)
        {
            return reader.GetInt32().ToString();
        }
        else if (reader.TokenType == JsonTokenType.String)
        {
            return reader.GetString() ?? string.Empty;
        }
        throw new JsonException("Unexpected token type for positionText.");
    }

    public override void Write(Utf8JsonWriter writer, string value, JsonSerializerOptions options)
    {
        writer.WriteStringValue(value);
    }
}