using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using StreamProcessing.Service;

namespace StreamProcessing.Controllers
{
    //[RoutePrefix("api/streamprocess")]
    public class StreamProcessController : ApiController
    {
        private readonly IStreamProcessService _streamProcessService;

        // Use Ninject for dependency injection
        public StreamProcessController(IStreamProcessService streamProcessService)
        {
            _streamProcessService = streamProcessService;
        }

        // web api for get score of stream input. could also be done in HttpPost
        [Route("api/parse")]
        [HttpGet]
        public IHttpActionResult GetScore(string input)
        {
            if (input == null)
            {
                return BadRequest();
            }
            try
            {
                _streamProcessService.SetInput(input);
                return Ok(_streamProcessService.GetScore());
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

    }
}
