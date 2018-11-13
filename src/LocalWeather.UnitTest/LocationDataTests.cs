using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Text;
using LocalWeather.Business;
using LocalWeather.Domain;

namespace LocalWeather.UnitTest
{
    [TestClass]
    public class LocationDataTests
    {
        [TestMethod]
        public void SetSwedishLocations_AddLocation_LocationsAreEqualToOne()
        {
            //Arrange
            var locationData = new LocationData();
            var locations = new List<Location>();
            
            //Act
            locations.Add(new Location { Display_Name = "Sverige"});
            locationData.SetSwedishLocations(locations);

            //Assert
            Assert.AreEqual(1, locations.Count);
        }

        [TestMethod]
        public void SetSwedishLocations_AddThreeLocations_LocationAreEqualToThree()
        {
            //Arrange
            var locationData = new LocationData();
            var locations = new List<Location>();

            //Act
            locations.Add(new Location { Display_Name = "Sverige" });
            locations.Add(new Location { Display_Name = "Sverige" });
            locations.Add(new Location { Display_Name = "Sverige" });
            locationData.SetSwedishLocations(locations);

            //Assert
            Assert.AreEqual(3, locations.Count);
        }

        [TestMethod]
        public void GetSwedishLocations_OneLocationInList_ListAreEqualToOne()
        {
            //Arrange
            var locationData = new LocationData();
            var locations = new List<Location>();
            
            //Act
            locations.Add(new Location { Display_Name = "Sverige" });

            //Assert
            Assert.AreEqual(1, locations.Count);
        }

        [TestMethod]
        public void GetSwedishLocations_ThreeLocationInList_ListAreEqualToThree()
        {
            //Arrange
            var locationData = new LocationData();
            var locations = new List<Location>();

            //Act
            locations.Add(new Location { Display_Name = "Sverige" });
            locations.Add(new Location { Display_Name = "Sverige" });
            locations.Add(new Location { Display_Name = "Sverige" });

            //Assert
            Assert.AreEqual(3, locations.Count);
        }
    }
}
