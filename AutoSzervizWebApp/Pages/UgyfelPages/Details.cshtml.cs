using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using AutoSzervizWebApp.Models;
using AutoSzervizWebApp.Data;

namespace AutoSzervizWebApp.Pages.UgyfelPages;

public class DetailsModel : PageModel
{
    private readonly AutoszervizDbContext _context;
    public DetailsModel(AutoszervizDbContext context)
    {
        _context = context;
    }

    public Ugyfel Ugyfel { get; set; } = default!;

    public decimal Osszkoltseg =>
        Ugyfel.Javitasok.Sum(j => j.Koltseg ?? 0m);

    public async Task<IActionResult> OnGetAsync(int? id)
    {
        if (id is null)
        {
            return NotFound();
        }

        var ugyfel = await _context.Ugyfelek
            .Include(u => u.Javitasok)
            .FirstOrDefaultAsync(u => u.Id == id);

        if (ugyfel is null)
        {
            return NotFound();
        }

        Ugyfel = ugyfel;
        return Page();
    }
}
