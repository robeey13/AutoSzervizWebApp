using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using AutoSzervizWebApp.Models;
using AutoSzervizWebApp.Data;
using AutoSzervizWebApp.DTOs;

namespace AutoSzervizWebApp.Pages.UgyfelPages;

public class CreateModel : PageModel
{
    private readonly AutoszervizDbContext _context;

    public CreateModel(AutoszervizDbContext context)
    {
        _context = context;
    }

    public IActionResult OnGet()
    {
        return Page();
    }

    [BindProperty]
    public UgyfelPostDTO Ugyfel { get; set; } = default!;

    // To protect from overposting attacks, see https://aka.ms/RazorPagesCRUD.
    public async Task<IActionResult> OnPostAsync()
    {
        if (!ModelState.IsValid)
        {
            return Page();
        }

        var UjUgyfel = new Ugyfel
        {
            Nev = Ugyfel.Nev,
            Telefon = Ugyfel.Telefon,
            Email = Ugyfel.Email
        };

        _context.Ugyfelek.Add(UjUgyfel);
        await _context.SaveChangesAsync();

        return RedirectToPage("./Index");
    }
}
