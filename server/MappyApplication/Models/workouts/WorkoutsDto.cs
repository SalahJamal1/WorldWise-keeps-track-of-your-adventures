using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace MappyApplication.Models.workouts;

public class WorkoutsDto
{
    public int Id { get; set; }
    public new string Type { get; set; }
    public DateTime Date { get; set; }
    public float Distance { get; set; }
    public float Duration { get; set; }
    public float Pace { get; set; }
    public float Cadence { get; set; }
    public string Emoji { get; set; }
    public string CityName { get; set; }
    public List<double> Coords { get; set; }
}