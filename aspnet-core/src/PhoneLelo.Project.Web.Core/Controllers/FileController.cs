using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using Abp.Auditing;
using Abp.IO.Extensions;
using Abp.UI;
using Abp.Web.Models;
using Microsoft.AspNetCore.Mvc;
using PhoneLelo.Project.DemoUiComponents.Dto;
using PhoneLelo.Project.File.Dto;
using PhoneLelo.Project.FileManagement;
using PhoneLelo.Project.Storage;
using PhoneLelo.Project.Storage.FileManagement;

namespace PhoneLelo.Project.Controllers
{
    [Route("api/[controller]/[action]")]
    public class FileController : ProjectControllerBase
    {
        private readonly ITempFileCacheManager _tempFileCacheManager;
        private readonly IBinaryObjectManager _binaryObjectManager;
        private readonly IFileStorageManager _fileStorageManager;

        public FileController(
            ITempFileCacheManager tempFileCacheManager,
            IBinaryObjectManager binaryObjectManager,
            IFileStorageManager fileStorageManager
        )
        {
            _tempFileCacheManager = tempFileCacheManager;
            _binaryObjectManager = binaryObjectManager;
            _fileStorageManager = fileStorageManager;
        }


        [HttpPost]
        public async Task<JsonResult> UploadFiles()
        {
            try
            {
                var files = Request.Form.Files;
                var PhoneLeloDataFileTypeString = Request.Form["PhoneLeloDataFileType"];
                var PhoneLeloDataFileType = (PhoneLeloDataFileType)Enum.Parse(typeof(PhoneLeloDataFileType), PhoneLeloDataFileTypeString);

                //Check input
                if (files == null)
                {
                    throw new UserFriendlyException(L("File_Empty_Error"));
                }

                List<UploadFileOutput> filesOutput = new List<UploadFileOutput>();

                foreach (var file in files)
                {
                    #region  FileSize Validation

                    var allowedFileSize = PhoneLeloDataFileTypeSettings.GetAllowedFileSizeInMb(PhoneLeloDataFileType);

                    if (ConvertBytesToMegabytes(file.Length) > allowedFileSize) //1MB
                    { throw new UserFriendlyException($"Invalid File Size. Allowed File Size {allowedFileSize}MB"); }

                    #endregion

                    #region  Extension Validation
                    var uploadedFileExtension = Path.GetExtension(file.FileName).ToLower();
                    var allowedFileExtensions = PhoneLeloDataFileTypeSettings.GetAllowedFileExtensions(PhoneLeloDataFileType);
                    var isValidExtension = allowedFileExtensions
                                                .Split(',')
                                                .Select(x => x.ToLower())
                                                .Contains(uploadedFileExtension);

                    if (isValidExtension == false)
                    {
                        throw new UserFriendlyException($"Invalid Extension. Allowed Extension {allowedFileExtensions}");
                    }
                    #endregion

                    var newFileName = _fileStorageManager
                            .UploadFile(
                                file.OpenReadStream(),
                                file.FileName,
                                PhoneLeloDataFileType);

                    filesOutput.Add(new UploadFileOutput
                    {
                        Id = Guid.Empty,
                        FileName = newFileName,
                        Url = _fileStorageManager.GenerateBlobUrl(
                            newFileName,
                            PhoneLeloDataFileType.ProductImages)
                    }); ;
                }

                return Json(new AjaxResponse(filesOutput));
            }
            catch (UserFriendlyException ex)
            {
                return Json(new AjaxResponse(new ErrorInfo(ex.Message)));
            }
        }

        private static double ConvertBytesToMegabytes(long bytes)
        {
            return (bytes / 1024f) / 1024f;
        }


        [DisableAuditing]
        private ActionResult DownloadTempFile(FileDto file)
        {
            var fileBytes = _tempFileCacheManager.GetFile(file.FileToken);
            if (fileBytes == null)
            {
                return NotFound(L("RequestedFileDoesNotExists"));
            }

            return File(fileBytes, file.FileType, file.FileName);
        }

        [DisableAuditing]
        private async Task<ActionResult> DownloadBinaryFile(Guid id, string contentType, string fileName)
        {
            var fileObject = await _binaryObjectManager.GetOrNullAsync(id);
            if (fileObject == null)
            {
                return StatusCode((int)HttpStatusCode.NotFound);
            }

            return File(fileObject.Bytes, contentType, fileName);
        }
    }
}