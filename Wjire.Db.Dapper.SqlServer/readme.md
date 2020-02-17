
1.the configuration priority:
    appsettings.Development.json > appsettings.json  

      "connectionStrings": {
        "NCovRead": "Data Source=localhost;Initial Catalog=NCov;User ID=sa;Password=1",
        "NCovWrite": "Data Source=localhost;Initial Catalog=NCov;User ID=sa;Password=1"
      }

2.examples:

    NCoVDbFactory.cs:

        public static partial class NCoVDbFactory
        {
            private static readonly string NCoVRead = "NCoVRead";
            private static readonly string NCoVWrite = "NCoVWrite";

            public static IUnitOfWork CreateTransaction()
            {
                return new TransactionConnection(NCoVWrite);
            }

            public static IUnitOfWork CreateSingle()
            {
                return new SingleConnection(NCoVRead);
            }
        }


    RouteInfoRepositoryFactory.cs:

        public partial class NCoVDbFactory
        {
            public static RouteInfoRepository CreateRouteInfoRepositoryRead()
            {
                return new RouteInfoRepository(NCoVRead);
            }

            public static RouteInfoRepository CreateRouteInfoRepositoryRead(IUnitOfWork unit)
            {
                return new RouteInfoRepository(unit);
            }

            public static RouteInfoRepository CreateRouteInfoRepositoryWrite()
            {
                return new RouteInfoRepository(NCoVWrite);
            }

            public static RouteInfoRepository CreateRouteInfoRepositoryWrite(IUnitOfWork unit)
            {
                return new RouteInfoRepository(unit);
            }
        }


    RouteInfoRepository.cs:

        public class RouteInfoRepository : BaseRepository<RouteInfo>
        {
            public RouteInfoRepository(string name) : base(name) { }

            public RouteInfoRepository(IUnitOfWork unit) : base(unit) { }

            public RouteInfo GetById(int id)
            {
                return QueryFirstOrDefault<RouteInfo>($"select * from {TableName} where id=@id", new { id });
            }

            public void Update(string routeno)
            {
                Execute("update routeinfo set routeno = @routeno where id = 3", new { routeno });
            }

            public List<RouteInfo> GetRouteInfos()
            {
                return Query<RouteInfo>("select * from routeinfo").ToList();
            }
        }
          
                   
    TestLogic.cs:

        public class TestLogic
        {
            public RouteInfo Get(int id)
            {
                using (var db = NCoVDbFactory.CreateRouteInfoRepositoryRead())
                {
                    ......
                }
            }

            public void Add(RouteInfo info)
            {
                using (var db = NCoVDbFactory.CreateRouteInfoRepositoryWrite())
                {
                    ......
                }
            }

            public void LongConnectionTest()
            {
                using (var unit = NCoVDbFactory.CreateSingle())
                {
                    var infoRepo = NCoVDbFactory.CreateRouteInfoRepositoryRead(unit);
                    var addressRepo = NCoVDbFactory.CreateAddressRepositoryRead(unit);
                    ......
                }
            }

            public void TransactionTest(RouteInfo info, Address address)
            {
                using (var unit = NCoVDbFactory.CreateTransaction())
                {
                    try
                    {
                        var infoRepo = NCoVDbFactory.CreateRouteInfoRepositoryWrite(unit);
                        var addressRepo = NCoVDbFactory.CreateAddressRepositoryWrite(unit);
                        ......
                        unit.Commit();
                    }
                    catch (Exception)
                    {
                        unit.Rollback();
                    }
                }
            }
        }


    RouteInfo.cs:

        public class RouteInfo
        {
            //if the field is Increment, please add KeyAttribute for use the already method : Add(T t);
            [Key]
            public int Id { get; set; }

            public string RouteNo { get; set; }
        }



        Add(T t)
        {
            ......
            foreach (PropertyInfo property in type.GetProperties())
            {
                KeyAttribute keyAttribute = property.GetCustomAttribute<KeyAttribute>();
                if (keyAttribute == null)
                {
                    addBuilder.Append($"@{property.Name},");
                }
            }
            ......
        }


3.here is a CodeBuilder for quick create example codes
    https://github.com/wjire/Wjire.Lib/tree/master/Wjire.CodeBuilder


