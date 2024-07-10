using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Models;

[Table("LapTimes")]
public class LapTime
{
    public Race? Race { get; set; }
    [ForeignKey("Race")]
    public int? raceId { get; set; }

    public Driver? Driver { get; set; }
    [ForeignKey("Driver")]
    public int? driverId { get; set; }

    [Required]
    public int lap { get; set; }

    [Required]
    public int position { get; set; }

    [Required]
    [MaxLength(8)]
    public required string time { get; set; }

    [Required]
    public required float milliseconds { get; set; }
}