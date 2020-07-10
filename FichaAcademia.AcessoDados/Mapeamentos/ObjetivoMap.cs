using FichaAcademia.Dominio.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace FichaAcademia.AcessoDados.Mapeamentos
{
    public class ObjetivoMap : IEntityTypeConfiguration<Objetivo>
    {
        public void Configure(EntityTypeBuilder<Objetivo> builder)
        {
            //determina a chave primaria
            builder.HasKey(o => o.ObjetivoId);

            //A propriedade nome tem no maximo 50 caracteres e é obrigatorio.
            builder.Property(o => o.Nome).HasMaxLength(50).IsRequired();
            //A propriedade descricao tem no maximo 500 caracteres e é obrigatorio.
            builder.Property(o => o.Descricao).HasMaxLength(500).IsRequired();

            //varios alunos para um objetivo
            builder.HasMany(o => o.Alunos).WithOne(o => o.Objetivo);

            //determina o nome da tabela
            builder.ToTable("Objetivos");
        }
    }
}
