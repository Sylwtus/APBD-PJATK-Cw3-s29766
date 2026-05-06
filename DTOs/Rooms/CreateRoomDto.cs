using System.ComponentModel.DataAnnotations;

namespace APBD_PJATK_Cw3_s29766.DTOs.Rooms;

public class CreateRoomDto
{
    [Required]
    public string Name { get; set; }

    [Required]
    public string BuildingCode { get; set; }

    public int Floor { get; set; }

    [Range(1, int.MaxValue)]
    public int Capacity { get; set; }

    public bool HasProjector { get; set; }
    public bool IsActive { get; set; }
}