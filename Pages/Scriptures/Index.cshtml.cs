using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using ScriptureJournal.Models;
using ScriptureJournal.Data;
using Microsoft.AspNetCore.Mvc.Rendering;


namespace ScriptureJournal.Pages_Scriptures
{
    public class IndexModel : PageModel
    {
        private readonly ScriptureJournalContext _context;

        public IndexModel(ScriptureJournalContext context)
        {
            _context = context;
        }


        public string BookSort { get; set; }
        public string DateSort { get; set; }
        public string CurrentSort { get; set; }

        public IList<Scripture> Scripture { get; set; } = default!;
        public IList<Scripture> ScriptureSort { get; set; } = default!;

        [BindProperty(SupportsGet = true)]
        public string SearchString { get; set; }
        public SelectList Book { get; set; }
        [BindProperty(SupportsGet = true)]
        public string ScriptureBook { get; set; }


        public async Task OnGetAsync(string sortOrder)
        {

            BookSort = String.IsNullOrEmpty(sortOrder) ? "name_desc" : "";
            DateSort = sortOrder == "Date" ? "date_desc" : "Date";

            IQueryable<Scripture> scriptureOrdering = from s in _context.Scripture
                                                      select s;

            switch (sortOrder)
            {
                case "name_desc":
                    scriptureOrdering = scriptureOrdering.OrderByDescending(s => s.Book);
                    break;
                case "Date":
                    scriptureOrdering = scriptureOrdering.OrderBy(s => s.DateAdded);
                    break;
                case "date_desc":
                    scriptureOrdering = scriptureOrdering.OrderByDescending(s => s.DateAdded);
                    break;
                default:
                   scriptureOrdering = scriptureOrdering.OrderBy(s => s.Book);
                    break;
            }


            // Use LINQ to get list of books.
            IQueryable<string> bookQuery = from b in _context.Scripture
                                           orderby b.Book
                                           select b.Book;

            var notes = from n in _context.Scripture
                        select n;

            if (!string.IsNullOrEmpty(SearchString))
            {
                notes = notes.Where(s => s.Notes.Contains(SearchString));
            }

            if (!string.IsNullOrEmpty(ScriptureBook))
            {
                notes = notes.Where(x => x.Book == ScriptureBook);
            }
            Book = new SelectList(await bookQuery.Distinct().ToListAsync());

            // This await is needed for filtering
            Scripture = await notes.ToListAsync();

            // This bottom await (when on) makes sorting work but makes the filtering await above break.
            // When this is off, the filtering works, but the sorting breaks.
            // Scripture = await scriptureOrdering.AsNoTracking().ToListAsync();
        }
    }
}
