using NUnit.Framework;
using System.Collections.Generic;

namespace csharp
{
    [TestFixture]
    public class GildedRoseTest
    {
        [Test]
        public void QualityInBounds()
        {
            // given
            IList<Item> Items = new List<Item>();
            Items.Add(new Item { Name = "Nintendo", SellIn = 1, Quality = 2 });
            Items.Add(new Item { Name = "Nintendo", SellIn = 1, Quality = 51 });
            Items.Add(new Item { Name = "Nintendo", SellIn = 1, Quality = 0 });

            GildedRose app = new GildedRose(Items);

            // when
            app.UpdateQuality();

            // then
            Assert.AreEqual(1, Items[0].Quality);
            Assert.AreEqual(50, Items[1].Quality);
            Assert.AreEqual(0, Items[2].Quality);
        }

        [Test]
        public void BrieSellInPositiveSellIn()
        {
            // given
            IList<Item> Items = new List<Item>();
            Items.Add(new Item { Name = "Aged Brie", SellIn = 1, Quality = 2 });
            Items.Add(new Item { Name = "Aged Brie", SellIn = 1, Quality = 51 });
            Items.Add(new Item { Name = "Aged Brie", SellIn = 1, Quality = 0 });
            Items.Add(new Item { Name = "Aged Brie", SellIn = 1, Quality = 49 });

            GildedRose app = new GildedRose(Items);

            // when
            app.UpdateQuality();

            // then
            Assert.AreEqual(3, Items[0].Quality);
            Assert.AreEqual(50, Items[1].Quality);
            Assert.AreEqual(1, Items[2].Quality);
            Assert.AreEqual(50, Items[3].Quality);
        }

        [Test]
        public void BrieSellInNegativeSellIn()
        {
            // given
            IList<Item> Items = new List<Item>();

            Items.Add(new Item { Name = "Aged Brie", SellIn = -1, Quality = 2 });
            Items.Add(new Item { Name = "Aged Brie", SellIn = -1, Quality = 49 });
            Items.Add(new Item { Name = "Aged Brie", SellIn = -1, Quality = 0 });

            GildedRose app = new GildedRose(Items);

            // when
            app.UpdateQuality();

            // then
            Assert.AreEqual(4, Items[0].Quality);
            Assert.AreEqual(50, Items[1].Quality);
            Assert.AreEqual(2, Items[2].Quality);
        }

        [Test]
        public void BackstagePassesUnderFiveDays()
        {
            // given
            IList<Item> Items = new List<Item>();
            Items.Add(new Item { Name = "Backstage passes thingy", SellIn = 1, Quality = 2 });
            Items.Add(new Item { Name = "Backstage passes thingy", SellIn = 5, Quality = 49 });


            GildedRose app = new GildedRose(Items);

            // when
            app.UpdateQuality();

            // then
            Assert.AreEqual(5, Items[0].Quality);
            Assert.AreEqual(50, Items[1].Quality);
        }

        [Test]
        public void BackstagePassesBetweenFiveAndTenDays()
        {
            // given
            IList<Item> Items = new List<Item>();
            Items.Add(new Item { Name = "Backstage passes thingy", SellIn = 6, Quality = 2 });
            Items.Add(new Item { Name = "Backstage passes thingy", SellIn = 9, Quality = 5 });

            GildedRose app = new GildedRose(Items);

            // when
            app.UpdateQuality();

            // then
            Assert.AreEqual(4, Items[0].Quality);
            Assert.AreEqual(7, Items[1].Quality);
        }

        [Test]
        public void BackstagePassesAboveTenDays()
        {
            // given
            IList<Item> Items = new List<Item>();
            Items.Add(new Item { Name = "Backstage passes thingy", SellIn = 11, Quality = 2 });
            Items.Add(new Item { Name = "Backstage passes thingy", SellIn = 16, Quality = 49 });

            GildedRose app = new GildedRose(Items);

            // when
            app.UpdateQuality();

            // then
            Assert.AreEqual(3, Items[0].Quality);
            Assert.AreEqual(50, Items[1].Quality);
        }

