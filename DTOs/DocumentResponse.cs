namespace Docklly.DTOs
{
    /// <summary>
    /// DTO for document response
    /// </summary>
    public class DocumentResponse
    {
        public Guid DocumentId { get; set; }
        public string Title { get; set; } = string.Empty;
        public string DocumentType { get; set; } = string.Empty;
        public string Status { get; set; } = string.Empty;
        public int VersionNumber { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }
        public string? Tags { get; set; }
    }
}
