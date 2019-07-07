using System;
using System.Collections.Generic;
using System.Text;

namespace TestController
{
    public static class MedianServiceTestHelper
    {
        public static List<double> Get_Odd_Number_Of_Values()
        {
            return new List<double>() { 8, 1, 6, 3, 4, 5, 2, 7, 0 };
        }

        public static List<double> Get_Even_Number_Of_Values()
        {
            return new List<double>() { 8, 1, 6, 3, 4, 5, 2, 7, 0, 9 };
        }

        public static List<double> Get_All_Zero_Values()
        {
            return new List<double>() { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 };
        }

    }
}
