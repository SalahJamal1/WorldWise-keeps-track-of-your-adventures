namespace MappyApplication.Models.cities;

public class CitiesUpdate
{
    public string CityName { get; set; }
    public string Country { get; set; }
    public string Emoji { get; set; }
    public DateTime Date { get; set; }
    public string Notes { get; set; }
    public int Lat { get; set; }
    public int Lng { get; set; }
}