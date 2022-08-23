using TissueSample2.Shared.Models;

namespace TissueSample2.Server.Interfaces
{
    public interface ISample
    {
        public List<Sample> GetSampleDetails(int c_id);
        public int AddSample(Sample sample);
        public int UpdateSampleDetails(Sample sample);
        public Sample GetSampleData(int id);
        public int DeleteSample(int id);
    }
}
