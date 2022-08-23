using TissueSample2.Server.Interfaces;
using TissueSample2.Server.Models;
using TissueSample2.Shared.Models;
using Microsoft.EntityFrameworkCore;

namespace TissueSample2.Server.Services
{

    public class CollectionManager : ICollection
    {
        readonly DatabaseContext _dbContext = new();
        public CollectionManager(DatabaseContext dbContext)
        {
            _dbContext = dbContext;
        }
        //To Get all Collection details
        public List<Collection> GetCollectionDetails()
        {
            try
            {
                return _dbContext.Collections.ToList();
            }
            catch
            {
                throw;
            }
        }
        //To Add a new collection record
        public void AddCollection(Collection collection)
        {
            try
            {
                _dbContext.Collections.Add(collection);
                _dbContext.SaveChanges();
            }
            catch
            {
                throw;
            }
        }
        //To Update the records of a particluar collection
        public void UpdateCollectionDetails(Collection collection)
        {
            try
            {
                _dbContext.Entry(collection).State = EntityState.Modified;
                _dbContext.SaveChanges();
            }
            catch
            {
                throw;
            }
        }
        //Get the details of a particular collection
        public Collection GetCollectionData(int c_id)
        {
            try
            {
                Collection? collection = _dbContext.Collections.Find(c_id);
                if (collection != null)
                {
                    return collection;
                }
                else
                {
                    throw new ArgumentNullException();
                }
            }
            catch
            {
                throw;
            }
        }
        //To Delete the record of a particular collection and all associated sample records
        public void DeleteCollection(int c_id)
        {
            try
            {
                Collection? collection = _dbContext.Collections.Find(c_id);
                if (collection != null)
                {
                    List<Sample>? samples = _dbContext.Samples.Where(m => m.c_id == c_id).ToList();
                    
                    if (samples.Count > 0)
                    {
                        _dbContext.Samples.RemoveRange(samples);
                        _dbContext.SaveChanges();
                    }
                    _dbContext.Collections.Remove(collection);
                    _dbContext.SaveChanges();
                }
                else
                {
                    throw new ArgumentNullException();
                }
            }
            catch
            {
                throw;
            }
        }
    }

}
