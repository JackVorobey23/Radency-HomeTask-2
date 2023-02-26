namespace HomeTask2.DataModel
{
    public class Review
    {
        public int? Id { get; set; }
        public int? BookId { get; set; }
        public string Reviewer { get; set; }
        public string Message { get; set; }
    }
}
