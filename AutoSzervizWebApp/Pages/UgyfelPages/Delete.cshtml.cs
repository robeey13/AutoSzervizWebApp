using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using AutoSzervizWebApp.Models;
using AutoSzervizWebApp.Data;

namespace AutoSzervizWebApp.Pages.UgyfelPages;

public class DeleteModel : PageModel
{
    private readonly AutoszervizDbContext _context;

    public DeleteModel(AutoszervizDbContext context)
    {
        _context = context;
    }

    public Ugyfel Ugyfel { get; set; } = default!;

    public async Task<IActionResult> OnGetAsync(int? id)
    {
        if (id is null)
        {
            return NotFound();
        }

        var ugyfel = await _context.Ugyfelek.Include(u => u.Javitasok).FirstOrDefaultAsync(m => m.Id == id);
        if (ugyfel is null)
        {
            return NotFound();
        }
        else
        {
            Ugyfel = ugyfel;
        }

        return Page();
    }

    public async Task<IActionResult> OnPostAsync(int? id)
    {
        if (id is null)
        {
            return NotFound();
        }

        var ugyfel = await _context.Ugyfelek.Include(u => u.Javitasok).FirstOrDefaultAsync(m => m.Id == id);

        if (ugyfel is null)
        {
            return NotFound();
        }

        Ugyfel = ugyfel;

        var vanJavitas = await _context.Javitasok.AnyAsync(j => j.UgyfelId == id);

        if (vanJavitas)
        {
            ModelState.AddModelError(string.Empty, "Nem lehet törölni az ügyfelet, mert van hozzá kapcsolódó javítás!");
            return Page();
        }

        _context.Ugyfelek.Remove(ugyfel);
        await _context.SaveChangesAsync();

        return RedirectToPage("./Index");
    }
}
