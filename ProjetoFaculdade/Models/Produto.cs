using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ProjetoFaculdade.Models
{
    [Table("Produto")]
    public class Produto
    {
        [Key]
        public int Id { get; set; }

        [Required(ErrorMessage = "Por favor, informe a descrição")]
        public string Descricao { get; set; }

        [Required(ErrorMessage = "Por favor, informe a quantidade")]
        public string Quantidade { get; set; }

        [Required(ErrorMessage = "Por favor, informe o preco")]
        public string Preco { get; set; }

        [ValidateNever]
        public Fornecedor Fornecedor { get; set; }

    }
}
