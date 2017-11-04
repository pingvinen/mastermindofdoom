using System;
using System.Collections.Generic;
using System.Security.Cryptography;

namespace Pingvinen.MasterMindOfDoom
{
    /// <summary>
    /// Random generator of doom
    /// </summary>
    public class Randoom
    {
        private readonly RandomNumberGenerator rng;
        
        public Randoom()
        {
            rng = RandomNumberGenerator.Create();
        }

        /// <summary>
        /// Get a random number in a given range
        /// </summary>
        /// <param name="min">Minimum value</param>
        /// <param name="max">Maximum value</param>
        /// <returns>A value in the given range</returns>
        public virtual int Get(int min, int max)
        {
            // taken from http://csharphelper.com/blog/2014/08/use-a-cryptographic-random-number-generator-in-c/

            var scale = uint.MaxValue;

            while (scale == uint.MaxValue)
            {
                // Get four random bytes.
                var fourBytes = new byte[4];
                rng.GetBytes(fourBytes);


                // Convert that into an uint.
                scale = BitConverter.ToUInt32(fourBytes, 0);
            }

            // Add min to the scaled difference between max and min.
            return (int) (min + (max - min) * (scale / (double) uint.MaxValue));
        }

        /// <summary>
        /// Get a sequence of random numbers in the given range
        /// </summary>
        /// <param name="length">How many numbers to get</param>
        /// <param name="min">Minimum value</param>
        /// <param name="max">Maximum value</param>
        /// <returns>A list of numbers from the given range</returns>
        public virtual List<int> GetSequence(int length, int min, int max)
        {
            var res = new List<int>(length);

            for (var i = 0; i < length; i++)
            {
                res.Add(Get(min, max));
            }
            
            return res;
        }
    }
}