using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Abp.Application.Services;
using Abp.Authorization;
using PhoneLelo.Project.Authorization;
using PhoneLelo.Project.Product.Dto;

namespace PhoneLelo.Project.Import.MobilePhone
{
    public class UserLocationAppService : ApplicationService, IUserLocationAppService
    {
        private readonly RosterLocationManager _rosterLocationManager;

        public UserLocationAppService(
            RosterLocationManager rosterLocationManager)
        {
            _rosterLocationManager = rosterLocationManager;
        }

        [AbpAllowAnonymous]
        public async Task<List<DropdownOutputDto>> GetStates()
        {
            var states = await _rosterLocationManager
                .GetAllStateListAsync();

            var statesDropdown = states
                .Select(x => new DropdownOutputDto()
                {
                    Id =x.Id,
                    Name =x.Name
                }).ToList();

            return statesDropdown;
        }
        
        [AbpAllowAnonymous]
        public async Task<List<DropdownOutputDto>> GetCitiesByStateId(
            long stateId)
        {
            var cities = await _rosterLocationManager
                .GetAllCityListAsync(stateId: stateId);

            var citiesDropdown = cities
                .Select(x => new DropdownOutputDto()
                {
                    Id =x.Id,
                    Name =x.Name
                }).ToList();

            return citiesDropdown;
        }

        [AbpAllowAnonymous]
        public async Task<List<DropdownOutputDto>> GetNeighbourhoodsByCityId(
            long cityId)
        {
            var neighbourhoods = await _rosterLocationManager
                .GetAllNeighbourhoodListAsync(cityId: cityId);

            var neighbourhoodsDropdown = neighbourhoods
                .Select(x => new DropdownOutputDto()
                {
                    Id = x.Id,
                    Name = x.Name
                }).ToList();

            return neighbourhoodsDropdown;
        }
    }
}

