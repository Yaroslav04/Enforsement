using SQLite;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Enforsement.Core.Model
{
    public class EnforsementClass
    {
        [AutoIncrement]
        [PrimaryKey]
        [NotNull]
        public int Id { get; set; }
        public string Type { get; set; }
        public string CriminalNumber { get; set; }
        public DateTime InitDate { get; set; }
        public DateTime ControlDate { get; set; }
        public string Investigator { get; set; }
        public string Qualification { get; set; }
        public string Court { get; set; }
        public string Description { get; set; }
        public string Status { get; set; }
    }
}
