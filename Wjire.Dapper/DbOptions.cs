using System;
using System.Collections.Generic;

namespace Wjire.Dapper
{
    public class DbOptions
    {
        internal IList<IDbOptionsExtension> Extensions { get; } = new List<IDbOptionsExtension>();

        public void RegisterExtension(IDbOptionsExtension extension)
        {
            if (extension == null)
            {
                throw new ArgumentNullException(nameof(extension));
            }
            Extensions.Add(extension);
        }
    }


    public class ConnectionOptions : DbOptions
    {
        public string Read { get; set; }

        public string Write { get; set; }
    }
}
