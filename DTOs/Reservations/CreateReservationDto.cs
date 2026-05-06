using System.ComponentModel.DataAnnotations;

namespace APBD_PJATK_Cw3_s29766.DTOs.Reservations;

public class CreateReservationDto
{
    [Required]
    public int RoomId { get; set; }

    [Required]
    public string OrganizerName { get; set; }

    [Required]
    public string Topic { get; set; }

    [Required]
    public DateTime Date { get; set; }

    [Required]
    public TimeSpan StartTime { get; set; }

    [Required]
    public TimeSpan EndTime { get; set; }

    [Required]
    public string Status { get; set; }
}