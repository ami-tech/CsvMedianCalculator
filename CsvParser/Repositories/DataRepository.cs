using MedianCalculator.Configuration;
using MedianCalculator.Model;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.IO;

namespace MedianCalculator.Repositories
{
    public class DataRepository: RepositoryBase, IDataRepository
    {
        public DataRepository(IOptions<AppConfiguration> configuration) : base(configuration)
        {

        }

        /// <summary>
        /// Write results in the approprite repository
        /// </summary>
        /// <param name="results"></param>
        public void WriteResults(List<ResultsModel> results)
        {
            foreach(ResultsModel resultsModel in results)
            {
                Console.WriteLine(resultsModel.FileName + "," 
                                    + resultsModel.DateTimeValue + ","
                                    + resultsModel.DataValue + ","
                                    + resultsModel.MedianValue);
            }            
        }

        /// <summary>
        /// Reads all the files in the Folder set in configuration in appsettings.json 
        /// </summary>
        /// <returns>A dictionary with Key as File name and value as list of rows of file</returns>
        public Dictionary<string, List<CSVData>> GetCSVData()
        {            
            List<ResultsModel> resultData = new List<ResultsModel>();

            Dictionary<string, List<CSVData>> csvData = new Dictionary<string, List<CSVData>>();

            // read files from the folder
            foreach (string file in Directory.EnumerateFiles(_configuration.Value.CSVFolderPath, "*.csv"))
            {
                if (System.IO.File.Exists(file))
                {
                    csvData.Add(file, ReadCSVFile(file));
                }
            }

            return csvData;
        }

        /// <summary>
        /// Reads CSV file to get data to caculate Median
        /// </summary>
        /// <param name="FileName">File path</param>
        /// <returns>A List of CSV data to calculate median</returns>
        private List<CSVData> ReadCSVFile(string FileName)
        {
            List<CSVData> csvData = new List<CSVData>();
            int lineNumber = 0;

            using (var reader = new StreamReader(FileName))
            {
                try
                {
                    while (!reader.EndOfStream)
                    {
                        var line = reader.ReadLine();
                        lineNumber++;
                        if (lineNumber == 1) // skip the first line
                            continue;

                        var values = line.Split(',');

                        try
                        {
                            csvData.Add(new CSVData()
                            {
                                // a) "Data Value" column for the "LP" file type or 
                                // b) or the "Energy" column for the "TOU" file type
                                DataValue = double.Parse(values[5]),

                                // Date time column in Both files
                                DateTimeValue = DateTime.Parse(values[3]) 
                            });
                        }
                        catch (Exception ex)
                        {
                            // Log Parsing error
                            Console.WriteLine("Exception occured while parsing csv data: " + ex.Message);
                        }
                    }
                }
                catch (Exception ex)
                {
                    // Log File IO Error
                    Console.WriteLine("Exception occured while reading file: " + ex.Message);
                }
            }
            return csvData;
        }
    }
}
