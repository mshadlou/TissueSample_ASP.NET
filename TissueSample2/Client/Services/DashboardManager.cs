using TissueSample2.Shared.Models;
using TissueSample2.Client.Services;

namespace TissueSample2.Client.Services
{
    public interface IDashboardManger
    {
        List<Sample> sampleList { get; set; }
        bool Show_CollectionForm { get; set; }
        bool Show_SampleForm { get; set; }
        bool Show_SampleTable { get; set; }
        string SearchString { get; set; }
        Collection temp_collection { get; set; }
        Sample temp_sample { get; set; }
        List<Collection> collectionList { get; set; }
        List<Collection> searchCollectionData { get; set; }

        public IModal MODAL { get; set; }

        Task OnInitializedAsync();
        Task FetchSampleTable(int c_id);
        void FilterCollection();
        void ResetSearch();
        void EditCollection(int id);
        void DeleteCollection(int id);
        void EditSample(int id);
        void DeleteSample(int id);
        Task SaveCollection();
        Task SaveSample();
        Task ApproveModal();
    }

    public class DashboardManager: IDashboardManger
    {
        private IModal _imodal;
        private readonly ICollectionService _collectionService;
        private readonly ISampleService _sampleService;
        public DashboardManager(IModal imodal, ICollectionService collectionService, ISampleService sampleService)
        {
            _imodal = imodal;
            _collectionService = collectionService;
            _sampleService = sampleService;
        }
        private List<Sample> TempsampleList;
        private bool Toggle { get; set; } = false;
        private int temp_c_id { get; set; } = 0;

        #region Paraneters
        public Collection temp_collection { get; set; } = new();
        public Sample temp_sample { get; set; } = new(0, 0, 0, "");
        public List<Collection> collectionList { get; set; } = new();
        public List<Collection> searchCollectionData { get; set; } = new();

        public bool Show_CollectionForm { get; set; } = true;
        public bool Show_SampleForm { get; set; } = false;
        public bool Show_SampleTable { get; set; } = false;
        
        public string SearchString { get; set; } = string.Empty;
        
        public IModal MODAL { get { return _imodal; } set { _imodal = value; } }

        public List<Sample> sampleList
        {
            get { return TempsampleList; }
            set
            {
                TempsampleList = value;
                if (value[0].id != 0)
                {
                    TempsampleList = value;
                }
                else
                {
                    TempsampleList = new();
                }
                ViewSample();
            }
        }
        #endregion
        ///////////////////////////////////////////////////////////////////////////////////////////////////////////////
        public async Task OnInitializedAsync()
        {
            await GetAllCollections();
            _imodal.ViewModal("", false);
            ResetView();
        }
        ///////////////////////////////////////////////////////////////////////////////////////////////////////////////
        protected async Task GetAllCollections()
        {
            collectionList = await _collectionService.GetCollections();
            searchCollectionData = collectionList;
        }

        protected async Task GetAllSamples(int c_id)
        {
            sampleList = await _sampleService.GetSamples(c_id);
        }

        ///////////////////////////////////////////////////////////////////////////////////////////////////////////////
        public async Task FetchSampleTable(int c_id)
        {
            temp_collection = collectionList.FirstOrDefault(x => x.c_id == c_id);
            temp_sample = new(0, 0, 0, "");
            if (temp_c_id != c_id)
            {
                await GetAllSamples(c_id).ContinueWith(t =>
                {
                    temp_c_id = c_id;
                    Toggle = true;
                });
            }
            else
            {
                if (Toggle)
                {
                    Toggle = false;
                    ResetView();
                }
                else
                {
                    Toggle = true;
                    await GetAllSamples(c_id);
                }
            }
        }

        ///////////////////////////////////////////////////////////////////////////////////////////////////////////////

        public void FilterCollection()
        {
            if (!string.IsNullOrEmpty(SearchString))
            {
                collectionList = searchCollectionData
                    .Where(x => x.title.IndexOf(SearchString, StringComparison.OrdinalIgnoreCase) != -1 || x.disease_term.IndexOf(SearchString, StringComparison.OrdinalIgnoreCase) != -1)
                    .ToList();
            }
            else
            {
                collectionList = searchCollectionData;
            }
        }

        public void ResetSearch()
        {
            SearchString = string.Empty;
            collectionList = searchCollectionData;
        }

