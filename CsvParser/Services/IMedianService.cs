using MedianCalculator.Model;
using System.Collections.Generic;

namespace MedianCalculator.Services
{
    public interface IMedianService
    {
        double? CalculateMedian(List<double> dataValues);

        List<ResultsModel> GetMedianVarianceResults(string fileName, List<CSVData> dataValues, double median);
    }
}
