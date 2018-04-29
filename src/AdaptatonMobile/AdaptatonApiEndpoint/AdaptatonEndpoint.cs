using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using AdaptatonApiEndpoint.Models;

namespace AdaptatonApiEndpoint
{
    public class AdaptatonEndpoint : ApiRestClient
    {
        private static readonly AdaptatonEndpoint _instance = new AdaptatonEndpoint();
        public static AdaptatonEndpoint Instance => _instance;

        public AdaptatonEndpoint()
        {
        }

        protected override string BaseUrl => "http://adaptaton18.azurewebsites.net/api/";

        public async Task<User> CheckDNI(string dni)
        {
            try
            {
                return await GetApiAsync<User>($"User/FindByDni/{dni}");
            }
            catch (ApiException ex)
            {
                if (ex.ErrorCode == System.Net.HttpStatusCode.NotFound)
                    return null;
                throw;
            }
        }

        public async Task<User> Authenticate(Credentials credentials)
        {
            try
            {
                return await PostApiAsync<User>("User/Auth", credentials);
            }
            catch (ApiException ex)
            {
                if (ex.ErrorCode == System.Net.HttpStatusCode.NotFound)
                    return null;
                throw;
            }
        }

        public async Task SaveIncident(IncidentRegister incident)
        {
            await PostApiAsync("Pins", incident);
        }

        public async Task<List<Incident>> GetIncidents()
        {
            return await GetApiAsync<List<Incident>>("Pins");
        }
    }
}
