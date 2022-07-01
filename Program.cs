using System;
using System.Collections.Generic;
using System.Threading;

namespace csharp
{
    public class Program
    {
        public static Item createItem(String name, int sellIn, int quality, bool isConjured)
        {
            Item item = new Item();
            item.Name = name;
            item.SellIn = sellIn;
            item.Quality = quality;

            if (isConjured)
            {
                item.Name = "Conjured " + name;
            }

            return item;
        }

        public static IList<Item> createTestItemList()
        {
            IList<Item> items = new List<Item>();
            items.Add(createItem("+5 Dexterity Vest", 10, 20, false));
            items.Add(createItem("Aged Brie", 2, 0, false));
            items.Add(createItem("Elixir of the Mongoose", 5, 7, false));
            items.Add(createItem("Sulfuras, Hand of Ragnaros", 0, 80, false));
            items.Add(createItem("Sulfuras, Hand of Ragnaros", -1, 80, false));
            items.Add(createItem("Backstage passes to a TAFKAL80ETC concert", 15, 20, false));
            items.Add(createItem("Backstage passes to a TAFKAL80ETC concert", 10, 49, false));
            items.Add(createItem("Backstage passes to a TAFKAL80ETC concert", 5, 49, false));
            items.Add(createItem("Conjured Mana Cake", 3, 6, true));
            return items;
        }

        public static void simulate30Days(GildedRose app)
        {
            for (var day = 0; day < 31; day++)
            {
                Console.WriteLine("-------- day " + day + " --------");
                Console.WriteLine("name, sellIn, quality");
                for (var item = 0; item < app.items.Count; item++)
                {
                    Console.WriteLine(app.items[item]);
                }
                Console.WriteLine("");
                app.UpdateQuality();
            }
        }
        public static void Main(string[] args)
        {
            IList<Item> items = createTestItemList();

            var app = new GildedRose(items);

            simulate30Days(app);
        }
    }
}
}
