using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using AutoSzervizWebApp.Models;
using AutoSzervizWebApp.Data;

namespace AutoSzervizWebApp.Pages.JavitasPages;

public class IndexModel : PageModel
{
    private readonly AutoszervizDbContext _context;

    public IndexModel(AutoszervizDbContext context)
    {
        _context = context;
    }

    public IList<Javitas> Javitas { get; set; } = new List<Javitas>();

    public async Task OnGetAsync()
    {
        Javitas = await _context.Javitasok
            .Include(j => j.Ugyfel)
            .OrderByDescending(j => j.FelvetelDatuma)
            .ToListAsync();
    }
}
