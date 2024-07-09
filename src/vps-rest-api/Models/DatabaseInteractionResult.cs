
namespace Models;

public class DatabaseInteractionResult
{
    public required bool Success { get; set; }
    public required string Message { get; set; }
    public int RowsAffected { get; set; }
}