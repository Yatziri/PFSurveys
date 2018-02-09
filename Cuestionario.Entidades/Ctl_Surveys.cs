using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;

namespace Survey.Entidades
{
    public class Ctl_Surveys
    {
        public int idSurvey { get; set; }
        public int idActivity { get; set; }
        public int idUnit { get; set; }

        public DateTime creationDate { get; set; }
        public DateTime closeDate { get; set; }

        [AllowHtml]
        public string name { get; set; }
        public int status { get; set; }

    }
}
