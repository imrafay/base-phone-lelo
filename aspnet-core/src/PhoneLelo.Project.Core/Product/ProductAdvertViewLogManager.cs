using Abp.Domain.Repositories;
using Abp.Domain.Services;
using Abp.EntityFrameworkCore.Repositories;
using Abp.Linq.Extensions;
using Microsoft.EntityFrameworkCore;
using PhoneLelo.Project.Authorization.Users;
using PhoneLelo.Project.Product.Dto;
using PhoneLelo.Project.Product.Enum;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PhoneLelo.Project.Authorization
{
    public class ProductAdvertViewLogManager : DomainService
    {
        private readonly IRepository<ProductAdvertViewLog, long> _productAdvertViewLogRepository;

        public ProductAdvertViewLogManager(
               IRepository<ProductAdvertViewLog, long> productAdvertViewLogRepository)

        {
            _productAdvertViewLogRepository = productAdvertViewLogRepository;
        }

        public async Task<int> GetViewsCountByAdvertId(long advertId)
        {

            var viewsCount = await _productAdvertViewLogRepository
                    .CountAsync(x => x.ProductAdvertId == advertId);

            return viewsCount;
        }

        public async Task CreateAsync(
            long advertId,
            long? userId)
        {

            await _productAdvertViewLogRepository
                    .InsertAsync(
                        new ProductAdvertViewLog()
                        {
                            ProductAdvertId = advertId,
                            UserId = userId
                        });

            await CurrentUnitOfWork.SaveChangesAsync();
        }
    }
}
