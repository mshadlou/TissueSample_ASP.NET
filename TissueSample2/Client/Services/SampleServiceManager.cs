using TissueSample2.Shared.Models;
using TissueSample2.Client.Services;
using System.Net;
using System.Net.Http.Headers;
using System.Net.Http.Json;

namespace TissueSample2.Client.Services
{
    public interface ISampleService
    {
        Task<List<Sample>> GetSamples(int c_id);

        Task<Sample> GetSample(int id);

        Task<HttpResponseMessage> DeleteSample(int c_id);

        Task<HttpResponseMessage> AddSample(Sample sample);

        Task<HttpResponseMessage> UpdateSample(Sample sample);
    }

    public class SampleServiceManager : ISampleService
    {
        private readonly HttpClient Http;
        public SampleServiceManager(HttpClient HTTP)
        {
            Http = HTTP;
        }
        public async Task<List<Sample>> GetSamples(int c_id)
        {
            return await Http.GetFromJsonAsync<List<Sample>>("api/Sample" + "?cid=" + c_id);
        }
        public async Task<Sample> GetSample(int id)
        {
            return await Http.GetFromJsonAsync<Sample>("api/Sample/" + id);
        }
        public async Task<HttpResponseMessage> DeleteSample(int id)
        {
            return await Http.DeleteAsync("api/Sample/" + id);
        }
        public async Task<HttpResponseMessage> AddSample(Sample sample)
        {
            return await Http.PostAsJsonAsync("api/Sample", sample);
        }
        public async Task<HttpResponseMessage> UpdateSample(Sample sample)
        {
            return await Http.PutAsJsonAsync("api/Sample", sample);
        }
    }
}
