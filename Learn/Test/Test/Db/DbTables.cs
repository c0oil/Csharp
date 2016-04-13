using System.Data.Linq.Mapping;

namespace Test.Db
{
    public class DbTables
    {
        [Table(Name = "Customers")]
        public class Customer
        {
            private string customerId;

            [Column(IsPrimaryKey = true, Storage = "customerId", DbType = "NCHAR (5)")]
            public string CustomerID
            {
                get { return customerId; }
                set { customerId = value; }
            }

            private string city;

            [Column(Storage = "city", DbType = "NVARCHAR (15)")]
            public string City
            {
                get { return city; }
                set { city = value; }
            }
        }

        [Table(Name = "Territories")]
        public class Territories
        {
            private string territoryID;

            [Column(IsPrimaryKey = true, Storage = "TerritoryID")]
            public string TerritoryID
            {
                get { return territoryID; }
                set { territoryID = value; }
            }

            private string territoryDescription;

            [Column(Storage = "TerritoryDescription")]
            public string TerritoryDescription
            {
                get { return territoryDescription; }
                set { territoryDescription = value; }
            }

            private int regionID;

            [Column(Storage = "RegionID")]
            public int RegionID
            {
                get { return regionID; }
                set { regionID = value; }
            }
        }
    }
}
