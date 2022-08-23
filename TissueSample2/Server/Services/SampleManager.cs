using TissueSample2.Server.Interfaces;
using TissueSample2.Server.Models;
using TissueSample2.Shared.Models;
using Microsoft.EntityFrameworkCore;

namespace TissueSample2.Server.Services
{
    public class SampleManager : ISample
    {
        readonly DatabaseContext _dbContext = new();
        public SampleManager(DatabaseContext dbContext)
        {
            _dbContext = dbContext;
        }

        //To Get all Sample details according to collection id
        public List<Sample> GetSampleDetails(int c_id)
        {
            try
            {
                List<Sample>? temp = _dbContext.Samples.Where(m => m.c_id == c_id).ToList();
                if (temp.Count > 0)
                {
                    return temp;
                } else
                {
                    temp = new List<Sample>();
                    temp.Add(new Sample(0, 0, 0, ""));
                    return temp;
                }
                //return _dbContext.Samples.ToList();
            }
            catch
            {
                throw;
            }
        }

        //To Add a new sample record
        public int AddSample(Sample sample)
        {
            sample.date = DateTime.Now;
            try
            {                
                _dbContext.Samples.Add(sample);
                return _dbContext.SaveChanges();
            }
            catch
            {
                throw;
            }
        }

        //To Update the records of a particluar collection
        public int UpdateSampleDetails(Sample sample)
        {
            try
            {
                _dbContext.Entry(sample).State = EntityState.Modified;
                return _dbContext.SaveChanges();
            }
            catch
            {
                throw;
            }
        }

        //Get the details of a particular sample
        public Sample GetSampleData(int id)
        {
            try
            {
                Sample? sample = _dbContext.Samples.Find(id);
                if (sample != null)
                {
                    return sample;
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

        //To Delete the record of a particular sample
        public int DeleteSample(int id)
        {
            try
            {
                Sample? sample = _dbContext.Samples.Find(id);
                if (sample != null)
                {
                    _dbContext.Samples.Remove(sample);
                    return _dbContext.SaveChanges();
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
