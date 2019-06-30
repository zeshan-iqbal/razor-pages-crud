using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using RazorPagesMovie.Models;

namespace RazorPagesMovie.Pages.Movies
{
    public class IndexModel : PageModel
    {
        private readonly RazorPagesMovie.Models.RazorPagesMovieContext _context;

        public IndexModel(RazorPagesMovie.Models.RazorPagesMovieContext context)
        {
            _context = context;
        }

        public IList<Movie> Movie { get; set; }

        [BindProperty(SupportsGet = true)]
        public string SearchString { get; set; }
        public SelectList Genres { get; set; }

        [BindProperty(SupportsGet = true)]
        public string MovieGenre { get; set; }

        public async Task OnGetAsync()
        {
            var query = from m in _context.Movie select m;

            if (!string.IsNullOrWhiteSpace(SearchString))
            {
                query = query.Where(m => m.Title.Contains(SearchString));
            }

            if (!string.IsNullOrWhiteSpace(MovieGenre))
            {
                query = query.Where(m => m.Genre.Equals(MovieGenre, StringComparison.OrdinalIgnoreCase));
            }

            Movie = await query.ToListAsync();
            Genres = new SelectList(await _context.Movie.Select(m => m.Genre).Distinct().ToListAsync());
        }
    }
}
