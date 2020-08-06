using Abp.Domain.Repositories;
using Abp.Domain.Services;
using System.Threading.Tasks;
using System.Collections.Generic;
using System.Linq;
using PhoneLelo.Project.Location;
using Abp.Linq.Extensions;
using Microsoft.EntityFrameworkCore;
using Abp.EntityFrameworkCore.Repositories;
using System;
using System.Net.Http;
using System.Net.Http.Headers;
using Abp.Extensions;
using Newtonsoft.Json.Linq;

namespace PhoneLelo.Project.Authorization
{
    public class RosterLocationManager : DomainService
    {
        private readonly IRepository<State, long> _stateRepository;
        private readonly IRepository<City, long> _cityRepository;
        private readonly IRepository<Neighbourhood, long> _neighbourhoodRepository;

        public RosterLocationManager(
            IRepository<State, long> stateRepository,
            IRepository<City, long> cityRepository,
            IRepository<Neighbourhood, long> neighbourhoodRepository)
        {
            _stateRepository = stateRepository;
            _cityRepository = cityRepository;
            _neighbourhoodRepository = neighbourhoodRepository;
        }

        public async Task<dynamic> GetOlxApiResponse(
            string baseUrl,
            string baseParameter,
            string optionalParameter = null)
        {
            HttpClient client = new HttpClient();
            client.BaseAddress = new Uri(baseUrl);

            // Add an Accept header for JSON format.
            client.DefaultRequestHeaders.Accept.Add(
            new MediaTypeWithQualityHeaderValue("application/json"));

            // List data response.
            var parameters = baseParameter;
            if (!optionalParameter.IsNullOrEmpty())
            {
                parameters = parameters + optionalParameter;
            }
            HttpResponseMessage response = await client.GetAsync(parameters);
            if (response.IsSuccessStatusCode)
            {
                // Parse the response body.
                var dataObjects = await response.Content.ReadAsStringAsync();
                dynamic data = JObject.Parse(dataObjects);
                return data.data;
            }

            return null;
        }

        public IQueryable<State> GetAllStates()
        {
            var stateQuery = _stateRepository
                .GetAll();

            return stateQuery;
        }

        public IQueryable<City> GetAllCities(
            long stateId = 0)
        {
            var cityQuery = _cityRepository
                .GetAll()
                .WhereIf(stateId > 0, x => x.StateId == stateId);

            return cityQuery;
        }

        public IQueryable<Neighbourhood> GetAllNeighbourhoods(
            long cityId = 0)
        {
            var neighbourhoodQuery = _neighbourhoodRepository
                .GetAll()
                .WhereIf(cityId > 0, x => x.CityId == cityId);

            return neighbourhoodQuery;
        }

        public async Task<List<State>> GetAllStateListAsync()
        {
            var states = await _stateRepository
                .GetAllListAsync();
            return states;
        }

        public async Task<List<City>> GetAllCityListAsync(long stateId = 0)
        {
            var cities = await _cityRepository
                .GetAll()
                .WhereIf(stateId > 0, x => x.StateId == stateId)
                .ToListAsync();

            return cities;
        }

        public async Task<List<Neighbourhood>> GetAllNeighbourhoodListAsync(
            long cityId = 0)
        {
            var neighbourhoods = await _neighbourhoodRepository
                 .GetAll()
                 .WhereIf(cityId > 0, x => x.CityId == cityId)
                 .ToListAsync();

            return neighbourhoods;
        }


        public async Task<long> InsertStateAsync(State state)
        {
            var stateDb = await _stateRepository
                .FirstOrDefaultAsync(x =>
                x.RosterSourceId == state.RosterSourceId);
            if (stateDb == null)
            {
                await _stateRepository.InsertAsync(state);
                await CurrentUnitOfWork.SaveChangesAsync();

                return state.Id;
            }
            return stateDb.Id;
        }

        public async Task InsertCitiesAsync(List<City> cities)
        {
            var citiesRosterSourceIds = cities
                .Select(x => x.RosterSourceId)
                .ToList();

            //Insert only new cities
            var dbCities = await _cityRepository
                .GetAll()
                .Where(c => citiesRosterSourceIds
                    .Contains(c.RosterSourceId))
                .ToListAsync();

            cities = cities
                .Where(c => !dbCities
                    .Select(x => x.RosterSourceId)
                    .ToList()
                        .Contains(c.RosterSourceId))
                .ToList();

            if (cities.Any())
            {
                await _cityRepository
                .GetDbContext()
                .AddRangeAsync(cities);

                await CurrentUnitOfWork.SaveChangesAsync();
            }
        }
    }
}
