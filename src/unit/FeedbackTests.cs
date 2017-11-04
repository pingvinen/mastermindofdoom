using Xunit;

namespace Pingvinen.MasterMindOfDoom
{
    public class FeedbackTests
    {
        private readonly Feedback feedback;

        public FeedbackTests()
        {
            feedback = new Feedback();
        }

        [Fact]
        public void ValueAndPositionMatches_returns_0_ifNone()
        {
            Assert.Equal(0, feedback.ValueAndPositionMatches);
        }
        
        [Fact]
        public void ValueAndPositionMatches_works()
        {
            feedback.Add(Match.ValueAndPosition);
            feedback.Add(Match.ValueOnly);
            feedback.Add(Match.ValueAndPosition);
            feedback.Add(Match.ValueAndPosition);
            
            Assert.Equal(3, feedback.ValueAndPositionMatches);
        }
        
        [Fact]
        public void ValueOnlyMatches_returns_0_ifNone()
        {
            Assert.Equal(0, feedback.ValueOnlyMatches);
        }
        
        [Fact]
        public void ValueOnlyMatches_works()
        {
            feedback.Add(Match.ValueAndPosition);
            feedback.Add(Match.ValueOnly);
            feedback.Add(Match.ValueAndPosition);
            feedback.Add(Match.ValueOnly);
            
            Assert.Equal(2, feedback.ValueOnlyMatches);
        }
    }
}