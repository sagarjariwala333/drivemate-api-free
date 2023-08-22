using DriveMate.Entities;
using DriveMate.Requests;

namespace DriveMate.Interfaces
{
    public interface IDocumentService
    {
        public Task<UserDocument> SaveAsync(UploadDocumentRequest uploadDocumentRequest);
    }
}
