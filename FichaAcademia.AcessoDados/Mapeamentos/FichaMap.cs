using FichaAcademia.Dominio.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace FichaAcademia.AcessoDados.Mapeamentos
{
    class FichaMap : IEntityTypeConfiguration<Ficha>
    {
        public void Configure(EntityTypeBuilder<Ficha> builder)
        {
            //determina a chave primaria
            builder.HasKey(f => f.FichaId);

            //A propriedade nome tem no maximo 50 caracteres e é obrigatorio.
            builder.Property(f => f.Nome).HasMaxLength(50).IsRequired();
            builder.Property(f => f.Cadastro).IsRequired();
            builder.Property(f => f.Validade).IsRequired();

             
            builder.HasOne(f => f.Aluno).WithMany(f => f.Fichas).HasForeignKey(f => f.AlunoId);

            //pode ter varias listaexercicio mas só uma ficha e se a ficha for deletada delata as listaexercicio em cascata
            builder.HasMany(f => f.ListaExercicios).WithOne(f => f.Ficha).OnDelete(DeleteBehavior.Cascade);

            //determina o nome da tabela
            builder.ToTable("Fichas");
        }
    }
}
