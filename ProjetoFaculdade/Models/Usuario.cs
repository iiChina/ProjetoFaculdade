using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;

namespace ProjetoFaculdade.Models
{
    public class Usuario
    {
        [Key]
        public int Id { get; set; }

        [Required(ErrorMessage = "Por favor, informe o Nome")]
        public string Nome { get; set; }

        [Required(ErrorMessage = "Por favor, informe o Login")]
        public string Login { get; set; }

        [Required(ErrorMessage = "Por favor, informe a Senha")]
        [DataType(DataType.Password)]
        public string Senha { get; set; }

        [Required(ErrorMessage = "Por favor, informe o Nível de acesso")]
        public char Nivel { get; set; }
    }
}
