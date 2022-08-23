using TissueSample2.Server.Interfaces;
using TissueSample2.Shared.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace TissueSample2.Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CollectionController : ControllerBase
    {
        private readonly ICollection _ICollection;

        public CollectionController(ICollection iColl)
        {
            _ICollection = iColl;
        }

        [HttpGet]
        public async Task<List<Collection>> GetCollection() // get all collections
        {
            //System.Diagnostics.Debug.Print("Request is here *********************");
            return await Task.FromResult(_ICollection.GetCollectionDetails());
        }

        [HttpGet("{id}")]
        public IActionResult GetCollection(int id) // get specific collection
        {
            Collection collection = _ICollection.GetCollectionData(id);
            if (collection != null)
            {
                return Ok(collection);
            }
            return NotFound();
        }

        [HttpPost]
        public void PostCollection(Collection collection) // add new collection
        {
            _ICollection.AddCollection(collection);
        }

        [HttpPut]
        public void PutCollection(Collection collection) // update collection
        {
            _ICollection.UpdateCollectionDetails(collection);
        }

        [HttpDelete("{id}")]
        public IActionResult DeleteCollection(int id) // delete specific collection
        {
            _ICollection.DeleteCollection(id);
            return Ok();
        }
    }
}
