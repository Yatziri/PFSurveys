using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Survey.Entidades
{
    public class Consulta
    {
		public int id { get; set; }
        public int idSurvey { get; set; }
        public int Usuario { get; set; }
        public int Actividad { get; set; }
        public int Unidad { get; set; }
        public string NombreEncuesta { get; set; }
        public string TableName { get; set; }

    }
}
