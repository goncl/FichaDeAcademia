using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace FichaAcademia.Dominio.Models
{
    public class Aluno
    {
        public int AlunoId { get; set; }

        [Required(ErrorMessage = "Campo obrigatório.")]
        [StringLength(100, ErrorMessage = "Use menos caracteres.")]
        [Remote("AlunoExiste", "Alunos", AdditionalFields = "AlunoId")]
        public string NomeCompleto { get; set; }

        [Required(ErrorMessage = "Campo obrigatório.")]
        [Range(14, 100, ErrorMessage = "Idade inválida.")]
        public int Idade { get; set; }

        [Required(ErrorMessage = "Campo obrigatório.")]
        [Range(10, 250, ErrorMessage = "Peso inválido.")]
        public double Peso { get; set; }

        //indica que tem uma chave estrangeira de objetivo em aluno
        public int ObjetivoId { get; set; }
        public Objetivo Objetivo { get; set; }

        //indica que tem uma chave estrangeira de professor em aluno
        public int ProfessorId { get; set; }
        public Professor Professor { get; set; }

        [Required(ErrorMessage = "Campo obrigatório.")]
        [Range(1, 7, ErrorMessage = "Frequencia inválida.")]
        public int FrequenciaSemanal { get; set; }

        //indica que sera exibido uma coleção de Aluno em Fichas
        public ICollection<Ficha> Fichas { get; set; }
    }
}
