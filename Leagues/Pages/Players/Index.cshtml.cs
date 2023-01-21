using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Leagues.Data;
using Leagues.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace Leagues.Pages.Players
{
	public class IndexModel : PageModel
    {
        private readonly LeagueContext _context;

        // variables to find out if a team is selected as favorite
        public string? FavoriteTeam { get; set; }
        public string? FavoriteTeamId { get; set; }


        // variables for filtering and searching players:
        [BindProperty(SupportsGet = true)]
        public string SearchString { get; set; }
        [BindProperty(SupportsGet = true)]
        public string SelectedTeam { get; set; }
        [BindProperty(SupportsGet = true)]
        public string SelectedPosition { get; set; }
        [BindProperty(SupportsGet = true)]
        public string SortField { get; set; } = "Name";

        // store the teams and positions
        public SelectList Teams { get; set; }
        public SelectList Positions { get; set; }


        public IndexModel(LeagueContext context)
        {
            _context = context;
        }

        // get all players
        public List<Player> Players { get; set; }

        public async Task<IActionResult> OnGetAsync()
        {
            var players = from p in _context.Players
                          select p;

            if (players == null)
            {
                return NotFound();
            }
            else
            {
                // read the favorite team from a cookie

                FavoriteTeam = HttpContext.Session.GetString("_Favorite");
                FavoriteTeamId = HttpContext.Session.GetString("_FavoriteId");

           
                // make 2 select lists (teams names and player positions) for the filter dropdowns
                IQueryable<string> teamQuery = from t in _context.Teams
                                               orderby t.Name
                                               select t.Name;
                Teams = new SelectList(await teamQuery.ToListAsync());

                IQueryable<string> positionQuery = from p in _context.Players
                                                   orderby p.Position
                                                   select p.Position;
                Positions = new SelectList(await positionQuery.Distinct().ToListAsync());


                // check if there are filters or search strings:
                if (!string.IsNullOrEmpty(SearchString))
                {
                    players = players.Where(p => p.Name.Contains(SearchString));
                }

                if (!string.IsNullOrEmpty(SelectedTeam))
                {
                    var Team = _context.Teams.First(t => t.Name == SelectedTeam);
                    if (Team != null) {
                        players = players.Where(p => p.TeamId == Team.TeamId);
                     }
                }

                if (!string.IsNullOrEmpty(SelectedPosition))
                {
                    players = players.Where(p => p.Position == SelectedPosition);
                }

                // modify the query is the user is sorting
                switch (SortField)
                {
                    case "Name":
                        players = players.OrderBy(p => p.Name).ThenBy(p => p.TeamId);
                        break;
                    case "Number":
                        players = players.OrderBy(p => p.Number).ThenBy(p => p.TeamId);
                        break;
                    case "Position":
                        players = players.OrderBy(p => p.Position).ThenBy(p => p.TeamId);
                        break;
                    default:
                        players = players.OrderBy(p => p.Name).ThenBy(p => p.TeamId);
                        break;
                }



                Players = await players.ToListAsync();
                
                return Page();
            }

            
        }

        // return a string for the class of each player <a> tag, bold for starter, gold for favorite

        public string PlayerClass(Player Player)
        {
            string Class = "d-flex";
            if (Player.Depth == 1)
                Class += " starter";
            if (Player.TeamId == FavoriteTeamId)
                Class += " favorite";
            return Class;
        }
    }
}
