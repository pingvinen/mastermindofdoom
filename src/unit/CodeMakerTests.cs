using System;
using System.Collections.Generic;
using FakeItEasy;
using Xunit;

namespace Pingvinen.MasterMindOfDoom
{
    public class CodeMakerTests
    {
        private readonly CodeMaker maker;
        private readonly Randoom randoom;

        public CodeMakerTests()
        {
            randoom = A.Fake<Randoom>();

            A.CallTo(() => randoom.GetSequence(A<int>._, A<int>._, A<int>._)).Returns(new List<int> { 4 });
            
            maker = new CodeMaker(randoom);
        }

        #region Generate code
        [Fact]
        public void GenerateCode_throwsIfAlreadyHasACode()
        {
            maker.Code = new Code();

            Assert.Throws<InvalidOperationException>(() => maker.GenerateCode(4, 6));
        }

        [Fact]
        public void GenerateCode_usesRandoom_toGetValues()
        {
            maker.GenerateCode(15, 666);

            A.CallTo(() => randoom.GetSequence(15, 0, 666)).MustHaveHappened();
        }
        #endregion

        #region Check guess
        [Fact]
        public void CheckGuess_throws_ifDifferentLength()
        {
            maker.Code = new Code(1, 2, 3, 4);
            var guess = new Code(6);

            Assert.Throws<ArgumentException>(() => maker.CheckGuess(guess));
        }
        
        [Fact]
        public void CheckGuess_noMatches_whenNothingMatches()
        {
            maker.Code = new Code(1, 2, 3, 4);
            var guess = new Code(6, 6, 6, 6);

            var actual = maker.CheckGuess(guess);
            
            Assert.Empty(actual.Matches);
        }
        
        [Fact]
        public void CheckGuess_oneValue_oneValueAndPos()
        {
            maker.Code = new Code(1, 2, 3, 4);
            var guess = new Code(1, 4, 6, 6);

            var actual = maker.CheckGuess(guess);

            Assert.Equal(2, actual.Matches.Count);
            Assert.Equal(1, actual.ValueOnlyMatches);
            Assert.Equal(1, actual.ValueAndPositionMatches);
        }
        
        [Fact]
        public void CheckGuess_doNotOverrepresent_valueOnly()
        {
            maker.Code = new Code(1, 2, 3, 4);
            var guess = new Code(4, 4, 4, 6);

            var actual = maker.CheckGuess(guess);

            Assert.Equal(1, actual.Matches.Count);
            Assert.Equal(1, actual.ValueOnlyMatches);
            Assert.Equal(0, actual.ValueAndPositionMatches);
        }
        
        [Fact]
        public void CheckGuess_fullMatch()
        {
            maker.Code = new Code(1, 1, 1, 1);
            var guess = new Code(1, 1, 1, 1);

            var actual = maker.CheckGuess(guess);

            Assert.Equal(4, actual.Matches.Count);
            Assert.Equal(0, actual.ValueOnlyMatches);
            Assert.Equal(4, actual.ValueAndPositionMatches);
        }
        
        [Fact]
        public void CheckGuess_exactMatchesTakesPrecedence()
        {
            maker.Code = new Code(3, 3, 0, 3);
            var guess = new Code(3, 3, 3, 3);

            var actual = maker.CheckGuess(guess);

            Assert.Equal(3, actual.Matches.Count);
            Assert.Equal(0, actual.ValueOnlyMatches);
            Assert.Equal(3, actual.ValueAndPositionMatches);
        }
        #endregion
    }
}