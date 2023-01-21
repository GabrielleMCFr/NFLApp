using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Leagues.Data;
using Leagues.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;

namespace Leagues.Pages.Teams
{
	public class TeamModel : PageModel
    {

        private readonly LeagueContext _context;
        public Team Team { get; set; } = default!;
        public List<Player> Players { get; set; }

        public TeamModel(LeagueContext context)
        {
            _context = context;
        }

        public async Task OnGetAsync(string Id)
        {
            Team = await _context.Teams
            .Include(t => t.Division)
            .AsNoTracking()
            .FirstOrDefaultAsync(t => t.TeamId == Id);

            Players = await _context.Players.Where(p => p.TeamId == Id).AsNoTracking().ToListAsync();

        }
    }
}
