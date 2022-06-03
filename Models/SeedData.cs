using Microsoft.EntityFrameworkCore;
using ScriptureJournal.Data;

namespace ScriptureJournal.Models
{
    public static class SeedData
    {
        public static void Initialize(IServiceProvider serviceProvider)
        {
            using (var context = new ScriptureJournalContext(
                serviceProvider.GetRequiredService<
                    DbContextOptions<ScriptureJournalContext>>()))
            {
                if (context == null || context.Scripture == null)
                {
                    throw new ArgumentNullException("Null ScriptureJournalContext");
                }

                // Look for any scriptures.
                if (context.Scripture.Any())
                {
                    return;   // DB has been seeded
                }

                context.Scripture.AddRange(
                    new Scripture
                    {
                        Book = "1st Nephi",
                        Chapter = "10",
                        Verse = "5",
                        Notes = "I like this scripture",
                        DateAdded = DateTime.Parse("2010-3-21")
                    },

                    new Scripture
                    {
                        Book = "2nd Nephi",
                        Chapter = "2",
                        Verse = "2",
                        Notes = "Placeholder",
                        DateAdded = DateTime.Parse("2011-5-13")
                    },

                    new Scripture
                    {
                        Book = "Mormon",
                        Chapter = "3",
                        Verse = "10",
                        Notes = "",
                        DateAdded = DateTime.Parse("2009-2-20")
                    },

                    new Scripture
                    {
                        Book = "Mosiah",
                        Chapter = "7",
                        Verse = "10",
                        Notes = ":D",
                        DateAdded = DateTime.Parse("2015-8-3")
                    }
                );
                context.SaveChanges();
            }
        }
    }
}
