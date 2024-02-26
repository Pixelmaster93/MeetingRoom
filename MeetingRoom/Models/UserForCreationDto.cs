using System.ComponentModel.DataAnnotations;

namespace MeetingRoom.Models
{
    public class UserForCreationDto
    {
        [Required(ErrorMessage = "You should provide a name value!")] //Il NOME deve essere per forza presenete, nel caso uscirà l'errore di ErrorMessage!
        [MaxLength(15)] // Lunghezza masssima del nome
        public string UserName { get; set; } = string.Empty;

        [Required(ErrorMessage = "You should provide a mail addres!")]
        [MaxLength(100)] // Lunghezza massima di description consentita
        public string? MailAddres { get; set; }

        [Required(ErrorMessage = "You should provide a phone number!")]
        [MaxLength(20)] // Lunghezza massima di description consentita
        public string PhoneNumber { get; set; } = string.Empty;
    }
}
