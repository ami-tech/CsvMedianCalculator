using MedianCalculator.Model;
using MedianCalculator.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;

namespace MedianCalculator.Services
{
    public class MedianService : IMedianService
    {
        IDataRepository _dataRepository;

        public MedianService(IDataRepository dataRepository)
        {
            _dataRepository = dataRepository;
        }

        /// <summary>
        /// Calculates median
        /// </summary>
        /// <param name="dataValues"></param>
        /// <returns>Median value</returns>
        public double? CalculateMedian(List<double> dataValues)
        {
            int count = dataValues.Count();
            if (count == 0)
                return null;

            dataValues = dataValues.OrderBy(n => n).ToList();

            int midpoint = count / 2;
            if (count % 2 == 0)
                return (Convert.ToDouble(dataValues.ElementAt(midpoint - 1)) + Convert.ToDouble(dataValues.ElementAt(midpoint))) / 2.0;
            else
                return Convert.ToDouble(dataValues.ElementAt(midpoint));            
        }

        /// <summary>
        /// Gets the result data list which contains values which are 20% above or below the median
        /// </summary>
        /// <param name="fileName"></param>
        /// <param name="dataValues"></param>
        /// <param name="median"></param>
        /// <returns>A list of Median variances with file names</returns>        
        public List<ResultsModel> GetMedianVarianceResults(string fileName, List<CSVData> dataValues, double median)
        {
            List<ResultsModel> results = new List<ResultsModel>();
            try
            {
                double positiveVariance = median - (median * .2);
                double negativeVariance = median + (median * .2);                

                foreach (var csvData in dataValues)
                {
                    if (csvData.DataValue < negativeVariance || csvData.DataValue > positiveVariance)
                    {
                        results.Add(
                            new ResultsModel()
                            {
                                FileName = fileName,
                                DateTimeValue = csvData.DateTimeValue,
                                DataValue = csvData.DataValue,
                                MedianValue = median
                            });
                    }
                }
                _dataRepository.WriteResults(results);
            }catch (Exception ex)
            {
                Console.WriteLine("Exception occured while Calculating median and wrting results: " + ex.Message);                
            }
            return results;
        }
    }
}
