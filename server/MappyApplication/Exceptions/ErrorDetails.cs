namespace MappyApplication.Exceptions;

public class ErrorDetails
{
    public DateTime time { get; set; } = DateTime.Now;
    public string message { get; set; }
    public string status { get; set; }
}