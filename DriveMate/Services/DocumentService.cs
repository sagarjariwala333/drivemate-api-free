using AutoMapper;
using DriveMate.Context;
using DriveMate.Entities;
using DriveMate.Interfaces;
using DriveMate.Requests;
using Microsoft.EntityFrameworkCore;

namespace DriveMate.Services
{
    public class DocumentService : IDocumentService
    {
        private AppDBContext _dbContext;
        private IMapper _mapper;

        public DocumentService(AppDBContext appDBContext, IMapper mapper)
        {
            _dbContext = appDBContext;
            _mapper = mapper;
        }

        public async Task<UserDocument> SaveAsync(UploadDocumentRequest uploadDocumentRequest)
        {
            try
            {
                if (uploadDocumentRequest.Id == null)
                {
                    UserDocument userDocument = _mapper.Map<UserDocument>(uploadDocumentRequest);
                    userDocument.FileDate = uploadDocumentRequest.FileData;
                    userDocument.IssueDate = DateTime.Now;
                    userDocument.ExpiryDate = DateTime.Now;
                    userDocument.IssueAt = "";
                    userDocument.Path = "";
                    var data = await _dbContext.UserDocuments.AddAsync(userDocument);
                    await _dbContext.SaveChangesAsync();
                    return userDocument;
                }
                else
                {
                    UserDocument userDocument = _mapper.Map<UserDocument>(uploadDocumentRequest);
                    var data = await _dbContext.UserDocuments
                        .FirstOrDefaultAsync(x => x.Id == uploadDocumentRequest.Id);

                    if(data != null)
                    {
                        data = userDocument;
                        await _dbContext.SaveChangesAsync();
                    }

                    return data;
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
    
    }
}
