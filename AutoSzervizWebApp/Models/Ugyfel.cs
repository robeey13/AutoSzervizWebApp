using System.ComponentModel.DataAnnotations;

namespace AutoSzervizWebApp.Models
{
    public class Ugyfel
    {
        public int Id { get; set; }

        [MaxLength(100, ErrorMessage = "A név nem haladhatja meg a 100 karaktert!")]
        [Required(ErrorMessage = "A név megadása kötelező!")]
        public string Nev { get; set; }

        [MaxLength(20, ErrorMessage = "A telefonszám nem lehet hosszabb 20 karakternél!")]
        public string? Telefon { get; set; }

        [EmailAddress(ErrorMessage = "Érvénytelen email cím!")]
        public string? Email { get; set; }

        public ICollection<Javitas> Javitasok { get; set; } = new List<Javitas>();
    }
}
