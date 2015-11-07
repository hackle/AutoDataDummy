using Moq;
using NUnit.Framework;
using Ploeh.AutoFixture;
using Ploeh.AutoFixture.AutoMoq;
using Ploeh.AutoFixture.NUnit2;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Reflection;
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
        [TestCase]
        public void TestAnyInt(int randomNumber)
        {
            Assert.That(randomNumber, Is.Not.Null);
        }

        [Test, AutoMoqData]
        public void TestAutoMoqFixture([Frozen]ISinger singer, string lyrics, Song song)
        {
            Mock.Get(singer).Setup(s => s.Sing()).Returns(lyrics);
            Assert.That(song.GetLyrics(), Is.EqualTo(lyrics));
        }

        [Test]
        public void Intro()
        {
            var fixture = new Fixture();
            var song = fixture.Customize(new AutoMoqCustomization()).Create<Song>();

            var field = song.GetType().GetFields(BindingFlags.Instance | BindingFlags.NonPublic).First(f => f.FieldType == typeof(ISinger));
            var singer = field.GetValue(song) as ISinger;

            string lyrics = fixture.Create<string>();
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
