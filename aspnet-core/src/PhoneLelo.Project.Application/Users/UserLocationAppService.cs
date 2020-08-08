using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Abp.Application.Services;
using Abp.Authorization;
using Abp.Domain.Entities;
using PhoneLelo.Project.Authorization;
using PhoneLelo.Project.Authorization.Users;
using PhoneLelo.Project.Product.Dto;
using PhoneLelo.Project.Users.Dto;

namespace PhoneLelo.Project.Import.MobilePhone
{
    public class UserLocationAppService : ApplicationService, IUserLocationAppService
    {
        private readonly RosterLocationManager _rosterLocationManager;
        private readonly UserManager _userManager;

        public UserLocationAppService(
            RosterLocationManager rosterLocationManager
            , UserManager userManager)
        {
            _rosterLocationManager = rosterLocationManager;
            _userManager = userManager;
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

        [AbpAllowAnonymous]
        public async Task UpdateUserLocation(UserLocationInputDto input)
        {
            var user = await _userManager.GetUserByIdAsync(input.UserId);
            if (!user.IsNullOrDeleted())
            {
                user.StateId = input.StateId;
                user.CityId= input.CityId;
                user.NeighbourhoodId = input.NeighbourhoodId;
                user.IsLocationFilled = true;

                await _userManager.UpdateAsync(user);
            }
        }
    }
}

