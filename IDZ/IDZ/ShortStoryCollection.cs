using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IDZ
{
    public class ShortStoryCollection : FictionBook
    {
        public int StoryCount { get; set; }

        public ShortStoryCollection() : base()
        {
            StoryCount = 0;
        }

        public ShortStoryCollection(string name, decimal price, string author, string publisher,
            string genre, string targetAudience, int storyCount)
            : base(name, price, author, publisher, genre, targetAudience)
        {
            StoryCount = storyCount;
        }

        public override string ToString()
        {
            return base.ToString() + $", Кількість оповідань: {StoryCount}";
        }
    }
}
