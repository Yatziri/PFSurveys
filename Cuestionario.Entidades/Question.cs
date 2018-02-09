using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
namespace Survey.Entidades
{
	public class Question
	{
		 
		public Ctl_Surveys encuesta { get; set; }

	 
		public List<Ctl_Survey_Questions> preguntas { get; set; }
	}
}
