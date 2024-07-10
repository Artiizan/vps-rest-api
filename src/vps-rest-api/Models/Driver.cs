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

    [MaxLength(2)]
    private string? _number;
    public string? number
    {
        get => _number;
        set => _number = value == @"\N" ? null : value;
    }

    [MaxLength(3)]
    private string? _code;
    public string? code
    {
        get => _code;
        set => _code = value == @"\N" ? null : value;
    }

    [Required]
    [MaxLength(255)]
    public required string forename { get; set; }

    [Required]
    [MaxLength(255)]
    public required string surname { get; set; }

    [Required]
    [Column(TypeName = "Date")]
    [DataType(DataType.Date)]
    public required DateTime dob { get; set; }

    [Required]
    [MaxLength(255)]
    public required string nationality { get; set; }

    [Url]
    public string? url { get; set; }
}