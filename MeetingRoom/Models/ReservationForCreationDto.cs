using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace MeetingRoom.Models
{
    public class ReservationForCreationDto
    {
        [Required(ErrorMessage = "You should provide a valid date!")]
        public string Date { get; set; }

        [Required(ErrorMessage = "You should provide a start of metting valid!")]
        public string StartTime { get; set; }

        [Required(ErrorMessage = "You should provide an end of metting valid!")]
        public string EndTime { get; set; }
    }
}
