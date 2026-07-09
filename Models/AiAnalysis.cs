using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Docklly.Models
{
    /// <summary>
    /// Stores AI-generated analysis and suggestions for documents
    /// </summary>
    [Table("AiAnalyses")]
    public class AiAnalysis
    {
        [Key]
        public Guid AnalysisId { get; set; } = Guid.NewGuid();

        [Required]
        public Guid DocumentId { get; set; }

        [StringLength(100)]
        public string AnalysisType { get; set; } = string.Empty; // KeyTerms, RiskAssessment, ClauseSuggestion, etc.

        [Column(TypeName = "nvarchar(max)")]
        public string Content { get; set; } = string.Empty;

        [Column(TypeName = "nvarchar(max)")]
        public string? JsonResult { get; set; }

        public int Confidence { get; set; } = 0; // 0-100

        [Column("AnalyzedAt")]
        public DateTime AnalyzedAt { get; set; } = DateTime.UtcNow;

        [StringLength(50)]
        public string AiModel { get; set; } = "gpt-4"; // Model used for analysis

        // Foreign key
        [ForeignKey(nameof(DocumentId))]
        public Document? Document { get; set; }
    }
}
