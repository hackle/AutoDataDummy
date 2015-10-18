using Moq;
using NUnit.Framework;
using Ploeh.AutoFixture;
using Ploeh.AutoFixture.AutoMoq;
using Ploeh.AutoFixture.NUnit2;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AutoMoqDummy
{
    public class Class1
    {
        [Test]
        public void ThisShouldPass()
        {
            Assert.That(true, Is.True);
        }

        [Theory, AutoData]
        public void TestAnyInt(int randomNumber)
        {
            Assert.That(randomNumber, Is.Not.Null);
        }

        [Theory, AutoMoqData]
        public void TestAutoMoqFixture([Frozen]ISinger singer, string lyrics, Song song)
        {
            Mock.Get(singer).Setup(s => s.Sing()).Returns(lyrics);
            Assert.That(song.GetLyrics(), Is.EqualTo(lyrics));
        }
    }

    public class AutoMoqDataAttribute : AutoDataAttribute
    {
        public AutoMoqDataAttribute() : base(new Fixture().Customize(new AutoMoqCustomization()))
        {

        }
    }
}
