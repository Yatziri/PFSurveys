using System.ComponentModel.DataAnnotations;



namespace Survey.Entidades
{
    public class Usuario
    {
        [Required]
        public string id { set; get; }
        [Required]
        public string email { set; get; }

        [Required]
        public string password { set; get; }

        [Required]
        public string name { set; get; }
        [Required]
        public string lastName { set; get; }
        [Required]
        public string surName { set; get; }
        [Required]
        public string status { set; get; }
      

    }
}
