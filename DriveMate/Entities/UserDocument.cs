namespace DriveMate.Entities
{
    public class UserDocument : BaseClass
    {
        public Guid UserId { get; set; }
        public string Name { get; set; }
        public string Type { get; set; }
        public string DocumentNo { get; set; }
        public DateTime IssueDate { get; set; } = DateTime.Now;
        public DateTime ExpiryDate { get; set; } = DateTime.Now;
        public string IssueAt { get; set; } = "";
        public string Path { get; set; } = "";
        public byte[] FileDate { get; set; }
        public Boolean status { get; set; }
    }
}
