using System;
using System.Collections.Generic;
using System.IO;
using Abp.Domain.Services;
using Abp.Extensions;
using PhoneLelo.Project.FileManagement;
using PhoneLelo.Project.Utils;
using Rhithm.Storage.FileManagement;

namespace PhoneLelo.Project.Storage.FileManagement
{
    public interface IFileStorageManager : IDomainService
    {
        string UploadFile(Stream stream, string fileName, PhoneLeloDataFileType rhithmDataFileType, bool autoGenerateName = true);


        string UploadFileText(string text, string fileName, PhoneLeloDataFileType rhithmDataFileType, bool autoGenerateName = true);

        Dictionary<string, Stream> GetAllCsvDataBlobsStreams();

        void DeleteFiles(List<string> blobNameList);

        string DownloadFileText(string fileName, PhoneLeloDataFileType rhithmDataFileType);
        string GenerateBlobUrl(string fileName, PhoneLeloDataFileType rhithmDataFileType);
    }

    public class FileStorageManager : DomainService, IFileStorageManager
    {
        // TODO: move to proper file extension consts file
        private const string CsvFileExtension = ".csv";
        private readonly IAzureStorageManager _azureStorageManager;

        public FileStorageManager(IAzureStorageManager azureStorageManager)
        {
            _azureStorageManager = azureStorageManager;
        }

        public string UploadFileText(string text, string fileName, PhoneLeloDataFileType rhithmDataFileType, bool autoGenerateName = true)
        {
            var extension = Path.GetExtension(fileName);
            var newFileName = fileName;

            if (autoGenerateName)
                newFileName = $"{Guid.NewGuid():N}{extension}";

            var mimeType = extension.GetMimeType();
            var blobFolderName = PhoneLeloDataFileTypeSettings.GetBlobFolderName(rhithmDataFileType);
            var blobName = $"{blobFolderName.EnsureEndsWith('/')}{newFileName}";

            _azureStorageManager.UploadFromString(text, blobName, mimeType);

            return newFileName;
        }

        public string UploadFile(Stream stream, string fileName, PhoneLeloDataFileType rhithmDataFileType, bool autoGenerateName = true)
        {
            var extension = Path.GetExtension(fileName);
            var newFileName = fileName;

            if (autoGenerateName)
                newFileName = $"{Guid.NewGuid():N}{extension}";

            var mimeType = extension.GetMimeType();
            var blobFolderName = PhoneLeloDataFileTypeSettings.GetBlobFolderName(rhithmDataFileType);
            var blobName = $"{blobFolderName.EnsureEndsWith('/')}{newFileName}";

            _azureStorageManager.UploadFromStream(stream, blobName, mimeType);

            return newFileName;
        }

        /// <summary>
        /// Returns a dictionary of all blobs which are of CSV type
        /// Dictionary Key: Blob name, Value: CSV Blob Stream
        /// </summary>
        /// <returns></returns>
        public Dictionary<string, Stream> GetAllCsvDataBlobsStreams()
        {
            var blobsList = _azureStorageManager.ListBlobsByExtension(CsvFileExtension);
            Dictionary<string, Stream> blobsStreamDictionary = null;

            foreach (var item in blobsList)
            {
                if (blobsStreamDictionary == null)
                {
                    blobsStreamDictionary = new Dictionary<string, Stream>();
                }

                var blobFolderName = PhoneLeloDataFileTypeSettings.GetBlobFolderName(PhoneLeloDataFileType.CsvImport);
                var blobName = $"{blobFolderName}{item.Name}";
                var blobStream = _azureStorageManager.DownloadBlobStream(blobName);
                blobsStreamDictionary.Add(blobName, blobStream);
            }
            return blobsStreamDictionary;
        }

        public void DeleteFiles(List<string> blobNameList)
        {
            _azureStorageManager.DeleteBlob(blobNameList);
        }


        public string DownloadFileText(string fileName, PhoneLeloDataFileType rhithmDataFileType)
        {
            var blobFolderName = PhoneLeloDataFileTypeSettings.GetBlobFolderName(rhithmDataFileType);
            var blobName = $"{blobFolderName.EnsureEndsWith('/')}{fileName}";

            return _azureStorageManager.DownloadBlobString(blobName);
        }


        public string GenerateBlobUrl(string fileName, PhoneLeloDataFileType rhithmDataFileType)
        {
            if (string.IsNullOrEmpty(fileName))
            {
                return null;
            }

            var blobFolderName = PhoneLeloDataFileTypeSettings.GetBlobFolderName(rhithmDataFileType);
            var blobName = $"{blobFolderName.EnsureEndsWith('/')}{fileName}";

            return _azureStorageManager.GenerateBlobUrl(blobName);
        }

    }
}
