using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Models;

[Table("Circuits")]
public class Circuit
{
    [Key, DatabaseGenerated(DatabaseGeneratedOption.None)]
    public int circuitId { get; set; }

    [Required]
    [MaxLength(255)]
    public required string circuitRef { get; set; }

    [Required]
    [MaxLength(255)]
    public required string name { get; set; }

    [Required]
    [MaxLength(255)]
    public required string location { get; set; }

    [Required]
    [MaxLength(255)]
    public required string country { get; set; }

    [Column(TypeName = "decimal(9, 6)")]
    public decimal lat { get; set; }

    [Column(TypeName = "decimal(9, 6)")]
    public decimal lng { get; set; }

    public int alt { get; set; }

    [Url]
    public string? url { get; set; }
}