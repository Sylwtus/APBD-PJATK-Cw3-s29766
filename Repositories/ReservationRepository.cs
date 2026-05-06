using APBD_PJATK_Cw3_s29766.Models;

namespace APBD_PJATK_Cw3_s29766.Repositories;

public class ReservationRepository : IReservationRepository
{
    private static readonly List<Reservation> _reservations = new()
    {
        new Reservation { Id = 1, RoomId = 1, OrganizerName = "Jan Nowak", Topic = "C# Basics", Date = new DateTime(2026, 5, 10), StartTime = new TimeSpan(9,0,0), EndTime = new TimeSpan(11,0,0), Status = "confirmed" },
        new Reservation { Id = 2, RoomId = 2, OrganizerName = "Anna Kowalska", Topic = "REST API", Date = new DateTime(2026, 5, 10), StartTime = new TimeSpan(12,0,0), EndTime = new TimeSpan(14,0,0), Status = "planned" },
        new Reservation { Id = 3, RoomId = 3, OrganizerName = "Piotr Zieliński", Topic = "Docker", Date = new DateTime(2026, 5, 11), StartTime = new TimeSpan(10,0,0), EndTime = new TimeSpan(12,0,0), Status = "confirmed" }
    };

    public IEnumerable<Reservation> GetAll() => _reservations;

    public Reservation? GetById(int id)
        => _reservations.FirstOrDefault(r => r.Id == id);

    public IEnumerable<Reservation> Filter(DateTime? date, string? status, int? roomId)
    {
        var query = _reservations.AsQueryable();

        if (date.HasValue)
            query = query.Where(r => r.Date.Date == date.Value.Date);

        if (!string.IsNullOrEmpty(status))
            query = query.Where(r => r.Status == status);

        if (roomId.HasValue)
            query = query.Where(r => r.RoomId == roomId.Value);

        return query.ToList();
    }

    public Reservation Add(Reservation reservation)
    {
        reservation.Id = _reservations.Any() ? _reservations.Max(r => r.Id) + 1 : 1;
        _reservations.Add(reservation);
        return reservation;
    }

    public Reservation Update(Reservation reservation)
    {
        return reservation;
    }

    public void Delete(Reservation reservation)
    {
        _reservations.Remove(reservation);
    }
}