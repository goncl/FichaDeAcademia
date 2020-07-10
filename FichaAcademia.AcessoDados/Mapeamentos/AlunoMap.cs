using FichaAcademia.Dominio.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace FichaAcademia.AcessoDados.Mapeamentos
{
    public class AlunoMap : IEntityTypeConfiguration<Aluno>
    {
        public void Configure(EntityTypeBuilder<Aluno> builder)
        {
            //determina a chave primaria
            builder.HasKey(a => a.AlunoId);

            //A propriedade nomecompleto tem no maximo 100 caracteres e é obrigatorio.
            builder.Property(a => a.NomeCompleto).HasMaxLength(100).IsRequired();
            builder.Property(a => a.Idade).IsRequired();
            builder.Property(a => a.Peso).IsRequired();
            builder.Property(a => a.FrequenciaSemanal).IsRequired();

            //um objetivo pode ter varios alunos e tem a chave estrangeira ObjetivoId
            builder.HasOne(a => a.Objetivo).WithMany(a => a.Alunos).HasForeignKey(a => a.ObjetivoId);
            //um professor pode ter varios alunos e tem a chave estrangeira ProfessorId
            builder.HasOne(a => a.Professor).WithMany(a => a.Alunos).HasForeignKey(a => a.ProfessorId);

            //determina que na tabela fichas tem uma chave estrangeira de aluno
            //varioa fichas para um aluno
            builder.HasMany(a => a.Fichas).WithOne(a => a.Aluno).OnDelete(DeleteBehavior.Cascade);

            //determina o nome da tabela
            builder.ToTable("Alunos");
        }
    }
}
