using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace Survey.Entidades
{
    public class Survey
    {
        [Required]
        public string idSurvey { set; get; }
        [Required]
        public string idUsuario { set; get; }
        [Required]
        public string titulo { set; get; }
        [Required]
        public string descripcion { set; get; }
        [Required]
        public string estado { set; get; }
       
    }
}
