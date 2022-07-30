using Enforsement.Core.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Enforsement.Core.Servise
{
    public static class SoketToEnforsementClass
    {
        public static EnforsementClass Convert(EnforsementClassSoket _soket)
        {
            EnforsementClass enforsementClass = new EnforsementClass();
            enforsementClass.Id = _soket.Id;
            enforsementClass.Type = _soket.Type;
            enforsementClass.CriminalNumber = _soket.CriminalNumber;
            enforsementClass.InitDate = _soket.InitDate;
            enforsementClass.ControlDate = _soket.ControlDate;
            enforsementClass.Investigator = _soket.Investigator;
            enforsementClass.Qualification = _soket.Qualification;
            enforsementClass.Court = _soket.Court;
            enforsementClass.Description = _soket.Description;
            enforsementClass.Status = _soket.Status;
            return enforsementClass;
        }
    }
}
