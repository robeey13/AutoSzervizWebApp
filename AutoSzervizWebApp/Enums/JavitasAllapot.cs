using System.ComponentModel.DataAnnotations;

namespace AutoSzervizWebApp.Enums
{
    public enum JavitasAllapot
    {
        [Display(Name = "Felvéve")]
        Felveve,

        [Display(Name = "Folyamatban")]
        Folyamatban,

        [Display(Name = "Kész")]
        Kesz
    }
}
