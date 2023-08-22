using DriveMate.BaseClass;
using DriveMate.Entities;
using DriveMate.Interfaces;
using DriveMate.Requests;
using DriveMate.Requests.TripRequest;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace DriveMate.Controllers
{
    [Authorize]
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class DocumentController : ApiBaseController
    {
        private readonly ILogger<DocumentController> logger;
        private IDocumentService _documentService;

        public DocumentController(ILogger<DocumentController> logger, IDocumentService documentService)
        {
            this.logger = logger;
            _documentService = documentService;
        }

        [HttpPost(Name = "AddDocument")]
        public async Task<JsonResponse> AddDocument
            (IFormCollection imgdata)
        {
            try
            {
                UploadDocumentRequest userDocument = new UploadDocumentRequest();
                IFormFile formFile = imgdata.Files[0];

                using(var memory = new MemoryStream())
                {
                    formFile.CopyTo(memory);
                    userDocument.FileData = memory.ToArray();
                }

                userDocument.UserId = user_unique_id;
                userDocument.Name = formFile.FileName;

                var d = imgdata.ToList();
                var doc = d[0];
                userDocument.DocumentNo = d[0].Value;
                userDocument.status = (d[1].Value == 1) ? true : false;
                userDocument.IsDeleted = false;
                userDocument.Type = Path.GetExtension(formFile.FileName);
               // userDocument.Id = user_unique_id;
                
                var result = await _documentService.SaveAsync(userDocument);
                return new JsonResponse(200, true, "Success", result);
            }
            catch (Exception ex)
            {
                return new JsonResponse(200, true, "Fail", ex.Message);
            }
        }
    }
}
