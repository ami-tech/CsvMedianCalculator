using MedianCalculator.Configuration;
using Microsoft.Extensions.Options;

namespace MedianCalculator.Repositories
{
    public class RepositoryBase
    {
        protected readonly IOptions<AppConfiguration> _configuration;

        protected RepositoryBase(IOptions<AppConfiguration> configuration)
        {
            _configuration = configuration;
        }        
    }
}
