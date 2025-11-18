using System.ComponentModel.DataAnnotations.Schema;

namespace MappyApplication.Data;

public class Token
{
    public int Id { get; set; }

    public string AccessToken { get; set; }

    public string RefreshToken { get; set; }

    public bool Revoked { get; set; } = false;
    public bool Expired { get; set; } = false;
    public DateTime CreatedAt { get; set; } = DateTime.Now;
    public string DeviceId { get; set; }
    [ForeignKey(nameof(UserId))] public string UserId { get; set; }
}