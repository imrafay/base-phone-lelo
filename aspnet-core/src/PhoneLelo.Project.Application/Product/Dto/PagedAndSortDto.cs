using System;
using System.ComponentModel.DataAnnotations;
using Abp.Application.Services.Dto;
using Abp.Authorization.Users;
using Abp.AutoMapper;
using CsvHelper.Configuration.Attributes;
using PhoneLelo.Project.Authorization.Users;
using PhoneLelo.Project.Product.Enum;

namespace PhoneLelo.Project.Product.Dto
{
    public class PagedAndSortDto
    {    
        public int Id { get; set; }
        public int Name { get; set; }
        public SortByEnum SortBy { get; set; }
    }
}
