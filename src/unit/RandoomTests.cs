using Xunit;

namespace Pingvinen.MasterMindOfDoom
{
    public class RandoomTests
    {
        private readonly Randoom randoom;

        public RandoomTests()
        {
            randoom = new Randoom();
        }

        [Fact]
        public void Works()
        {
            var numbers = randoom.GetSequence(35, 3, 27);

            Assert.True(numbers.TrueForAll(i => i >= 3 && i <= 27), "All numbers must be in range");
            var firstRun = string.Join("", numbers);
            
            numbers = randoom.GetSequence(35, 0, 27);

            Assert.True(numbers.TrueForAll(i => i >= 0 && i <= 27), "All numbers must be in range");
            var secondRun = string.Join("", numbers);
            
            Assert.NotEqual(firstRun, secondRun);
        }
    }
}