using TissueSample2.Server.Interfaces;
using TissueSample2.Shared.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace TissueSample2.Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SampleController : ControllerBase
    {
        private readonly ISample _ISample;

        public SampleController(ISample iSample)
        {
            _ISample = iSample;
        }

        [HttpGet]
        public async Task<List<Sample>> GetSample([FromQuery] int cid) // get all samples associated with one collection
        {
            System.Diagnostics.Debug.Print("Request is here *********************");
            return await Task.FromResult(_ISample.GetSampleDetails(cid));
        }

        [HttpGet("{id}")]
        public IActionResult GetOneSample(int id) // get specific sample
        {
            System.Diagnostics.Debug.Print("******************* Request is here *********************");
            Sample sample = _ISample.GetSampleData(id);
            if (sample != null)
            {
                return Ok(sample);
            }
            return NotFound();
        }

        [HttpPost]
        public IActionResult PostSample(Sample sample) // add new sample
        {
            return Ok(_ISample.AddSample(sample));
        }

        [HttpPut]
        public IActionResult PutSample(Sample sample) // update one sample
        {
            return Ok(_ISample.UpdateSampleDetails(sample));
        }

        [HttpDelete("{id}")]
        public IActionResult DeleteSample(int id) // delete specific sample
        {
            return Ok(_ISample.DeleteSample(id));
        }
    }
}
