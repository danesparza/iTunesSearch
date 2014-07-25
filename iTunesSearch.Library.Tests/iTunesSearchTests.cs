using System;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace iTunesSearch.Library.Tests
{
    [TestClass]
    public class iTunesSearchTests
    {
        [TestMethod]
        public void GetEpisodesForShow_ValidShow_ReturnsEpisodes()
        {
            //  Arrange
            iTunesSearch search = new iTunesSearch();
            string showName = "Modern Family";

            //  Act
            var items = search.GetEpisodesForShow(showName, 200).Result;

            //  Assert
            Assert.IsTrue(items.Episodes.Any());

        }
    }
}
