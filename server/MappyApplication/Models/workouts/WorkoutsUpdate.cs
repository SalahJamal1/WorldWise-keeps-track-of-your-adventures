namespace MappyApplication.Models.workouts;

public class WorkoutsUpdate
{
    public WorkoutsType Type { get; set; }
    public DateTime Date { get; set; }
    public float Distance { get; set; }
    public float Duration { get; set; }
    public float Pace { get; set; }
    public float Cadence { get; set; }
    public string Emoji { get; set; }
    public string CityName { get; set; }
    public List<int> Coords { get; set; }
}