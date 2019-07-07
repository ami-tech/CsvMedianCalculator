using MedianCalculator.Model;
using System.Collections.Generic;

namespace MedianCalculator.Services
{
    public interface IFilesService
    {
        Dictionary<string, List<CSVData>> GetCSVData();    
    }
}
