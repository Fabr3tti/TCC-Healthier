using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace TCC.Models
{
    [Table("Fruta")]
    public class Fruta
    {
        [Key]
        public int Id { get; set; }

        [Required(ErrorMessage = "A Fruta está sem nome")]
        public string Nome { get; set; }

        [Required(ErrorMessage = "A Fruta está sem seus benefícios")]
        public string Benefício1 { get; set; }

        [Required(ErrorMessage = "A Fruta está sem seus benefícios")]
        public string Benefício2 { get; set; }

        [Required(ErrorMessage = "A Fruta está sem seus benefícios")]
        public string Benefício3 { get; set; }
        public string Benefício4 { get; set; }
        public string Benefício5 { get; set; }
        public string Benefício6 { get; set; }


        [StringLength(300)]
        public string Foto { get; set; }

    }
}
