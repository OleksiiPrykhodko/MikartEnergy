namespace MikartEnergy.Common.DTO.CallbackRequest
{
    public class CallbackRequestDTO
    {
        public Guid Id { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }
        public bool InWork { get; set; }
        public bool IsDeleted { get; set; }

        public string AuthorFirstName { get; set; }
        public string AuthorLastName { get; set; }
        public string AuthorEmail { get; set; }
        public string AuthorPhone { get; set; }
        public string Message { get; set; }
        public string IntrerestedIn { get; set; }
        public int Budget { get; set; }
    }
}
