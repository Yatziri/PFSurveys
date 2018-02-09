using System.ComponentModel.DataAnnotations;


namespace Survey.Entidades
{
    public class Pregunta
    {
        [Required]
        public string idPregunta { set; get; }
        [Required]
        public string idSurvey { set; get; }
        [Required]
        public string idTipoPregunta { set; get; }
        [Required]
        public string enunciado { set; get; }
    }
}
