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

    [BindProperty]
    public Ugyfel Ugyfel { get; set; } = default!;

    public async Task<IActionResult> OnGetAsync(int? id)
    {
        if (id is null)
        {
            return NotFound();
        }

        var ugyfel = await _context.Ugyfelek.FirstOrDefaultAsync(m => m.Id == id);
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

        var ugyfel = await _context.Ugyfelek.FindAsync(id);
        var vanJavitas = await _context.Javitasok.AnyAsync(j => j.UgyfelId == id);

        if (ugyfel != null && !vanJavitas)
        {
            Ugyfel = ugyfel;
            _context.Ugyfelek.Remove(Ugyfel);
            await _context.SaveChangesAsync();
        }
        if (vanJavitas)
        {
            ModelState.AddModelError(string.Empty, "Nem lehet törölni az ügyfelet, mert van hozzá kapcsolódó javítás!");
            return Page();
        }

        return RedirectToPage("./Index");
    }
}
