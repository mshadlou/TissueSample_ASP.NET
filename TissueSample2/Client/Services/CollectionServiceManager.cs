using TissueSample2.Shared.Models;
using TissueSample2.Client.Services;
using System.Net;
using System.Net.Http.Headers;
using System.Net.Http.Json;

namespace TissueSample2.Client.Services
{
    public interface ICollectionService
    {
        Task<List<Collection>> GetCollections();

        Task<Collection> GetCollection(int c_id);

        Task<HttpResponseMessage> DeleteCollection(int c_id);

        Task<HttpResponseMessage> AddCollection(Collection collection);

        Task<HttpResponseMessage> UpdateCollection(Collection collection);
    }

    public class CollectionServiceManager: ICollectionService
    {
        private readonly HttpClient Http;
        public CollectionServiceManager(HttpClient HTTP)
        {
            Http = HTTP;
        }
        public async Task<List<Collection>> GetCollections()
        {
            return await Http.GetFromJsonAsync<List<Collection>>("api/Collection");
        }
        public async Task<Collection> GetCollection(int c_id)
        {
            return await Http.GetFromJsonAsync<Collection>("api/Collection/" + c_id);
        }
        public async Task<HttpResponseMessage> DeleteCollection(int c_id)
        {
            return await Http.DeleteAsync("api/Collection/" + c_id);
        }
        public async Task<HttpResponseMessage> AddCollection(Collection collection)
        {
            return await Http.PostAsJsonAsync("api/Collection", collection);
        }
        public async Task<HttpResponseMessage> UpdateCollection(Collection collection)
        {
            return await Http.PutAsJsonAsync("api/Collection", collection);
        }
    }
}
