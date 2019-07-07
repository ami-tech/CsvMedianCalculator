using MedianCalculator.Model;
using System.Collections.Generic;

namespace MedianCalculator.Repositories
{
    public interface IDataRepository
    {
        Dictionary<string, List<CSVData>> GetCSVData();
        void WriteResults(List<ResultsModel> results);
    }
}
