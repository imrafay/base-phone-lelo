using PhoneLelo.Project.FileManagement;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Rhithm.Storage.FileManagement
{
    [AttributeUsage(AttributeTargets.All)]
    public class PhoneLeloDataFileTypeSettings : Attribute
    {
        public PhoneLeloDataFileTypeSettings(
            string blobFolderName,
            string allowedFileExtensions,
            double allowedFileSizeInMb)
        {
            BlobFolderName = blobFolderName;
            AllowedFileExtensions = allowedFileExtensions;
            AllowedFileSizeInMb = allowedFileSizeInMb;
        }


        public string AllowedFileExtensions { get; set; }
        public static string GetAllowedFileExtensions(PhoneLeloDataFileType type)
        {
            return GetAttribute(type).AllowedFileExtensions;
        }


        public double AllowedFileSizeInMb { get; set; }
        public static double GetAllowedFileSizeInMb(PhoneLeloDataFileType type)
        {
            return GetAttribute(type).AllowedFileSizeInMb;
        }



        public string BlobFolderName { get; set; }
        public static string GetBlobFolderName(PhoneLeloDataFileType type)
        {
            return GetAttribute(type).BlobFolderName;
        }


        private static PhoneLeloDataFileTypeSettings GetAttribute(PhoneLeloDataFileType type)
        {

            var memberInfo = typeof(PhoneLeloDataFileType).GetMember(type.ToString()).FirstOrDefault();

            if (memberInfo != null)
            {
                var attribute = (PhoneLeloDataFileTypeSettings)
                    memberInfo.GetCustomAttributes(typeof(PhoneLeloDataFileTypeSettings), false)
                        .FirstOrDefault();
                return attribute;
            }

            return null;
        }
    }
}
