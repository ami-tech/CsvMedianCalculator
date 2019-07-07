using System;
using Xunit;
using Moq;
using MedianCalculator.Controllers;
using MedianCalculator.Services;
using MedianCalculator.Repositories;
using System.Collections.Generic;

namespace TestController
{
    public class MedianServiceTests
    {                
        private readonly Mock<IDataRepository> _dataRepo = new Mock<IDataRepository>();
        
        public MedianServiceTests()
        {            
            _dataRepo = new Mock<IDataRepository>();
        }

        [Fact]
        public void Verify_Median_Calculation_of_All_zero_List()
        {
            //Arrange
            List<double> dataValues = MedianServiceTestHelper.Get_All_Zero_Values();
            MedianService medianService = new MedianService(_dataRepo.Object);

            //Act
            double? returnValue = medianService.CalculateMedian(dataValues);

            //Assert
            Assert.Equal(0, returnValue);
        }

        [Fact]
        public void Verify_Median_Calculation_of_Even_number_List()
        {
            //Arrange
            List<double> dataValues = MedianServiceTestHelper.Get_Even_Number_Of_Values();
            MedianService medianService = new MedianService(_dataRepo.Object);

            //Act
            double? returnValue = medianService.CalculateMedian(dataValues);

            //Assert
            Assert.Equal(4.5, returnValue);
        }

        [Fact]
        public void Verify_Median_Calculation_of_Odd_number_List()
        {
            //Arrange
            List<double> dataValues = MedianServiceTestHelper.Get_Odd_Number_Of_Values();
            MedianService medianService = new MedianService(_dataRepo.Object);

            //Act
            double? returnValue = medianService.CalculateMedian(dataValues);

            //Assert
            Assert.Equal(4, returnValue);
        }


    }
}
