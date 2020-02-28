using System.ComponentModel.DataAnnotations;

namespace Wjire.Db.Dapper.SqlServer.WebApi
{
    /// <summary>
    /// Company
    /// </summary>
    public class Company
    {

        /// <summary>
        /// 
        /// </summary>
        [Key]
        public int Id { get; set; }


        /// <summary>
        /// 
        /// </summary>
        public string CompanyName { get; set; }


    }
}
