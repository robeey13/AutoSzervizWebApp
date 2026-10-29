using AutoSzervizWebApp.Data;
using AutoSzervizWebApp.Enums;
using AutoSzervizWebApp.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;

namespace AutoSzervizWebApp.Pages.JavitasPages;

public class IndexModel : PageModel
{
    private readonly AutoszervizDbContext _context;

    public IndexModel(AutoszervizDbContext context)
    {
        _context = context;
    }

    public IList<Javitas> Javitas { get; set; } = new List<Javitas>();

    [BindProperty(SupportsGet = true)]
    public JavitasAllapot? AllapotSzuro { get; set; }

    public async Task OnGetAsync()
    {
        IQueryable<Javitas> query = _context.Javitasok.Include(j => j.Ugyfel);

        if (AllapotSzuro.HasValue)
        {
            query = query.Where(j => j.Allapot == AllapotSzuro.Value);
        }

        Javitas = await query.OrderByDescending(j => j.FelvetelDatuma).ToListAsync();
    }

    [TempData]
    public string? Hiba { get; set; }

    public async Task<IActionResult> OnPostAllapotAsync(int id)
    {
        var javitas = await _context.Javitasok.FindAsync(id);

        if (javitas is null)
        {
            return NotFound();
        }

        if (javitas.Allapot == JavitasAllapot.Folyamatban &&
            (javitas.Koltseg is null || javitas.Koltseg <= 0))
        {
            Hiba = "A javítást csak 0-nál nagyobb költséggel lehet készre állítani.";
            return RedirectToPage();
        }

        javitas.Allapot = javitas.Allapot switch
        {
            JavitasAllapot.Felveve => JavitasAllapot.Folyamatban,
            JavitasAllapot.Folyamatban => JavitasAllapot.Kesz,
            JavitasAllapot.Kesz => JavitasAllapot.Kesz,
            _ => javitas.Allapot
        };

        await _context.SaveChangesAsync();

        return RedirectToPage();
    }
}
