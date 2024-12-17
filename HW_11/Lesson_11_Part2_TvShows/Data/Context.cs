using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Lesson_11_Part2_TvShows.Models;

namespace Lesson_11_Part2_TvShows.Data
{
    public class Context : DbContext
    {
        public Context (DbContextOptions<Context> options)
            : base(options)
        {
        }

        public DbSet<Lesson_11_Part2_TvShows.Models.TvShow> TvShow { get; set; } = default!;
    }
}
