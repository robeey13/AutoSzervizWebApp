using AutoSzervizWebApp.Enums;
using System.ComponentModel.DataAnnotations;

namespace AutoSzervizWebApp.Models
{
    public class Javitas
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Az ügyfél kiválasztása kötelező!")]
        public int UgyfelId { get; set; }
        public Ugyfel? Ugyfel { get; set; }

        [Required(ErrorMessage = "A rendszám megadása kötelező!")]
        [MaxLength(20, ErrorMessage = "A rendszám nem haladhatja meg a 20 karaktert!")]

        [RegularExpression(@"^[A-Z]{3}-[0-9]{3}$",ErrorMessage = "A rendszám formátuma például: ABC-123")]
        public string Rendszam { get; set; }
        
        [MaxLength(300, ErrorMessage = "A leírás nem haladhatja meg a 300 karaktert!")]
        public string? Leiras { get; set; }
        public decimal? Koltseg { get; set; }
        public JavitasAllapot Allapot { get; set; }

        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTime FelvetelDatuma { get; set; }
    }
}
