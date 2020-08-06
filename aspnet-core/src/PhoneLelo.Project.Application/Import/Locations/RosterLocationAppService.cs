using Abp.Application.Services;
using Abp.Authorization;
using Abp.Runtime.Session;
using PhoneLelo.Project.Authorization;
using System;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using Newtonsoft.Json.Linq;
using PhoneLelo.Project.Location;
using System.Collections.Generic;
using PhoneLelo.Project.Import.Locations;

namespace PhoneLelo.Project.Import.MobilePhone
{
    public class RosterLocationAppService : ApplicationService, IRosterLocationAppService
    {
        private readonly IAbpSession _abpSession;
        private readonly ProductCompanyManager _productCompanyManager;
        private readonly ProductModelManager _productModelManager;
        private readonly RosterLocationManager _rosterLocationManager;

        public RosterLocationAppService(

            IAbpSession abpSession,
            ProductCompanyManager productCompanyManager,
            ProductModelManager productModelManager, RosterLocationManager rosterLocationManager)
        {
            _abpSession = abpSession;
            _productCompanyManager = productCompanyManager;
            _productModelManager = productModelManager;
            _rosterLocationManager = rosterLocationManager;
        }

        [AbpAllowAnonymous]
        public async Task GetAndSeedPakistanCities()
        {
            var countries = await _rosterLocationManager.GetOlxApiResponse(
                baseUrl: AppConsts.OlxBaseUrl,
                baseParameter: AppConsts.OlxUrlParameters);

            var citiesList = new List<City>();

            foreach (var country in countries)
            {
                foreach (var state in country.children)
                {
                    var stateLongitude = Convert.ToDecimal(state.longitude.ToString());
                    var stateLatitude = Convert.ToDecimal(state.latitude.ToString());

                    var stateDb = new State()
                    {
                        RosterSourceId = state.id.ToString(),
                        Name = state.name.ToString(),
                        Longitude = stateLongitude,
                        Latitude = stateLatitude
                    };

                    var stateId = await _rosterLocationManager
                        .InsertStateAsync(stateDb);


                    foreach (var city in state.children)
                    {
                        var cityLongitude = Convert.ToDecimal(city.longitude.ToString());
                        var cityLatitude = Convert.ToDecimal(city.latitude.ToString());

                        var cityDb = new City()
                        {
                            StateId = stateId,
                            RosterSourceId = city.id.ToString(),
                            Name = city.name.ToString(),
                            Longitude = cityLongitude,
                            Latitude = cityLatitude
                        };

                        citiesList.Add(cityDb);
                    }
                }
            }

            await _rosterLocationManager.InsertCitiesAsync(citiesList);
        }
    }
}

