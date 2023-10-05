namespace DriveMate.Requests
{
    public class AllUsersRequest
    {
        public string Role { get; set; }
        public string FilterOn { get; set; }
        public string FilterQuery { get; set; }
        public string SortBy { get; set; }
        public bool IsAscending { get; set; }
    }
}
