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
    }
}