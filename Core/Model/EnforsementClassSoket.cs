using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Enforsement.Core.Model
{
    public class EnforsementClassSoket : EnforsementClass
    {
        public string InitDateSoket { get; set; }
        public string ControlDateSoket { get; set; }
        public int Days { get; set; }
        public string TypeIcon {get; set; }
        public string TermIcon { get; set; }

        public EnforsementClassSoket(EnforsementClass enforsementClass)
        {
            Id = enforsementClass.Id;
            Type = enforsementClass.Type;
            CriminalNumber = enforsementClass.CriminalNumber;
            InitDate = enforsementClass.InitDate;
            ControlDate = enforsementClass.ControlDate;
            Investigator = enforsementClass.Investigator;
            Qualification = enforsementClass.Qualification;
            Court = enforsementClass.Court;
            Description = enforsementClass.Description;
            Status = enforsementClass.Status;     
        }

    }
}
