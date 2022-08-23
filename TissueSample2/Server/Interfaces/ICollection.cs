using TissueSample2.Shared.Models;

namespace TissueSample2.Server.Interfaces
{
    public interface ICollection
    {
        public List<Collection> GetCollectionDetails();
        public void AddCollection(Collection collection);
        public void UpdateCollectionDetails(Collection collection);
        public Collection GetCollectionData(int c_id);
        public void DeleteCollection(int c_id);
    }
}