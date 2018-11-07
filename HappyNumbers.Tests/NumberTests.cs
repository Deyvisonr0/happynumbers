using System;
using System.Collections.Generic;
using System.Numerics;
using HappyNumbers.Domain.Entities;
using Xunit;

namespace HappyNumbers.Tests
{
    public class NumberTests
    {
        /// <summary>
        /// int / item1 = Number, bool / item2 = Lucky, bool / item3 = happy
        /// </summary>
        private readonly List<Tuple<int, bool, bool>> _expectList = new List<Tuple<int, bool, bool>>
        {
            new Tuple<int, bool, bool>(7,true,true),
            new Tuple<int, bool, bool>(21,true,false),
            new Tuple<int, bool, bool>(28,false,true),
            new Tuple<int, bool, bool>(142,false,false),
            new Tuple<int, bool, bool>(37,true,false),
            new Tuple<int, bool, bool>(100,false,true)
        };

        [Fact]
        public void NumberGreaterThanRangeShouldInvalidateEntity()
        {
            var number = new Number(30, 1, 25);
             
            Assert.False(number.Valid, "Invalid number or range");
        }

        [Fact]
        public void ValidNumberAndRangeShouldWork()
        {
            try
            {
                foreach (var expect in _expectList)
                {
                    var max = expect.Item1 > 25 ? expect.Item1 + 1 : 25;
                    var number = new Number(expect.Item1, 1, max);

                    Assert.Equal(expect.Item2, number.Lucky());
                    Assert.Equal(expect.Item3, number.Happy());
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }

        }
    }
}
