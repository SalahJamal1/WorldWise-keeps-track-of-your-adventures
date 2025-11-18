namespace MappyApplication.Models.cities;

public class CitiesDto
{
    public int Id { get; set; }
    public string CityName { get; set; }
    public string Country { get; set; }
    public string Emoji { get; set; }
    public DateTime Date { get; set; }
    public string Notes { get; set; }
    public double Lat { get; set; }
    public double Lng { get; set; }
}