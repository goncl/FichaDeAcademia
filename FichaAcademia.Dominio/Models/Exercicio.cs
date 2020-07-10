using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace FichaAcademia.Dominio.Models
{
    public class Exercicio
    {
        public int ExercicioId { get; set; }

        [Required(ErrorMessage = "Campo obrigatório.")]
        [StringLength(50, ErrorMessage = "Use menos caracteres.")]
        [Remote("ExercicioExiste", "Exercicios", AdditionalFields = "ExercicioId")]
        public string Nome { get; set; }

        //indica que tem uma chave estrangeira de CategoriaExercicio em Exercicio
        public int CategoriaExercicioId { get; set; }
        public CategoriaExercicio CategoriaExercicio { get; set; }

        //indica que sera exibido uma coleção de Exercicio em ListaExercicios
        public ICollection<ListaExercicio> ListaExercicios { get; set; }
    }
}