        ///////////////////////////////////////////////////////////////////////////////////////////////////////////////
        
        public void EditCollection(int id)
        {
            ResetView();
            temp_collection = collectionList.FirstOrDefault(x => x.c_id == id);
            Console.WriteLine(temp_collection.c_id);
        }

        public void DeleteCollection(int id)
        {
            _imodal.ViewModal("This will delete both Collection and all associated Samples permanently!", true, collectionList.FirstOrDefault(x => x.c_id == id));
        }

        public void EditSample(int id)
        {
            temp_sample = sampleList.FirstOrDefault(x => x.id == id);
        }

        public void DeleteSample(int id)
        {
            _imodal.ViewModal("This will delete the Sample data permanently!", true, sampleList.FirstOrDefault(x => x.id == id));
        }

        ///////////////////////////////////////////////////////////////////////////////////////////////////////////////
        public async Task SaveCollection()
        {
            if (temp_collection.c_id != 0)
            {
                Console.WriteLine($" Edit Collection; ID:  {temp_collection.c_id}, Term: {temp_collection.disease_term}");
                using (var resp = await _collectionService.UpdateCollection(temp_collection))
                {
                    if (resp.IsSuccessStatusCode)
                    {
                        await GetAllCollections().ContinueWith(t =>
                        {
                            temp_collection = new();
                        });
                    }
                }
            }
            else
            {
                Console.WriteLine($" NEW Collection; ID:  {temp_collection.c_id}, Term: {temp_collection.disease_term}");
                using (var resp = await _collectionService.AddCollection(temp_collection))
                {
                    if (resp.IsSuccessStatusCode)
                    {
                        await GetAllCollections().ContinueWith(t =>
                        {
                            temp_collection = new();
                        });
                    }
                }
            }
        }

        public async Task SaveSample()
        {
            if (temp_sample.id != 0)
            {
                Console.WriteLine($" Edit Sample; ID:  {temp_sample.id}, CID: {temp_sample.c_id}");
                using (var resp = await _sampleService.UpdateSample(temp_sample))
                {
                    if (resp.IsSuccessStatusCode)
                    {
                        await GetAllSamples(temp_sample.c_id).ContinueWith(t =>
                        {
                            temp_sample = new(0, 0, 0, "");
                        });
                    }
                }
            }
            else
            {
                temp_sample.c_id = temp_collection.c_id;
                Console.WriteLine($" NEW Sample; ID:  {temp_sample.id}, CID: {temp_sample.c_id}");
                using (var resp = await _sampleService.AddSample(temp_sample))
                {
                    if (resp.IsSuccessStatusCode)
                    {
                        await GetAllSamples(temp_sample.c_id).ContinueWith(t =>
                        {
                            temp_sample = new(0, 0, 0, "");
                        });
                    }
                }
            }
        }
        ///////////////////////////////////////////////////////////////////////////////////////////////////////////////
        // modal warning
        
        public async Task ApproveModal()
        {
            _imodal.ViewModal("", false);
            if (_imodal.OBJ.GetType().Equals(typeof(Collection)))
            {
                Collection temp = (Collection)_imodal.OBJ;
                using (var resp = await _collectionService.DeleteCollection(temp.c_id))
                {
                    if (resp.IsSuccessStatusCode)
                    {
                        await GetAllCollections().ContinueWith(t =>
                        {
                            temp_collection = new();
                            ResetView();
                        });
                    }
                }
                temp = null;
            }
            if (_imodal.OBJ.GetType().Equals(typeof(Sample)))
            {
                Sample temp = (Sample)_imodal.OBJ;
                using (var resp = await _sampleService.DeleteSample(temp.id))
                {
                    if (resp.IsSuccessStatusCode)
                    {
                        await GetAllSamples(temp.c_id);
                    }
                }
                temp = null;
            }
        }
        ///////////////////////////////////////////////////////////////////////////////////////////////////////////////
        
        private void ResetView()
        {
            
            Show_CollectionForm = true;
            Show_SampleForm = false;
            Show_SampleTable = false;
            TempsampleList = new();
            temp_collection = new();

        }

        private void ViewSample()
        {
            Show_CollectionForm = false;
            Show_SampleTable = true;
            Show_SampleForm = true;
        }

    }
}
