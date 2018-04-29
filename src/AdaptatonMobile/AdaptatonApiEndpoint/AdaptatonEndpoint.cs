using System;
using System.Threading.Tasks;

namespace AdaptatonApiEndpoint
{
    public class AdaptatonEndpoint : ApiRestClient
    {
        private static readonly AdaptatonEndpoint _instance = new AdaptatonEndpoint();
        public static AdaptatonEndpoint Instance => _instance;

        public AdaptatonEndpoint()
        {
        }

        protected override string BaseUrl => "";

        public async Task<bool> CheckDNI(string dni)
        {
            return true;
            //return await GetApiAsync<bool>("");
        }
    }
}
