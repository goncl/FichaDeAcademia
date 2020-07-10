using FichaAcademia.AcessoDados.Interfaces;
using FichaAcademia.Dominio.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace FichaAcademia.AcessoDados.Repositorios
{
    public class ExercicioRepositorio : RepositorioGenerico<Exercicio>, IExercicioRepositorio
    {
        private readonly Contexto _contexto;

        public ExercicioRepositorio(Contexto contexto) : base(contexto)
        {
            _contexto = contexto;
        }

        public async Task<bool> ExecicioExiste(string nome)
        {
            return await _contexto.Exercicios.AnyAsync(e => e.Nome == nome);
        }

        public async Task<bool> ExecicioExiste(string nome, int ExercicioId)
        {
            return await _contexto.Exercicios.AnyAsync(e => e.Nome == nome && e.ExercicioId == ExercicioId);
        }

        public new async Task<IEnumerable<Exercicio>> PegarTodos()
        {
            return await _contexto.Exercicios.Include(e => e.CategoriaExercicio).ToListAsync();
        }
    }
}
