using APBD_PJATK_Cw3_s29766.DTOs.Reservations;
using APBD_PJATK_Cw3_s29766.Models;

namespace APBD_PJATK_Cw3_s29766.Mappers;

public static class ReservationMapper
{
    public static ReservationDto ToDto(Reservation r) => new()
    {
        Id = r.Id,
        RoomId = r.RoomId,
        OrganizerName = r.OrganizerName,
        Topic = r.Topic,
        Date = r.Date,
        StartTime = r.StartTime,
        EndTime = r.EndTime,
        Status = r.Status
    };

    public static Reservation FromCreateDto(CreateReservationDto dto) => new()
    {
        RoomId = dto.RoomId,
        OrganizerName = dto.OrganizerName,
        Topic = dto.Topic,
        Date = dto.Date,
        StartTime = dto.StartTime,
        EndTime = dto.EndTime,
        Status = dto.Status
    };

    public static void Update(Reservation r, UpdateReservationDto dto)
    {
        r.RoomId = dto.RoomId;
        r.OrganizerName = dto.OrganizerName;
        r.Topic = dto.Topic;
        r.Date = dto.Date;
        r.StartTime = dto.StartTime;
        r.EndTime = dto.EndTime;
        r.Status = dto.Status;
    }
}