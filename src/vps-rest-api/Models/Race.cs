using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Models;

[Table("Races")]
public class Race
{
    [Key, DatabaseGenerated(DatabaseGeneratedOption.None)]
    public int raceId { get; set; }

    [Required]
    public int year { get; set; }

    [Required]
    public int round { get; set; }

    [Required]
    [ForeignKey("Circuit")]
    public int circuitId { get; set; }

    [Required]
    [MaxLength(255)]
    public required string name { get; set; }

    [Required]
    [Column(TypeName = "date")]
    [DataType(DataType.Date)]
    public DateTime date { get; set; }

    [Required]
    [MaxLength(8)]
    public required string time { get; set; }

    [Required]
    [Url]
    public required string url { get; set; }

    [Column(TypeName = "date")]
    [DataType(DataType.Date)]
    public DateTime? fp1Date { get; set; }

    [MaxLength(8)]
    public string? fp1Time { get; set; }

    [Column(TypeName = "date")]
    [DataType(DataType.Date)]
    public DateTime? fp2Date { get; set; }

    [MaxLength(8)]
    public string? fp2Time { get; set; }

    [Column(TypeName = "date")]
    [DataType(DataType.Date)]
    public DateTime? fp3Date { get; set; }

    [MaxLength(8)]
    public string? fp3Time { get; set; }

    [Column(TypeName = "date")]
    [DataType(DataType.Date)]
    public DateTime? qualiDate { get; set; }

    [MaxLength(8)]
    public string? qualiTime { get; set; }

    [Column(TypeName = "date")]
    [DataType(DataType.Date)]
    public DateTime? sprintDate { get; set; }

    [MaxLength(8)]
    public string? sprintTime { get; set; }
}