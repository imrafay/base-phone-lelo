using System;
using System.ComponentModel.DataAnnotations;
using Abp.Application.Services.Dto;
using Abp.Authorization.Users;
using Abp.AutoMapper;
using CsvHelper.Configuration.Attributes;
using PhoneLelo.Project.Authorization.Users;

namespace PhoneLelo.Project.Product.Dto
{
    public class DropdownOutputDto
    {    
        public long Id { get; set; }
        public string Name { get; set; }
    }
}
