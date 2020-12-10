using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace TCC.Models
{
        [Table("Receita")]
        public class Receita
        {

        [Key]
        public int Id { get; set; }

        [Required(ErrorMessage = "A Receita não está nomeada")]
        public string Título { get; set; }

        [Required(ErrorMessage = "A Receita está sem seus ingredientes")]
        public string Ingredientes { get; set; }

        [Required(ErrorMessage = "A Receita não possui um método de preparo")]
        public string Preparo { get; set; }

        [StringLength(300)]
        public string FotoRe { get; set; }

        }

}
