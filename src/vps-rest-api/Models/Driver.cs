using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Models;

[Table("Drivers")]
public class Driver
{
    [Key, DatabaseGenerated(DatabaseGeneratedOption.None)]
    public int driverId { get; set; }

    [Required]
    [MaxLength(255)]
    public required string driverRef { get; set; }

    [Required]
    [MaxLength(10)]
    public required string number { get; set; }

    [Required]
    [MaxLength(10)]
    public required string code { get; set; }

    [Required]
    [MaxLength(255)]
    public required string forename { get; set; }

    [Required]
    [MaxLength(255)]
    public required string surname { get; set; }

    [Required]
    [Column(TypeName = "date")]
    [DataType(DataType.Date)]
    public required DateTime dob { get; set; }

    [Required]
    [MaxLength(255)]
    public required string nationality { get; set; }

    [Url]
    public string? url { get; set; }
}