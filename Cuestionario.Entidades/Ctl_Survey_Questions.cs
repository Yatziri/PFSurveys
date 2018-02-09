using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Survey.Entidades
{
    public class Ctl_Survey_Questions
    {
        public int idSurvey { get; set; }
        public int id { get; set; }
        public int numericalOrder { get; set; }
        public string question { get; set; }

        public int idQuestionType { get; set; }

        public string questionType { get; set; }
        public int textLength { get; set; }
        public string optionQuestion { get; set; }

        public string respuesta { get; set; }

        public string respuestas { get; set; }
    }

   
}
