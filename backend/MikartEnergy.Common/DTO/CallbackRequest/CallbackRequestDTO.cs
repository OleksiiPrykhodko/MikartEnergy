namespace MikartEnergy.Common.DTO.CallbackRequest
{
    public class CallbackRequestDTO
    {
        public Guid Id { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }
        public bool InWork { get; set; }
        public bool IsDeleted { get; set; }

        public string AuthorFirstName { get; set; } = string.Empty;
        public string AuthorLastName { get; set; } = string.Empty;
        public string AuthorEmail { get; set; } = string.Empty;
        public string AuthorPhone { get; set; } = string.Empty;
        public string Message { get; set; } = string.Empty;
        public string IntrerestedIn { get; set; } = string.Empty;
        public int Budget { get; set; }
    }
}
