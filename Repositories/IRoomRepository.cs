using APBD_PJATK_Cw3_s29766.Models;

namespace APBD_PJATK_Cw3_s29766.Repositories;

public interface IRoomRepository
{
    IEnumerable<Room> GetAll();
    Room? GetById(int id);
    IEnumerable<Room> GetByBuilding(string buildingCode);
    IEnumerable<Room> Filter(int? minCapacity, bool? hasProjector, bool? activeOnly);
    Room Add(Room room);
    Room Update(Room room);
    void Delete(Room room);
}