        [Test]
        public void BackstagePassesSellInNegative()
        {
            // given
            IList<Item> Items = new List<Item>();
            Items.Add(new Item { Name = "Backstage passes thingy", SellIn = -1, Quality = 2 });
            Items.Add(new Item { Name = "Backstage passes thingy", SellIn = -12, Quality = 5 });

            GildedRose app = new GildedRose(Items);

            // when
            app.UpdateQuality();

            // then
            Assert.AreEqual(0, Items[0].Quality);
            Assert.AreEqual(0, Items[1].Quality);
        }

        [Test]
        public void SulfurasSellInAndQualityDontChange()
        {
            // given
            IList<Item> Items = new List<Item>();
            Items.Add(new Item { Name = "Sulfuras, Hand of Ragnaros", SellIn = 13, Quality = 2 });

            GildedRose app = new GildedRose(Items);

            // when
            app.UpdateQuality();

            // then
            Assert.AreEqual(13, Items[0].SellIn);
            Assert.AreEqual(80, Items[0].Quality);
        }

        [Test]
        public void ConjuredItemsDecreaseTwiceAsFastInQuality()
        {
            // given
            IList<Item> Items = new List<Item>();
            Items.Add(new Item { Name = "Conjured Aged Brie", SellIn = 10, Quality = 2 });
            Items.Add(new Item { Name = "Conjured Sulfuras", SellIn = 11, Quality = 12 });
            Items.Add(new Item { Name = "Conjured backstage passes", SellIn = 2, Quality = 14 });

            GildedRose app = new GildedRose(Items);

            // when
            app.UpdateQuality();

            // then
            Assert.AreEqual(0, Items[0].Quality);
            Assert.AreEqual(10, Items[1].Quality);
            Assert.AreEqual(12, Items[2].Quality);
        }

        [Test]
        public void ConjuredItemsDecreaseFourTimesAsFastInQualityAfterSellIn()
        {
            // given
            IList<Item> Items = new List<Item>();
            Items.Add(new Item { Name = "Conjured Aged Brie", SellIn = -1, Quality = 2 });
            Items.Add(new Item { Name = "Conjured Sulfuras", SellIn = -24, Quality = 12 });
            Items.Add(new Item { Name = "Conjured backstage passes", SellIn = -2, Quality = 14 });

            GildedRose app = new GildedRose(Items);

            // when
            app.UpdateQuality();

            // then
            Assert.AreEqual(0, Items[0].Quality);
            Assert.AreEqual(8, Items[1].Quality);
            Assert.AreEqual(10, Items[2].Quality);
        }

        [Test]
        public void NormalSellInPositive()
        {
            // given
            IList<Item> Items = new List<Item>();
            Items.Add(new Item { Name = "Dell Monitor", SellIn = 1, Quality = 2 });
            Items.Add(new Item { Name = "Dell Laptop", SellIn = 1, Quality = 51 });
            Items.Add(new Item { Name = "Toshiba Docking Station", SellIn = 1, Quality = 0 });

            GildedRose app = new GildedRose(Items);

            // when
            app.UpdateQuality();

            // then
            Assert.AreEqual(1, Items[0].Quality);
            Assert.AreEqual(50, Items[1].Quality);
            Assert.AreEqual(0, Items[2].Quality);
        }

        [Test]
        public void NormalSellInNegative()
        {
            // given
            IList<Item> Items = new List<Item>();
            Items.Add(new Item { Name = "Microsoft Mouse", SellIn = -1, Quality = 4 });
            Items.Add(new Item { Name = "Microsoft Windows 10 Licence Key", SellIn = -1, Quality = 51 });
            Items.Add(new Item { Name = "Softwire mug", SellIn = -1, Quality = 0 });

            GildedRose app = new GildedRose(Items);

            // when
            app.UpdateQuality();

            // then
            Assert.AreEqual(2, Items[0].Quality);
            Assert.AreEqual(49, Items[1].Quality);
            Assert.AreEqual(0, Items[2].Quality);
        }
    }
}
