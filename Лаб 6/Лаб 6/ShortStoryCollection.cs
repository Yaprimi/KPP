using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Лаб_4
{
    public class ShortStoryCollection : FictionBook
    {
        public int StoryCount { get; set; }
        public bool IsThematic { get; set; }

        public ShortStoryCollection() : base()
        {
            StoryCount = 0;
            IsThematic = false;
        }

        public ShortStoryCollection(string name, decimal price, string author, string publisher,
            string genre, string targetAudience, int storyCount, bool isThematic)
            : base(name, price, author, publisher, genre, targetAudience)
        {
            StoryCount = storyCount;
            IsThematic = isThematic;
        }

        public override string ToString()
        {
            return base.ToString() + $", Кількість оповідань: {StoryCount}, Тематична збірка: {(IsThematic ? "Так" : "Ні")}";
        }
    }
}
