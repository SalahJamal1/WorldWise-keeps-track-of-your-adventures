using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;
using MappyApplication.Models.workouts;

namespace MappyApplication.Data;

public class Workouts
{
    public int Id { get; set; }

    public WorkoutsType Type { get; set; }

    public DateTime Date { get; set; }

    public float Distance { get; set; }
    public float Duration { get; set; }
    public float Pace { get; set; }
    public float Cadence { get; set; }
    public string Emoji { get; set; }
    public string CityName { get; set; }
    public List<double> Coords { get; set; }


    [ForeignKey(nameof(UserId))] public string? UserId { get; set; }
}