using BankingAPI.Kernel.Wrapper;
using EInsuranceProject.DTO;
using EInsuranceProject.Model;
using EInsuranceProject.Services;
using EInsuranceProject.Wrapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.StaticFiles;
using Newtonsoft.Json;

namespace EInsuranceProject.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DocumentController : ControllerBase
    {


        //private IDocumentService _documentService;
        //public DocumentController(IDocumentService documentService)
        //{
        //    _documentService = documentService;
        //}
        //[HttpPost]
        //[Route("upload")]
        //[ProducesResponseType(StatusCodes.Status200OK)]
        //[ProducesResponseType(typeof(string), StatusCodes.Status400BadRequest)]
        //public async Task<IActionResult> UploadFile(IFormFile file, CancellationToken cancellationtoken, int id, string name)
        //{
        //    var document = ConvertToDocument(id, name);
        //    var filename = await _documentService.UploadDocument(file, document);
        //    return Ok(new { filename = filename });

        //}
        //[HttpGet]
        //[Route("download")]
        //public async Task<IActionResult> DownloadFile(int documentId)
        //{
        //    var document = _documentService.GetById(documentId);
        //    if (document != null)
        //    {
        //        var filepath = Path.Combine(Directory.GetCurrentDirectory(), "Upload\\Files", document.Result.DocumentName);


        //        var provider = new FileExtensionContentTypeProvider();
        //        if (!provider.TryGetContentType(filepath, out var contenttype))
        //        {
        //            contenttype = "application/octet-stream";
        //        }

        //        var bytes = await System.IO.File.ReadAllBytesAsync(filepath);
        //        return File(bytes, contenttype, Path.GetFileName(filepath));
        //    }
        //    return BadRequest();
        //}


        //private Document ConvertToDocument(int customerID, string name)
        //{
        //    var Document = new Document()
        //    {
        //        CustomerId = customerID,
        //        status = false,
        //        DocumentType = name,
        //    };
        //    return Document;

        //}
        //private DocumentDTO ConvertToDocumentDTO(Document document)
        //{
        //    var DocumentDTO = new DocumentDTO()
        //    {
        //        DocumentId = document.DocumentId,
        //        DocumentName = document.DocumentName,
        //        CustomerId = document.CustomerId,
        //        Status = document.status,
        //        DocumentType = document.DocumentType,

        //    };
        //    return DocumentDTO;
        //}
        //[HttpGet("GetAllDocuments")]
        //public async Task<IActionResult> GetDocuments()
        //{
        //    string[] innerTable = { };
        //    var documents = await _documentService.GetAll(innerTable);
        //    var documentDTOS = new List<DocumentDTO>();
        //    foreach (var document in documents)
        //    {
        //        documentDTOS.Add(ConvertToDocumentDTO(document));
        //    }
        //    return Ok(documentDTOS);
        //}

        //[HttpGet("GetDocumentsByCustomerId/{customerId}")]
        //public async Task<IActionResult> GetDocumentssByCustomerIdAsync(int customerId)
        //{
        //    var documents = await _documentService.GetDocumentsByCustomerIdAsync(customerId);
        //    return Ok(documents);
        //}

        //[HttpPut]
        //[Route("update")]
        //public async Task<IActionResult> UpdateDocument([FromQuery] int documentId)
        //{
        //    var result = await _documentService.UpdateDocumentStatus(documentId);
        //    if (result)
        //        return Ok(documentId);
        //    else
        //        return BadRequest("Bad request");
        //}
        private IDocumentService _documentService;
        public DocumentController(IDocumentService documentService)
        {
            _documentService = documentService;
        }
        [HttpPost]
        [Route("upload")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(string), StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> UploadFile(IFormFile file, CancellationToken cancellationtoken, int id, string name)
        {
            var document = ConvertToDocument(id, name);
            var filename = await _documentService.UploadDocument(file, document);
            return Ok(new { filename = filename });

        }
        [HttpGet]
        [Route("download")]
        public async Task<IActionResult> DownloadFile(int documentId)
        {
            var document = _documentService.GetById(documentId);
            if (document != null)
            {
                var filepath = Path.Combine(Directory.GetCurrentDirectory(), "Upload\\Files", document.Result.DocumentName);


                var provider = new FileExtensionContentTypeProvider();
                if (!provider.TryGetContentType(filepath, out var contenttype))
                {
                    contenttype = "application/octet-stream";
                }

                var bytes = await System.IO.File.ReadAllBytesAsync(filepath);
                return File(bytes, contenttype, Path.GetFileName(filepath));
            }
            return BadRequest();
        }


        private Document ConvertToDocument(int customerID, string name)
        {
            var Document = new Document()
            {
                CustomerId = customerID,
                status = false,
                DocumentType = name,
            };
            return Document;

        }
        private DocumentDTO ConvertToDocumentDTO(Document document)
        {
            var DocumentDTO = new DocumentDTO()
            {
                DocumentId = document.DocumentId,
                DocumentName = document.DocumentName,
                CustomerId = document.CustomerId,
                Status = document.status,
                DocumentType = document.DocumentType,

            };
            return DocumentDTO;
        }
        [HttpGet("GetAllDocuments")]
        public async Task<IActionResult> GetDocuments()
        {
            string[] innerTable = { };
            var documents = await _documentService.GetAll(innerTable);
            var documentDTOS = new List<DocumentDTO>();
            foreach (var document in documents)
            {
                documentDTOS.Add(ConvertToDocumentDTO(document));
            }
            return Ok(documentDTOS);
        }

        [HttpGet("GetDocumentsByCustomerId/{customerId}")]
        public async Task<IActionResult> GetDocumentssByCustomerIdAsync(int customerId)
        {
            var documents = await _documentService.GetDocumentsByCustomerIdAsync(customerId);
            return Ok(documents);
        }
        [HttpPut]
        [Route("update")]
        public async Task<IActionResult> UpdateDocument([FromQuery] int documentId)
        {
            var result = await _documentService.UpdateDocumentStatus(documentId);
            if (result)
                return Ok(documentId);
            else
                return BadRequest("Bad request");
        }
        [HttpPut]
        [Route("delete")]
        public async Task<IActionResult> DeleteDocument([FromQuery] int documentId)
        {
            var result = await _documentService.DeleteDocument(documentId);
            if (result)
                return Ok(documentId);
            else
                return BadRequest("Bad request");
        }

        [HttpGet("get")]

        public async Task<IActionResult> GetEmployee([FromQuery] Parameter pageParameter)

        {

            string[] innerTable = { };

            var customers = await _documentService.GetAsync(pageParameter);

            var metadata = new

            {

                customers.TotalCount,

                customers.PageSize,

                customers.CurrentPage,

                customers.TotalPages,

                customers.HasNext,

                customers.HasPrevious,

            };

            Response.Headers.Add("X-Pagination", JsonConvert.SerializeObject(metadata));



            return Ok(customers);

        }


    }
}


    




