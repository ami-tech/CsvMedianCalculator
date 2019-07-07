using MedianCalculator.Model;
using MedianCalculator.Repositories;
using System.Collections.Generic;

namespace MedianCalculator.Services
{    
    public class FilesService: IFilesService
    {
        IDataRepository _dataRepository;

        public FilesService(IDataRepository dataRepository)
        {
            _dataRepository = dataRepository;
        }           

        public Dictionary<string, List<CSVData>> GetCSVData()
        {            
            return _dataRepository.GetCSVData();
        }
    }
}
