using FichaAcademia.Dominio.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace FichaAcademia.AcessoDados.Mapeamentos
{
    public class CategoriaExercicioMap : IEntityTypeConfiguration<CategoriaExercicio>
    {
        public void Configure(EntityTypeBuilder<CategoriaExercicio> builder)
        {
            //determina a chave primaria
            builder.HasKey(ce => ce.CategoriaExercicioId);

            //A propriedade nome tem no maximo 50 caracteres e é obrigatorio.
            builder.Property(ce => ce.Nome).HasMaxLength(50).IsRequired();

         //tem varios exercicios  mas só uma categoria   e se a categoria for deletada delata os exercicios em cascata
            builder.HasMany(ce => ce.Exercicios).WithOne(ce => ce.CategoriaExercicio).OnDelete(DeleteBehavior.Cascade);

            //determina o nome da tabela
            builder.ToTable("CategoriasExercicios");
        }
    }
}
