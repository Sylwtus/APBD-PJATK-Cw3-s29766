namespace APBD_PJATK_Cw3_s29766.DTOs.Reservations;

public class ReservationDto
{
    public int Id { get; set; }
    public int RoomId { get; set; }
    public string OrganizerName { get; set; }
    public string Topic { get; set; }
    public DateTime Date { get; set; }
    public TimeSpan StartTime { get; set; }
    public TimeSpan EndTime { get; set; }
    public string Status { get; set; }
}