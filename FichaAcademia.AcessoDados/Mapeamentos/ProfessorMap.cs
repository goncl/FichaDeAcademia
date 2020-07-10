using FichaAcademia.Dominio.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace FichaAcademia.AcessoDados.Mapeamentos
{
    public class ProfessorMap : IEntityTypeConfiguration<Professor>
    {
        public void Configure(EntityTypeBuilder<Professor> builder)
        {
            //determina a chave primaria
            builder.HasKey(p => p.ProfessorId);

            //A propriedade nome tem no maximo 50 caracteres e é obrigatorio.
            builder.Property(p => p.Nome).HasMaxLength(50).IsRequired();
            builder.Property(p => p.Turno).HasMaxLength(15).IsRequired();
            builder.Property(p => p.Foto).IsRequired();
            builder.Property(p => p.Telefone).IsRequired();

            //pode ter varios alunos mas só um professor
            builder.HasMany(p => p.Alunos).WithOne(p => p.Professor);

            //determina o nome da tabela
            builder.ToTable("Professores");
        }
    }
}
