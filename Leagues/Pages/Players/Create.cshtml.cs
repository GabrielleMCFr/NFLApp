using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Leagues.Data;
using Leagues.Models;

namespace Leagues.Pages.Players
{
    public class CreateModel : PageModel
    {
        private readonly Leagues.Data.LeagueContext _context;

        public CreateModel(Leagues.Data.LeagueContext context)
        {
            _context = context;
        }

        public IActionResult OnGet()
        {
            ViewData["TeamId"] = new SelectList(_context.Teams, "TeamId", "Name");

            IQueryable<string> positionQuery = from p in _context.Players
                                               orderby p.Position
                                               select p.Position;
            ViewData["Positions"] = new SelectList(positionQuery.Distinct().ToList());

            return Page();
        }

        [BindProperty]
        public Player Player { get; set; } = default!;
        

        // To protect from overposting attacks, see https://aka.ms/RazorPagesCRUD
        public async Task<IActionResult> OnPostAsync()
        {
          if (!ModelState.IsValid || _context.Players == null || Player == null)
            {
                return Page();
            }
            

            _context.Players.Add(Player);
            await _context.SaveChangesAsync();

            return RedirectToPage("./Index");
        }

        public string generateId()
        {
            return Guid.NewGuid().ToString("N");
        }
    }
}
