using APBD_PJATK_Cw3_s29766.DTOs.Rooms;
using APBD_PJATK_Cw3_s29766.Models;

namespace APBD_PJATK_Cw3_s29766.Mappers;

public static class RoomMapper
{
    public static RoomDto ToDto(Room room) => new()
    {
        Id = room.Id,
        Name = room.Name,
        BuildingCode = room.BuildingCode,
        Floor = room.Floor,
        Capacity = room.Capacity,
        HasProjector = room.HasProjector,
        IsActive = room.IsActive
    };

    public static Room FromCreateDto(CreateRoomDto dto) => new()
    {
        Name = dto.Name,
        BuildingCode = dto.BuildingCode,
        Floor = dto.Floor,
        Capacity = dto.Capacity,
        HasProjector = dto.HasProjector,
        IsActive = dto.IsActive
    };

    public static void Update(Room room, UpdateRoomDto dto)
    {
        room.Name = dto.Name;
        room.BuildingCode = dto.BuildingCode;
        room.Floor = dto.Floor;
        room.Capacity = dto.Capacity;
        room.HasProjector = dto.HasProjector;
        room.IsActive = dto.IsActive;
    }
}