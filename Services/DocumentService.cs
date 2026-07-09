using Docklly.Database;
using Docklly.Models;
using Microsoft.EntityFrameworkCore;

namespace Docklly.Services
{
    /// <summary>
    /// Service for managing legal documents
    /// </summary>
    public interface IDocumentService
    {
        Task<Document> CreateDocumentAsync(Document document);
        Task<Document?> GetDocumentByIdAsync(Guid id);
        Task<List<Document>> GetDocumentsByUserAsync(Guid userId);
        Task<Document?> UpdateDocumentAsync(Document document);
        Task<bool> DeleteDocumentAsync(Guid id);
        Task<List<Document>> SearchDocumentsAsync(string query);
        Task<bool> SetDocumentAccessAsync(Guid documentId, Guid userId, string accessLevel);
    }

    public class DocumentService : IDocumentService
    {
        private readonly AppDbContext _context;
        private readonly ILogger<DocumentService> _logger;

        public DocumentService(AppDbContext context, ILogger<DocumentService> logger)
        {
            _context = context;
            _logger = logger;
        }

        public async Task<Document> CreateDocumentAsync(Document document)
        {
            try
            {
                document.DocumentId = Guid.NewGuid();
                document.CreatedAt = DateTime.UtcNow;
                document.UpdatedAt = DateTime.UtcNow;
                document.Status = "Draft";
                document.VersionNumber = 1;

                _context.Documents.Add(document);
                await _context.SaveChangesAsync();
                _logger.LogInformation("Document created: {DocumentId}", document.DocumentId);
                return document;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error creating document");
                throw;
            }
        }

        public async Task<Document?> GetDocumentByIdAsync(Guid id)
        {
            try
            {
                return await _context.Documents
                    .Include(d => d.Versions)
                    .Include(d => d.ComplianceChecks)
                    .Include(d => d.AiAnalyses)
                    .FirstOrDefaultAsync(d => d.DocumentId == id);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error fetching document: {DocumentId}", id);
                throw;
            }
        }

        public async Task<List<Document>> GetDocumentsByUserAsync(Guid userId)
        {
            try
            {
                return await _context.Documents
                    .Where(d => d.CreatedBy == userId)
                    .OrderByDescending(d => d.CreatedAt)
                    .ToListAsync();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error fetching documents for user: {UserId}", userId);
                throw;
            }
        }

        public async Task<Document?> UpdateDocumentAsync(Document document)
        {
            try
            {
                var existingDoc = await _context.Documents.FirstOrDefaultAsync(d => d.DocumentId == document.DocumentId);
                if (existingDoc == null)
                    return null;

                existingDoc.Title = document.Title;
                existingDoc.Content = document.Content;
                existingDoc.Status = document.Status;
                existingDoc.UpdatedAt = DateTime.UtcNow;
                existingDoc.VersionNumber++;

                _context.Documents.Update(existingDoc);
                await _context.SaveChangesAsync();
                _logger.LogInformation("Document updated: {DocumentId}", document.DocumentId);
                return existingDoc;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error updating document: {DocumentId}", document.DocumentId);
                throw;
            }
        }

        public async Task<bool> DeleteDocumentAsync(Guid id)
        {
            try
            {
                var document = await _context.Documents.FirstOrDefaultAsync(d => d.DocumentId == id);
                if (document == null)
                    return false;

                _context.Documents.Remove(document);
                await _context.SaveChangesAsync();
                _logger.LogInformation("Document deleted: {DocumentId}", id);
                return true;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error deleting document: {DocumentId}", id);
                throw;
            }
        }

        public async Task<List<Document>> SearchDocumentsAsync(string query)
        {
            try
            {
                return await _context.Documents
                    .Where(d => d.Title.Contains(query) || d.Tags!.Contains(query))
                    .OrderByDescending(d => d.CreatedAt)
                    .ToListAsync();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error searching documents with query: {Query}", query);
                throw;
            }
        }

        public async Task<bool> SetDocumentAccessAsync(Guid documentId, Guid userId, string accessLevel)
        {
            try
            {
                var access = await _context.DocumentAccess
                    .FirstOrDefaultAsync(a => a.DocumentId == documentId && a.UserId == userId);

                if (access != null)
                {
                    access.AccessLevel = accessLevel;
                    _context.DocumentAccess.Update(access);
                }
                else
                {
                    var newAccess = new DocumentAccess
                    {
                        DocumentId = documentId,
                        UserId = userId,
                        AccessLevel = accessLevel,
                        GrantedAt = DateTime.UtcNow
                    };
                    _context.DocumentAccess.Add(newAccess);
                }

                await _context.SaveChangesAsync();
                return true;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error setting document access");
                throw;
            }
        }
    }
}
