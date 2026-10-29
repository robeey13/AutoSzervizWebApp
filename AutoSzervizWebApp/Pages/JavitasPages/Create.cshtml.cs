using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using AutoSzervizWebApp.Models;
using AutoSzervizWebApp.Data;
using AutoSzervizWebApp.Enums;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace AutoSzervizWebApp.Pages.JavitasPages;

public class CreateModel : PageModel
{
    private readonly AutoszervizDbContext _context;

    public CreateModel(AutoszervizDbContext context)
    {
        _context = context;
    }

    [BindProperty]
    public Javitas Javitas { get; set; } = new Javitas();

    public SelectList UgyfelekSelectList { get; set; } = default!;

    public async Task<IActionResult> OnGetAsync()
    {
        var ugyfelek = await _context.Ugyfelek.ToListAsync();
        UgyfelekSelectList = new SelectList(ugyfelek, "Id", "Nev", Javitas.UgyfelId);
        return Page();
    }

    public async Task<IActionResult> OnPostAsync()
    {
        if (!ModelState.IsValid)
        {
            var ugyfelek = await _context.Ugyfelek.ToListAsync();
            UgyfelekSelectList = new SelectList(ugyfelek, "Id", "Nev", Javitas.UgyfelId);
            return Page();
        }

        Javitas.Allapot = JavitasAllapot.Felveve;


        _context.Javitasok.Add(Javitas);
        await _context.SaveChangesAsync();

        return RedirectToPage("./Index");
    }
}
