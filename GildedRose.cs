using System.Collections.Generic;

namespace csharp
{
    public class GildedRose
    {
        public IList<Item> items;
        public GildedRose(IList<Item> items)
        {
            this.items = items;
        }

        public int QualityInBounds(Item item)
        {
            if (item.Quality > 50) return 50;
            if (item.Quality < 0) return 0;
            return item.Quality;
        }

        public bool SellInCheckForDays(Item item, int days)
        {
            return item.SellIn <= days;
        }
        public Item UpdateBrie(Item item, int multiplier)
        {
            item.Quality += multiplier;
            item.Quality = QualityInBounds(item);

            return item;
        }
        public Item UpdateBackstagePasses(Item item, int multiplier)
        {
            if (item.SellIn < 0)
            {
                item.Quality = 0;
                return item;
            }

            item.Quality += multiplier;

            if (SellInCheckForDays(item, 5))
            {
                item.Quality += multiplier;
            }

            if (SellInCheckForDays(item, 10))
            {
                item.Quality += multiplier;
            }

            item.Quality = QualityInBounds(item);

            return item;
        }
        public Item UpdateConjured(Item item, int multiplier)
        {
            item.Quality -= 2 * multiplier;
            item.Quality = QualityInBounds(item);

            return item;
        }
        public Item UpdateNormalItem(Item item, int multiplier)
        {
            item.Quality -= multiplier;
            item.Quality = QualityInBounds(item);

            return item;
        }
        public void UpdateQuality()
        {
            const int legendaryQuality = 80;
            for (var i = 0; i < items.Count; i++)
            {
                int multiplier = 1;

                if (items[i].SellIn < 0)
                {
                    multiplier += 1;
                }

                if (items[i].Name == "Aged Brie")
                {
                    items[i] = UpdateBrie(items[i], multiplier);
                }
                else if (items[i].Name.StartsWith("Backstage passes"))
                {
                    items[i] = UpdateBackstagePasses(items[i], multiplier);
                }
                else if (items[i].Name == "Sulfuras, Hand of Ragnaros")
                {
                    items[i].Quality = legendaryQuality;
                }
                else if (items[i].Name.StartsWith("Conjured"))
                {
                    items[i] = UpdateConjured(items[i], multiplier);
                }
                else
                {
                    items[i] = UpdateNormalItem(items[i], multiplier);
                }

                if (items[i].Name != "Sulfuras, Hand of Ragnaros")
                {

                    items[i].SellIn -= 1;
                }
            }
        }
    }
}
