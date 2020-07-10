using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace FichaAcademia.Dominio.Models
{
    public class Ficha
    {
        public int FichaId { get; set; }

        [Required(ErrorMessage = "Campo obrigatório.")]
        [StringLength(50, ErrorMessage = "Use menos caracteres.")]
        [Remote("FichaExiste", "Fichas", AdditionalFields = "FichaId,AlunoId")]
        public string Nome { get; set; }

        public DateTime Cadastro { get; set; }
        public DateTime Validade { get; set; }

        //indica que tem uma chave estrangeira de Aluno em Ficha
        public int AlunoId { get; set; }
        public Aluno Aluno { get; set; }

        //indica que sera exibido uma coleção de Ficha em ListaExercicios
        public ICollection<ListaExercicio> ListaExercicios { get; set; }
    }
}
