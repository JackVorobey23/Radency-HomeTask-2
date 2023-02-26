using HomeTask2.DataModel;
using Microsoft.EntityFrameworkCore;
using System.Buffers.Text;

namespace HomeTask2.DatabaseContext
{
    public class DataBaseContext:DbContext
    {
        public DataBaseContext(DbContextOptions<DataBaseContext> options):base(options)
        {
            Database.EnsureCreated();
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {

            modelBuilder.Entity<Book>().HasData
                (
                    new Book {Id = 1, Author = "Zhenyk", Content = "Beryslav detailed description", Cover = "damn", Genre = "Drama", Title = "Historical fiction" }, 
                    new Book {Id = 2, Cover="12345123",Title = "The Forgotten Garden", Content = "Family secrets revealed", Author = "Kate Morton", Genre = "Historical fiction" },
                    new Book {Id = 3, Cover="12345123",Title = "The Last Child", Content = "Race against time", Author = "John Hart", Genre = "Thriller" },
                    new Book {Id = 4, Cover="12345123",Title = "The Book of Longings", Content = "Jesus' wife story", Author = "Sue Monk Kidd", Genre = "Historical fiction" },
                    new Book {Id = 5, Cover="12345123",Title = "Red Rising", Content = "Mars uprising begins", Author = "Pierce Brown", Genre = "Historical fiction" },
                    new Book {Id = 6, Cover="12345123",Title = "The Overstory", Content = "Trees' secret lives", Author = "Richard Powers", Genre = "Historical fiction" },
                    new Book {Id = 7, Cover="12345123",Title = "The Nightingale", Content = "Sisters during WWII", Author = "Kristin Hannah", Genre = "Historical fiction" },
                    new Book {Id = 8, Cover="12345123",Title = "The Name of the Wind", Content = "Legendary wizard's tale", Author = "Patrick Rothfuss", Genre = "Fantasy" },
                    new Book {Id = 9, Cover="12345123",Title = "The Silent Patient", Content = "Locked in a mystery", Author = "Alex Michaelides", Genre = "Psychological thriller" },
                    new Book {Id = 10,Cover="12345123", Title = "The Immortal Life", Content = "Unforgettable scientific discovery", Author = "Rebecca Skloot", Genre = "Non-fiction" },
                    new Book {Id = 11, Cover = "12345123", Title = "The Martian", Content = "Stranded astronaut's survival", Author = "Andy Weir", Genre = "Science fiction" }
                );


            int ratId = 1;
            var rnd = new Random(123);
            modelBuilder.Entity<Rating>().HasData
                (
                    Enumerable.Range(1,11)
                    .SelectMany(bookId => Enumerable.Range(0,15)
                    .Select(ratingId => new Rating { Id = ratId++, BookId = bookId, Score = rnd.Next(1,6)}))
                );


            int revId = 1;
            modelBuilder.Entity<Review>().HasData
                (
                    Enumerable.Range(1,11)
                    .SelectMany(bookId => Enumerable.Range(0, rnd.Next(8,16))
                    .Select(reviewId => new Review { BookId = bookId, Id = revId++, Message = "nice book", Reviewer = $"Reviewer {revId}"}))
                );
        }
        public DbSet<Book> Books { get; set; }
    }
}
