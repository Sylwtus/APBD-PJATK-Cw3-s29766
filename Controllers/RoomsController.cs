using APBD_PJATK_Cw3_s29766.DTOs.Rooms;
using APBD_PJATK_Cw3_s29766.Exceptions;
using APBD_PJATK_Cw3_s29766.Mappers;
using APBD_PJATK_Cw3_s29766.Repositories;
using Microsoft.AspNetCore.Mvc;

namespace APBD_PJATK_Cw3_s29766.Controllers;

[ApiController]
[Route("api/[controller]")]
public class RoomsController : ControllerBase
{
    private readonly IRoomRepository _rooms;
    private readonly IReservationRepository _reservations;

    public RoomsController(IRoomRepository rooms, IReservationRepository reservations)
    {
        _rooms = rooms;
        _reservations = reservations;
    }

    [HttpGet]
    public ActionResult<IEnumerable<RoomDto>> GetAll()
        => Ok(_rooms.GetAll().Select(RoomMapper.ToDto));

    [HttpGet("{id}")]
    public ActionResult<RoomDto> GetById(int id)
    {
        var room = _rooms.GetById(id);
        if (room == null)
            throw new NotFoundException("Room not found");

        return Ok(RoomMapper.ToDto(room));
    }

    [HttpGet("building/{buildingCode}")]
    public ActionResult<IEnumerable<RoomDto>> GetByBuilding(string buildingCode)
        => Ok(_rooms.GetByBuilding(buildingCode).Select(RoomMapper.ToDto));

    [HttpGet("filter")]
    public ActionResult<IEnumerable<RoomDto>> Filter(
        [FromQuery] int? minCapacity,
        [FromQuery] bool? hasProjector,
        [FromQuery] bool? activeOnly)
        => Ok(_rooms.Filter(minCapacity, hasProjector, activeOnly).Select(RoomMapper.ToDto));

    [HttpPost]
    public ActionResult<RoomDto> Create([FromBody] CreateRoomDto dto)
    {
        if (!ModelState.IsValid)
            return BadRequest(ModelState);

        var room = RoomMapper.FromCreateDto(dto);
        var created = _rooms.Add(room);

        var result = RoomMapper.ToDto(created);
        return CreatedAtAction(nameof(GetById), new { id = result.Id }, result);
    }

    [HttpPut("{id}")]
    public ActionResult<RoomDto> Update(int id, [FromBody] UpdateRoomDto dto)
    {
        if (!ModelState.IsValid)
            return BadRequest(ModelState);

        var room = _rooms.GetById(id);
        if (room == null)
            throw new NotFoundException("Room not found");

        RoomMapper.Update(room, dto);
        _rooms.Update(room);

        return Ok(RoomMapper.ToDto(room));
    }

    [HttpDelete("{id}")]
    public IActionResult Delete(int id)
    {
        var room = _rooms.GetById(id);
        if (room == null)
            throw new NotFoundException("Room not found");

        var hasReservations = _reservations.GetAll().Any(r => r.RoomId == id);
        if (hasReservations)
            throw new ConflictException("Cannot delete room with existing reservations.");

        _rooms.Delete(room);
        return NoContent();
    }
}
