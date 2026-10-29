using AutoSzervizWebApp.Data;
using AutoSzervizWebApp.DTOs;
using AutoSzervizWebApp.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

namespace AutoSzervizWebApp.Pages.JavitasPages;

public class EditModel : PageModel
{
    private readonly AutoszervizDbContext _context;

    public EditModel(AutoszervizDbContext context)
    {
        _context = context;
    }

    [BindProperty]
    public JavitasEditDTO Javitas { get; set; } = default!;

    public SelectList UgyfelekSelectList { get; set; } = default!;

    private async Task UgyfelekBetolteseAsync()
    {
        var ugyfelek = await _context.Ugyfelek.ToListAsync();
        UgyfelekSelectList = new SelectList(
            ugyfelek, "Id", "Nev", Javitas.UgyfelId);
    }

    public async Task<IActionResult> OnGetAsync(int? id)
    {
        if (id is null)
        {
            return NotFound();
        }

        var javitas = await _context.Javitasok.FirstOrDefaultAsync(m => m.Id == id);

        if (javitas is null)
        {
            return NotFound();
        }
        Javitas = new JavitasEditDTO
        {
            Id = javitas.Id,
            UgyfelId = javitas.UgyfelId,
            Rendszam = javitas.Rendszam,
            Leiras = javitas.Leiras,
            Koltseg = javitas.Koltseg
        };

        await UgyfelekBetolteseAsync();
        return Page();
    }



    // To protect from overposting attacks, enable the specific properties you want to bind to.
    // For more details, see https://aka.ms/RazorPagesCRUD.
    public async Task<IActionResult> OnPostAsync()
    {
        if (!ModelState.IsValid)
        {
            await UgyfelekBetolteseAsync();
            return Page();
        }

        Javitas modositandoJavitas = await _context.Javitasok.FindAsync(Javitas.Id);

        modositandoJavitas.UgyfelId = Javitas.UgyfelId;
        modositandoJavitas.Rendszam = Javitas.Rendszam;
        modositandoJavitas.Leiras = Javitas.Leiras;
        modositandoJavitas.Koltseg = Javitas.Koltseg;

        try
        {
            await _context.SaveChangesAsync();
        }
        catch (DbUpdateConcurrencyException)
        {
            if (!JavitasExists(Javitas.Id))
            {
                return NotFound();
            }
            else
            {
                throw;
            }
        }

        return RedirectToPage("./Index");
    }

    private bool JavitasExists(int id)
    {
        return _context.Javitasok.Any(e => e.Id == id);
    }
}
