using APBD_PJATK_Cw3_s29766.Models;

namespace APBD_PJATK_Cw3_s29766.Repositories;

public class RoomRepository : IRoomRepository
{
    private static readonly List<Room> _rooms = new()
    {
        new Room { Id = 1, Name = "Sala A1", BuildingCode = "A", Floor = 1, Capacity = 20, HasProjector = true, IsActive = true },
        new Room { Id = 2, Name = "Sala A2", BuildingCode = "A", Floor = 1, Capacity = 30, HasProjector = false, IsActive = true },
        new Room { Id = 3, Name = "Sala B1", BuildingCode = "B", Floor = 2, Capacity = 25, HasProjector = true, IsActive = true },
        new Room { Id = 4, Name = "Sala C1", BuildingCode = "C", Floor = 0, Capacity = 15, HasProjector = false, IsActive = false }
    };

    public IEnumerable<Room> GetAll() => _rooms;

    public Room? GetById(int id)
        => _rooms.FirstOrDefault(r => r.Id == id);

    public IEnumerable<Room> GetByBuilding(string buildingCode)
        => _rooms.Where(r => r.BuildingCode == buildingCode);

    public IEnumerable<Room> Filter(int? minCapacity, bool? hasProjector, bool? activeOnly)
    {
        var query = _rooms.AsQueryable();

        if (minCapacity.HasValue)
            query = query.Where(r => r.Capacity >= minCapacity.Value);

        if (hasProjector.HasValue)
            query = query.Where(r => r.HasProjector == hasProjector.Value);

        if (activeOnly == true)
            query = query.Where(r => r.IsActive);

        return query.ToList();
    }

    public Room Add(Room room)
    {
        room.Id = _rooms.Any() ? _rooms.Max(r => r.Id) + 1 : 1;
        _rooms.Add(room);
        return room;
    }

    public Room Update(Room room)
    {
        return room;
    }

    public void Delete(Room room)
    {
        _rooms.Remove(room);
    }
}