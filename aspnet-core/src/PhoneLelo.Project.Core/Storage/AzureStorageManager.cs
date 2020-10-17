using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using Abp.Domain.Services;
using Azure.Storage.Blobs;
using Azure.Storage.Blobs.Models;
using Microsoft.Extensions.Configuration;

namespace PhoneLelo.Project.Storage
{
    public interface IAppConfigurationAccessor
    {
        IConfigurationRoot Configuration { get; }
    }

    public interface IAzureStorageManager : IDomainService
    {
        void UploadFromString(string text, string blobName, string contentType);

        void UploadFromStream(Stream stream, string blobName, string contentType);

        string DownloadBlobString(string blobName);

        bool IsBlobExist(string blobName);

        List<BlobItem> ListBlobsByExtension(string extension);

        List<BlobItem> ListBlobs(string extension = null);

        void DeleteBlob(List<string> blobNames);

        Stream DownloadBlobStream(string blobName);

        string GenerateBlobUrl(string blobName);

    }

    public class AzureStorageManager : DomainService, IAzureStorageManager
    {
        //private readonly IConfigurationRoot _appConfiguration;
        //public string ConnectionString => _appConfiguration["App:AzureStorage:ConnectionString"];
        //public string ContainerName => _appConfiguration["App:AzureStorage:ContainerName"];
        //public string BaseUrl => _appConfiguration["App:AzureStorage:BaseUrl"];

        //public AzureStorageManager(IAppConfigurationAccessor configurationAccessor)
        //{
        //    _appConfiguration = configurationAccessor.Configuration;

        //}

        public string ConnectionString => "DefaultEndpointsProtocol=https;AccountName=phonelelostore;AccountKey=2La4OgnweFV1fFFq+t7n4Z+JhpJfYOtF0XLdqI2MRWjFeMJdoMd3fSyRjDk3AkU7xDJk5CQk7pnqVczsgR/8oA==;EndpointSuffix=core.windows.net";
        public string BaseUrl  => "https://phonelelostore.blob.core.windows.net";
        public string ContainerName => "phonelelo";


        public void UploadFromString(string text, string blobName, string contentType)
        {
            UploadFromStream(new MemoryStream(Encoding.UTF8.GetBytes(text)), blobName, contentType);
        }

        public void UploadFromStream(Stream stream, string blobName, string contentType)
        {
            if (stream == null)
                return;

            var container = new BlobContainerClient(ConnectionString, ContainerName);
            stream.Position = 0;
            var blockBlob = container.GetBlobClient((blobName));
            //blockBlob.Properties.ContentType = contentType;
            blockBlob.Upload(stream);
        }

        public bool IsBlobExist(string blobName)
        {
            var container = new BlobContainerClient(ConnectionString, ContainerName);
            var blockBlob = container.GetBlobClient(blobName);
            return blockBlob.Exists();
        }

        public void DeleteBlob(string blobName)
        {
            var container = new BlobContainerClient(ConnectionString, ContainerName);
            var blockBlob = container.GetBlobClient(blobName);
            blockBlob.Delete();
        }

        public void DeleteBlob(List<string> blobNames)
        {
            foreach (var blobName in blobNames)
            {
                DeleteBlob(blobName);
            }
        }

        public Stream DownloadBlobStream(string blobName)
        {
            try
            {
                var container = new BlobContainerClient(ConnectionString, ContainerName);
                var blockBlob = container.GetBlobClient(blobName);
                var response = blockBlob.Download();
                return response.Value.Content;
            }
            catch (Exception ex)
            {
                return null;
            }
        }


        public string DownloadBlobString(string blobName)
        {
            try
            {
                var container = new BlobContainerClient(ConnectionString, ContainerName);
                var blockBlob = container.GetBlobClient(blobName);
                var response = blockBlob.Download();
                var stream = response.Value.Content;

                var reader = new StreamReader(stream, Encoding.UTF8);
                var text = reader.ReadToEnd();
                return text;
            }
            catch (Exception ex)
            {
                return string.Empty;
            }
        }

        public List<BlobItem> ListBlobsByExtension(string extension)
        {
            return ListBlobs(extension);
        }

        public List<BlobItem> ListBlobs(string extension = null)
        {
            var container = new BlobContainerClient(ConnectionString, ContainerName);
            var list = container.GetBlobs();
            var blobs = list.GetEnumerator();
            List<BlobItem> output = null;

            while (blobs.MoveNext())
            {
                if (output == null)
                {
                    output = new List<BlobItem>();
                }
                var currBlob = blobs.Current;
                if (extension == null)
                {
                    output.Add(currBlob);
                }
                else if (Path.GetExtension(currBlob.Name).Equals(extension))
                {
                    output.Add(currBlob);
                }
            }

            return output;
        }

        public string GenerateBlobUrl(string blobName)
        {
            return Flurl.Url.Combine(BaseUrl, ContainerName, blobName);
        }

    }
}
