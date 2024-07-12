namespace Models;

public class DriverLapTimes
{
    public Circuit? Circuit { get; set; }
    public Driver? Driver { get; set; }

    public string? meanTime { get; set; }

    public float meanMilliseconds { get; set; }

    public string? fastestTime { get; set; }
    public float fastestMilliseconds { get; set; }
}