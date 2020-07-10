using FichaAcademia.Dominio.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace FichaAcademia.AcessoDados.Mapeamentos
{
    class ExercicioMap : IEntityTypeConfiguration<Exercicio>
    {
        public void Configure(EntityTypeBuilder<Exercicio> builder)
        {
            //determina a chave primaria
            builder.HasKey(e => e.ExercicioId);

            //A propriedade nome tem no maximo 50 caracteres e é obrigatorio.
            builder.Property(e => e.Nome).HasMaxLength(50).IsRequired();

            //uma categoriaexercicio pode ter varios exercicios e tem a chave estrangeira CategoriaExercicioId)
            builder.HasOne(e => e.CategoriaExercicio).WithMany(e => e.Exercicios).HasForeignKey(e => e.CategoriaExercicioId);

            //pode ter varias listaexercicio mas só uma exercicio 
            builder.HasMany(e => e.ListaExercicios).WithOne(e => e.Exercicio);

            //determina o nome da tabela
            builder.ToTable("Exercicios");
        }
    }
}
