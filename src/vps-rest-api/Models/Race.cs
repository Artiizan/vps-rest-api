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

    public Circuit? Circuit { get; set; }
    [ForeignKey("Circuit")]
    public int? circuitId { get; set; }

    [Required]
    [MaxLength(255)]
    public required string name { get; set; }

    [Required]
    [Column(TypeName = "Date")]
    [DataType(DataType.Date)]
    public DateTime date { get; set; }

    [MaxLength(8)]
    private string? _time;
    public string? time { 
        get => _time;
        set => _time = value == @"\N" ? null : value;
    }

    [Url]
    public string? url { get; set; }

    [MaxLength(10)]
    private string? _fp1_date;
    public string? fp1_date
    {
        get => _fp1_date;
        set => _fp1_date = value == @"\N" ? null : value;
    }

    [MaxLength(8)]
    private string? _fp1_time;
    public string? fp1_time
    {
        get => _fp1_time;
        set => _fp1_time = value == @"\N" ? null : value;
    }

    [MaxLength(10)]
    private string? _fp2_date;
    public string? fp2_date
    {
        get => _fp2_date;
        set => _fp2_date = value == @"\N" ? null : value;
    }

    [MaxLength(8)]
    private string? _fp2_time;
    public string? fp2_time
    {
        get => _fp2_time;
        set => _fp2_time = value == @"\N" ? null : value;
    }

    [MaxLength(10)]
    private string? _fp3_date;
    public string? fp3_date
    {
        get => _fp3_date;
        set => _fp3_date = value == @"\N" ? null : value;
    }

    [MaxLength(8)]
    private string? _fp3_time;
    public string? fp3_time
    {
        get => _fp3_time;
        set => _fp3_time = value == @"\N" ? null : value;
    }

    [MaxLength(10)]
    private string? _quali_date;
    public string? quali_date
    {
        get => _quali_date;
        set => _quali_date = value == @"\N" ? null : value;
    }

    [MaxLength(8)]
    private string? _quali_time;
    public string? quali_time
    {
        get => _quali_time;
        set => _quali_time = value == @"\N" ? null : value;
    }

    [MaxLength(10)]
    private string? _sprint_date;
    public string? sprint_date
    {
        get => _sprint_date;
        set => _sprint_date = value == @"\N" ? null : value;
    }

    [MaxLength(8)]
    private string? _sprint_time;
    public string? sprint_time
    {
        get => _sprint_time;
        set => _sprint_time = value == @"\N" ? null : value;
    }
}