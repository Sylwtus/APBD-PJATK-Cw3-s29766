using APBD_PJATK_Cw3_s29766.DTOs.Reservations;
using APBD_PJATK_Cw3_s29766.Exceptions;
using APBD_PJATK_Cw3_s29766.Mappers;
using APBD_PJATK_Cw3_s29766.Repositories;
using Microsoft.AspNetCore.Mvc;

namespace APBD_PJATK_Cw3_s29766.Controllers;

[ApiController]
[Route("api/[controller]")]
public class ReservationsController : ControllerBase
{
    private readonly IReservationRepository _reservations;
    private readonly IRoomRepository _rooms;

    public ReservationsController(IReservationRepository reservations, IRoomRepository rooms)
    {
        _reservations = reservations;
        _rooms = rooms;
    }

    [HttpGet]
    public ActionResult<IEnumerable<ReservationDto>> GetAll()
        => Ok(_reservations.GetAll().Select(ReservationMapper.ToDto));

    [HttpGet("{id}")]
    public ActionResult<ReservationDto> GetById(int id)
    {
        var res = _reservations.GetById(id);
        if (res == null)
            throw new NotFoundException("Reservation not found");

        return Ok(ReservationMapper.ToDto(res));
    }

    [HttpGet("filter")]
    public ActionResult<IEnumerable<ReservationDto>> Filter(
        [FromQuery] DateTime? date,
        [FromQuery] string? status,
        [FromQuery] int? roomId)
        => Ok(_reservations.Filter(date, status, roomId).Select(ReservationMapper.ToDto));

    [HttpPost]
    public ActionResult<ReservationDto> Create([FromBody] CreateReservationDto dto)
    {
        if (!ModelState.IsValid)
            return BadRequest(ModelState);

        var room = _rooms.GetById(dto.RoomId);
        if (room == null)
            throw new NotFoundException("Room does not exist.");

        if (!room.IsActive)
            throw new ConflictException("Room is inactive.");

        if (dto.EndTime <= dto.StartTime)
            return BadRequest("EndTime must be later than StartTime.");

        var sameDayReservations = _reservations.Filter(dto.Date, null, dto.RoomId);
        var conflict = sameDayReservations.Any(r =>
            r.StartTime < dto.EndTime &&
            dto.StartTime < r.EndTime);

        if (conflict)
            throw new ConflictException("Time conflict with another reservation.");

        var reservation = ReservationMapper.FromCreateDto(dto);
        var created = _reservations.Add(reservation);

        var result = ReservationMapper.ToDto(created);
        return CreatedAtAction(nameof(GetById), new { id = result.Id }, result);
    }

    [HttpPut("{id}")]
    public ActionResult<ReservationDto> Update(int id, [FromBody] UpdateReservationDto dto)
    {
        if (!ModelState.IsValid)
            return BadRequest(ModelState);

        var res = _reservations.GetById(id);
        if (res == null)
            throw new NotFoundException("Reservation not found");

        ReservationMapper.Update(res, dto);
        _reservations.Update(res);

        return Ok(ReservationMapper.ToDto(res));
    }

    [HttpDelete("{id}")]
    public IActionResult Delete(int id)
    {
        var res = _reservations.GetById(id);
        if (res == null)
            throw new NotFoundException("Reservation not found");

        _reservations.Delete(res);
        return NoContent();
    }
}
