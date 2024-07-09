using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Models;

[Table("LapTimes")]
public class LapTime
{
    [ForeignKey("Race")]
    public int raceId { get; set; }

    [ForeignKey("Driver")]
    public int driverId { get; set; }

    public int lap { get; set; }

    public int position { get; set; }

    [Required]
    [MaxLength(15)]
    public required string time { get; set; }

    public float milliseconds { get; set; }
}