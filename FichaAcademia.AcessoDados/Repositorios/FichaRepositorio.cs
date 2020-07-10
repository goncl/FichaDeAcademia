using FichaAcademia.AcessoDados.Interfaces;
using FichaAcademia.Dominio.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FichaAcademia.AcessoDados.Repositorios
{
    public class FichaRepositorio : RepositorioGenerico<Ficha>, IFichaRepositorio
    {
        public readonly Contexto _contexto;

        public FichaRepositorio(Contexto contexto) : base(contexto)
        {
            _contexto = contexto;
        }

        public async Task<bool> FichaExiste(string Nome, int AlunoId)
        {
            return await _contexto.Fichas.AnyAsync(f => f.Nome == Nome && f.AlunoId == AlunoId);
        }

        public async Task<bool> FichaExiste(string Nome, int FichaId, int AlunoId)
        {
            return await _contexto.Fichas.AnyAsync(f => f.Nome == Nome && f.FichaId != FichaId && f.AlunoId == AlunoId);
        }

        public async Task<Ficha> PegarFichaAlunoPeloId(int id)
        {
            return await _contexto.Fichas.Include(f => f.Aluno).FirstOrDefaultAsync(f => f.FichaId == id);
        }

        public async Task<IEnumerable<Ficha>> PegarTodasFichasPeloAlunoId(int id)
        {
            return await _contexto.Fichas.Include(f => f.Aluno).ThenInclude(f => f.Objetivo).Where(f => f.AlunoId == id).ToListAsync();
        }
    }
}
