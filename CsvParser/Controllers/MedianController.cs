using Microsoft.AspNetCore.Mvc;
using MedianCalculator.Model;
using System.Collections.Generic;
using System.Linq;
using MedianCalculator.Services;

namespace MedianCalculator.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MedianController : ControllerBase
    {
        IFilesService _filesService;
        IMedianService _medianService;

        public MedianController (IFilesService filesService, IMedianService medianService)
        {
            _filesService = filesService;
            _medianService = medianService;
        }
        
        [HttpGet]
        [Route("CalculateMedian")]
        public virtual ActionResult<ResultsModel> CalculateMedian()
        {           
            //Dictionary<string, List<CSVData>> csvData = new Dictionary<string, List<CSVData>>();
            List<ResultsModel> resultData = new List<ResultsModel>();

            // read all files from the folder
            var csvDataDictionary = _filesService.GetCSVData();

            // For each file call median service to calculate Median
            foreach (KeyValuePair<string, List<CSVData>> entry in csvDataDictionary)
            {
                if (entry.Value.Count > 0)
                {
                    // Get "Data Value" Column to calculate Median
                    double? median = _medianService.CalculateMedian(entry.Value.Select(p => p.DataValue).ToList());

                    if (median.HasValue)
                    {
                        // accumulate all data in the Result collection
                        resultData.AddRange(_medianService.GetMedianVarianceResults(entry.Key, entry.Value, median.Value));
                    }
                }
            }
            
            // return result data as Json
            // pls note: this data is also written in the console from the service method.
            return Ok(resultData);                 
        }
    }
}
