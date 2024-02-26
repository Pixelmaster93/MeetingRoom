using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace MeetingRoom.Models
{
    public class ReservationForCreationDto
    {
        [Required(ErrorMessage = "You should provide a valid date!")] //Il NOME deve essere per forza presenete, nel caso uscirà l'errore di ErrorMessage!
        public string Date { get; set; }

        [Required(ErrorMessage = "You should provide a start of metting valid!")]
        public string StartTime { get; set; }

        [Required(ErrorMessage = "You should provide an end of metting valid!")] //Il NOME deve essere per forza presenete, nel caso uscirà l'errore di ErrorMessage!
        public string EndTime { get; set; }
    }
}
