using System;
using System.Collections.Generic;
using System.Text;

namespace PhoneLelo.Project.FileManagement
{
    public enum PhoneLeloDataFileType
    {
        [PhoneLeloDataFileTypeSettings(
               blobFolderName: "CsvImports/",
               allowedFileExtensions: ".csv,.CSV",
               allowedFileSizeInMb: 20)]
        CsvImport = 0,

        [PhoneLeloDataFileTypeSettings(
            blobFolderName: "ProductImages/",
            allowedFileExtensions: ".png,.PNG,.JPEG,.jpeg,.JPG,.jpg",
            allowedFileSizeInMb: 20)]
        ProductImages = 1,
    }
}
