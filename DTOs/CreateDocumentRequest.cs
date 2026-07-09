namespace Docklly.DTOs
{
    /// <summary>
    /// DTO for creating a new document
    /// </summary>
    public class CreateDocumentRequest
    {
        public string Title { get; set; } = string.Empty;
        public string Content { get; set; } = string.Empty;
        public string DocumentType { get; set; } = string.Empty;
        public string? Tags { get; set; }
    }
}
