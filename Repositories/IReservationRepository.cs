using APBD_PJATK_Cw3_s29766.Models;

namespace APBD_PJATK_Cw3_s29766.Repositories;

public interface IReservationRepository
{
    IEnumerable<Reservation> GetAll();
    Reservation? GetById(int id);
    IEnumerable<Reservation> Filter(DateTime? date, string? status, int? roomId);
    Reservation Add(Reservation reservation);
    Reservation Update(Reservation reservation);
    void Delete(Reservation reservation);
}