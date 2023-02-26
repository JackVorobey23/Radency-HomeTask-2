using System.ComponentModel.DataAnnotations;

namespace HomeTask2.DataModel
{
    public class Book
    {
        [Key]
        public int? Id { get; set; }
        public string Title { get; set; }
        public string Cover { get; set; }
        public string Content { get; set; }
        public string Author { get; set; }
        public string Genre { get; set; }
        public virtual ICollection<Review> Reviews { get; set; }
        public virtual ICollection<Rating> Ratings { get; set; }
    }
}
