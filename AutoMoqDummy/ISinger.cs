using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AutoMoqDummy
{
    public interface ISinger
    {
        string Sing();
    }

    public class Song {
        ISinger singer;
        public Song(ISinger singer)
        {
            this.singer = singer;
        }

        public string GetLyrics()
        {
            return this.singer.Sing();
        }
    }
}
