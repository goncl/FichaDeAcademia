using FichaAcademia.Dominio.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace FichaAcademia.AcessoDados.Mapeamentos
{
    public class ListaExercicioMap : IEntityTypeConfiguration<ListaExercicio>
    {
        public void Configure(EntityTypeBuilder<ListaExercicio> builder)
        {
            //determina a chave primaria
            builder.HasKey(le => le.ListaExercicioId);

            //A propriedade é obrigatória
            builder.Property(le => le.Frequencia).IsRequired();
            builder.Property(le => le.Repeticoes).IsRequired();
            builder.Property(le => le.Carga).IsRequired();

            //um exercicio pode ter varias listaExercicio e tem a chave estrangeira ExercicioId
            builder.HasOne(le => le.Exercicio).WithMany(le => le.ListaExercicios).HasForeignKey(le => le.ExercicioId);
            //uma ficha pode ter varias listaExercicio e tem a chave estrangeira FichaId
            builder.HasOne(le => le.Ficha).WithMany(le => le.ListaExercicios).HasForeignKey(le => le.FichaId);

            //determina o nome da tabela
            builder.ToTable("ListasExercicios");
        }
    }
}
