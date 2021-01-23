using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Abp.Domain.Services;
using Azure.Storage.Blobs;
using Azure.Storage.Blobs.Models;
using Firebase.Auth;
using Firebase.Storage;
using Microsoft.Extensions.Configuration;

namespace PhoneLelo.Project.Storage
{
    public interface IFirbaseStorageManager  : IDomainService
    {

        Task UploadFromStream(Stream stream, string folderName, string fileName);

    }

    public class FirbaseStorageManager : DomainService, IFirbaseStorageManager
    {
        //TODO:Remove Hard codeded value from here 
        private static string ApiKey = "AIzaSyB8ySYFi2EN1_LkhaH7peA8oVmPiXYGK6o";
        private static string Bucket = "phonelelo-srorage.appspot.com";
        private static string AuthEmail = "phone.lelo2020@gmail.com";
        private static string AuthPassword = "10_Hassan";

        public async Task UploadFromStream(Stream stream, string folderName,string fileName)
        {
            var auth = new FirebaseAuthProvider(new FirebaseConfig(ApiKey));
            var authLink = await auth.SignInWithEmailAndPasswordAsync(AuthEmail, AuthPassword);

            var cancellation = new CancellationTokenSource();
            var task = new FirebaseStorage(Bucket,
                new FirebaseStorageOptions
                {
                    AuthTokenAsyncFactory = () => Task.FromResult(authLink.FirebaseToken),
                    ThrowOnCancel = true // when you cancel the upload, exception is thrown. By default no exception is thrown
                })
                 .Child("ProductImages")
                 .Child(fileName)
                 .PutAsync(stream);
            
            var upload = new FirebaseStorage(Bucket)
                 .Child("Images")
                 .Child(fileName)
                 .PutAsync(stream,cancellation.Token);

            try
            {
                var result = await task;
            }
            catch (Exception ex)
            {

                Logger.Error(ex.StackTrace);
            }
        }
    }
}
