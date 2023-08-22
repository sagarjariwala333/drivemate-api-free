namespace DriveMate.Requests
{
    public class UploadDocumentRequest
    {
        public Guid? Id { get; set; }
        public Guid UserId { get; set; }
        public string Name { get; set; }
        public string Type { get; set; }
        public string DocumentNo { get; set; }
        public DateTime? IssueDate { get; set; }
        public DateTime? ExpiryDate { get; set; } 
        public byte[] FileData { get; set; }
        public string? IssueAt { get; set; }
        public string? path { get; set; }
        public bool status { get; set; }
        public bool IsDeleted { get; set; }
    }
}
