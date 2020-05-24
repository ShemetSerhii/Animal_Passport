using System;
using System.Threading.Tasks;
using AnimalPassport.BusinessLogic.Interfaces;
using AnimalPassport.WebApi.Extensions;
using AnimalPassport.WebApi.Models;
using Microsoft.AspNetCore.Mvc;

namespace AnimalPassport.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AttachmentController : ControllerBase
    {
        private readonly IAttachmentManager _attachmentManager;

        public AttachmentController(IAttachmentManager attachmentManager)
        {
            _attachmentManager = attachmentManager;
        }

        [HttpGet("{attachmentId}")]
        public async Task<IActionResult> GetAttachment(Guid attachmentId)
        {
            var file = await _attachmentManager.DownloadAttachmentAsync(attachmentId);

            return File(file.Content, file.ContentType, file.FileName);
        }

        [HttpPost("{medicalRowId}")]
        public async Task<IActionResult> UploadAttachment(Guid medicalRowId, [FromForm] AttachmentModel model)
        {
            await _attachmentManager.UploadAttachmentAsync(medicalRowId, model.Attachment.AsFile());

            return Ok();
        }

        [HttpDelete("{attachmentId}")]
        public async Task<IActionResult> DeleteAttachment(Guid attachmentId)
        {
            await _attachmentManager.DeleteAttachmentAsync(attachmentId);

            return Ok();
        }
    }
}