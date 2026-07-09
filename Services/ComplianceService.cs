using Docklly.Database;
using Docklly.Models;
using Microsoft.EntityFrameworkCore;

namespace Docklly.Services
{
    /// <summary>
    /// Service for compliance checking and regulatory validation
    /// </summary>
    public interface IComplianceService
    {
        Task<ComplianceCheck> CheckComplianceAsync(Guid documentId, string ruleType);
        Task<List<ComplianceCheck>> GetComplianceChecksByDocumentAsync(Guid documentId);
        Task<int> CalculateComplianceScoreAsync(Guid documentId);
    }

    public class ComplianceService : IComplianceService
    {
        private readonly AppDbContext _context;
        private readonly ILogger<ComplianceService> _logger;

        public ComplianceService(AppDbContext context, ILogger<ComplianceService> logger)
        {
            _context = context;
            _logger = logger;
        }

        public async Task<ComplianceCheck> CheckComplianceAsync(Guid documentId, string ruleType)
        {
            try
            {
                var document = await _context.Documents.FindAsync(documentId);
                if (document == null)
                    throw new Exception("Document not found");

                var complianceCheck = new ComplianceCheck
                {
                    DocumentId = documentId,
                    RuleType = ruleType,
                    Status = PerformComplianceCheck(document.Content, ruleType),
                    ComplianceScore = CalculateComplianceScore(document.Content, ruleType),
                    CheckedAt = DateTime.UtcNow
                };

                _context.ComplianceChecks.Add(complianceCheck);
                await _context.SaveChangesAsync();
                _logger.LogInformation("Compliance check completed for document {DocumentId}", documentId);
                return complianceCheck;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error checking compliance for document: {DocumentId}", documentId);
                throw;
            }
        }

        public async Task<List<ComplianceCheck>> GetComplianceChecksByDocumentAsync(Guid documentId)
        {
            try
            {
                return await _context.ComplianceChecks
                    .Where(c => c.DocumentId == documentId)
                    .OrderByDescending(c => c.CheckedAt)
                    .ToListAsync();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error fetching compliance checks for document: {DocumentId}", documentId);
                throw;
            }
        }

        public async Task<int> CalculateComplianceScoreAsync(Guid documentId)
        {
            try
            {
                var checks = await _context.ComplianceChecks
                    .Where(c => c.DocumentId == documentId)
                    .ToListAsync();

                if (checks.Count == 0)
                    return 0;

                return (int)checks.Average(c => c.ComplianceScore);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error calculating compliance score for document: {DocumentId}", documentId);
                throw;
            }
        }

        private string PerformComplianceCheck(string content, string ruleType)
        {
            // Implement rule-specific compliance checks
            return ruleType switch
            {
                "GDPR" => CheckGdprCompliance(content),
                "HIPAA" => CheckHipaaCompliance(content),
                "SOX" => CheckSoxCompliance(content),
                _ => "Unknown"
            };
        }

        private int CalculateComplianceScore(string content, string ruleType)
        {
            // Placeholder scoring
            return content.Length > 100 ? 85 : 50;
        }

        private string CheckGdprCompliance(string content)
        {
            if (content.Contains("GDPR", StringComparison.OrdinalIgnoreCase) ||
                content.Contains("data protection", StringComparison.OrdinalIgnoreCase))
                return "Passed";
            return "Warning";
        }

        private string CheckHipaaCompliance(string content)
        {
            if (content.Contains("protected health information", StringComparison.OrdinalIgnoreCase) ||
                content.Contains("PHI", StringComparison.OrdinalIgnoreCase))
                return "Passed";
            return "Warning";
        }

        private string CheckSoxCompliance(string content)
        {
            if (content.Contains("financial", StringComparison.OrdinalIgnoreCase) ||
                content.Contains("audit", StringComparison.OrdinalIgnoreCase))
                return "Passed";
            return "Warning";
        }
    }
}
