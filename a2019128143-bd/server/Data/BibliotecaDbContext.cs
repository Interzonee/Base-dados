using System.Reflection;
using System.Linq;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using Microsoft.Extensions.Configuration;

using A2019128143.Models.BibliotecaDb;

namespace A2019128143.Data
{
  public partial class BibliotecaDbContext : Microsoft.EntityFrameworkCore.DbContext
  {
    public BibliotecaDbContext(DbContextOptions<BibliotecaDbContext> options):base(options)
    {
    }

    public BibliotecaDbContext()
    {
    }

    partial void OnModelBuilding(ModelBuilder builder);

    protected override void OnModelCreating(ModelBuilder builder)
    {
        base.OnModelCreating(builder);

        builder.Entity<A2019128143.Models.BibliotecaDb.SpRegistrarDevolucao>().HasNoKey();
        builder.Entity<A2019128143.Models.BibliotecaDb.SpRegistrarEmprestimo>().HasNoKey();
        builder.Entity<A2019128143.Models.BibliotecaDb.SpRegistrarReserva>().HasNoKey();
        builder.Entity<A2019128143.Models.BibliotecaDb.VwMateriaisDisponivei>().HasNoKey();
        builder.Entity<A2019128143.Models.BibliotecaDb.MateriaisCategoria>().HasKey(table => new {
          table.MaterialID, table.CategoriaID
        });
        builder.Entity<A2019128143.Models.BibliotecaDb.Avaliaco>()
              .HasOne(i => i.Utilizadore)
              .WithMany(i => i.Avaliacos)
              .HasForeignKey(i => i.UtilizadorID)
              .HasPrincipalKey(i => i.UtilizadorID);
        builder.Entity<A2019128143.Models.BibliotecaDb.Avaliaco>()
              .HasOne(i => i.Materiai)
              .WithMany(i => i.Avaliacos)
              .HasForeignKey(i => i.MaterialID)
              .HasPrincipalKey(i => i.MaterialID);
        builder.Entity<A2019128143.Models.BibliotecaDb.Emprestimo>()
              .HasOne(i => i.Utilizadore)
              .WithMany(i => i.Emprestimos)
              .HasForeignKey(i => i.UtilizadorID)
              .HasPrincipalKey(i => i.UtilizadorID);
        builder.Entity<A2019128143.Models.BibliotecaDb.Emprestimo>()
              .HasOne(i => i.Materiai)
              .WithMany(i => i.Emprestimos)
              .HasForeignKey(i => i.MaterialID)
              .HasPrincipalKey(i => i.MaterialID);
        builder.Entity<A2019128143.Models.BibliotecaDb.Emprestimo>()
              .HasOne(i => i.Utilizadore1)
              .WithMany(i => i.Emprestimos1)
              .HasForeignKey(i => i.FuncionarioEmprestimoID)
              .HasPrincipalKey(i => i.UtilizadorID);
        builder.Entity<A2019128143.Models.BibliotecaDb.Emprestimo>()
              .HasOne(i => i.Utilizadore2)
              .WithMany(i => i.Emprestimos2)
              .HasForeignKey(i => i.FuncionarioDevolucaoID)
              .HasPrincipalKey(i => i.UtilizadorID);
        builder.Entity<A2019128143.Models.BibliotecaDb.Funcionario>()
              .HasOne(i => i.Utilizadore)
              .WithMany(i => i.Funcionarios)
              .HasForeignKey(i => i.UtilizadorID)
              .HasPrincipalKey(i => i.UtilizadorID);
        builder.Entity<A2019128143.Models.BibliotecaDb.MateriaisCategoria>()
              .HasOne(i => i.Materiai)
              .WithMany(i => i.MateriaisCategoria)
              .HasForeignKey(i => i.MaterialID)
              .HasPrincipalKey(i => i.MaterialID);
        builder.Entity<A2019128143.Models.BibliotecaDb.MateriaisCategoria>()
              .HasOne(i => i.CategoriasMateriai)
              .WithMany(i => i.MateriaisCategoria)
              .HasForeignKey(i => i.CategoriaID)
              .HasPrincipalKey(i => i.CategoriaID);
        builder.Entity<A2019128143.Models.BibliotecaDb.Multa>()
              .HasOne(i => i.Emprestimo)
              .WithMany(i => i.Multa)
              .HasForeignKey(i => i.EmprestimoID)
              .HasPrincipalKey(i => i.EmprestimoID);
        builder.Entity<A2019128143.Models.BibliotecaDb.Notificaco>()
              .HasOne(i => i.Utilizadore)
              .WithMany(i => i.Notificacos)
              .HasForeignKey(i => i.UtilizadorID)
              .HasPrincipalKey(i => i.UtilizadorID);
        builder.Entity<A2019128143.Models.BibliotecaDb.Reserva>()
              .HasOne(i => i.Utilizadore)
              .WithMany(i => i.Reservas)
              .HasForeignKey(i => i.UtilizadorID)
              .HasPrincipalKey(i => i.UtilizadorID);
        builder.Entity<A2019128143.Models.BibliotecaDb.Reserva>()
              .HasOne(i => i.Materiai)
              .WithMany(i => i.Reservas)
              .HasForeignKey(i => i.MaterialID)
              .HasPrincipalKey(i => i.MaterialID);

        builder.Entity<A2019128143.Models.BibliotecaDb.Avaliaco>()
              .Property(p => p.DataAvaliacao)
              .HasDefaultValueSql("(getdate())");

        builder.Entity<A2019128143.Models.BibliotecaDb.Emprestimo>()
              .Property(p => p.DataEmprestimo)
              .HasDefaultValueSql("(getdate())");

        builder.Entity<A2019128143.Models.BibliotecaDb.Emprestimo>()
              .Property(p => p.Status)
              .HasDefaultValueSql("('Ativo')");

        builder.Entity<A2019128143.Models.BibliotecaDb.Materiai>()
              .Property(p => p.NumeroExemplares)
              .HasDefaultValueSql("((1))");

        builder.Entity<A2019128143.Models.BibliotecaDb.Materiai>()
              .Property(p => p.DataAquisicao)
              .HasDefaultValueSql("(getdate())");

        builder.Entity<A2019128143.Models.BibliotecaDb.Multa>()
              .Property(p => p.DataGeracao)
              .HasDefaultValueSql("(getdate())");

        builder.Entity<A2019128143.Models.BibliotecaDb.Multa>()
              .Property(p => p.Status)
              .HasDefaultValueSql("('Pendente')");

        builder.Entity<A2019128143.Models.BibliotecaDb.Notificaco>()
              .Property(p => p.DataEnvio)
              .HasDefaultValueSql("(getdate())");

        builder.Entity<A2019128143.Models.BibliotecaDb.Notificaco>()
              .Property(p => p.Lida)
              .HasDefaultValueSql("((0))");

        builder.Entity<A2019128143.Models.BibliotecaDb.Reserva>()
              .Property(p => p.DataReserva)
              .HasDefaultValueSql("(getdate())");

        builder.Entity<A2019128143.Models.BibliotecaDb.Reserva>()
              .Property(p => p.Status)
              .HasDefaultValueSql("('Ativa')");

        builder.Entity<A2019128143.Models.BibliotecaDb.Utilizadore>()
              .Property(p => p.DataRegistro)
              .HasDefaultValueSql("(getdate())");

        builder.Entity<A2019128143.Models.BibliotecaDb.Utilizadore>()
              .Property(p => p.Ativo)
              .HasDefaultValueSql("((1))").ValueGeneratedNever();


        builder.Entity<A2019128143.Models.BibliotecaDb.Avaliaco>()
              .Property(p => p.DataAvaliacao)
              .HasColumnType("date");

        builder.Entity<A2019128143.Models.BibliotecaDb.Emprestimo>()
              .Property(p => p.DataEmprestimo)
              .HasColumnType("date");

        builder.Entity<A2019128143.Models.BibliotecaDb.Emprestimo>()
              .Property(p => p.DataDevolucaoPrevista)
              .HasColumnType("date");

        builder.Entity<A2019128143.Models.BibliotecaDb.Emprestimo>()
              .Property(p => p.DataDevolucaoEfetiva)
              .HasColumnType("date");

        builder.Entity<A2019128143.Models.BibliotecaDb.Funcionario>()
              .Property(p => p.DataContratacao)
              .HasColumnType("date");

        builder.Entity<A2019128143.Models.BibliotecaDb.Materiai>()
              .Property(p => p.DataAquisicao)
              .HasColumnType("date");

        builder.Entity<A2019128143.Models.BibliotecaDb.Multa>()
              .Property(p => p.DataGeracao)
              .HasColumnType("date");

        builder.Entity<A2019128143.Models.BibliotecaDb.Multa>()
              .Property(p => p.DataPagamento)
              .HasColumnType("date");

        builder.Entity<A2019128143.Models.BibliotecaDb.Notificaco>()
              .Property(p => p.DataEnvio)
              .HasColumnType("datetime");

        builder.Entity<A2019128143.Models.BibliotecaDb.Reserva>()
              .Property(p => p.DataReserva)
              .HasColumnType("date");

        builder.Entity<A2019128143.Models.BibliotecaDb.Reserva>()
              .Property(p => p.DataExpiracao)
              .HasColumnType("date");

        builder.Entity<A2019128143.Models.BibliotecaDb.Utilizadore>()
              .Property(p => p.DataRegistro)
              .HasColumnType("date");

        builder.Entity<A2019128143.Models.BibliotecaDb.Avaliaco>()
              .Property(p => p.AvaliacaoID)
              .HasPrecision(10, 0);

        builder.Entity<A2019128143.Models.BibliotecaDb.Avaliaco>()
              .Property(p => p.UtilizadorID)
              .HasPrecision(10, 0);

        builder.Entity<A2019128143.Models.BibliotecaDb.Avaliaco>()
              .Property(p => p.MaterialID)
              .HasPrecision(10, 0);

        builder.Entity<A2019128143.Models.BibliotecaDb.Avaliaco>()
              .Property(p => p.Nota)
              .HasPrecision(10, 0);

        builder.Entity<A2019128143.Models.BibliotecaDb.CategoriasMateriai>()
              .Property(p => p.CategoriaID)
              .HasPrecision(10, 0);

        builder.Entity<A2019128143.Models.BibliotecaDb.Emprestimo>()
              .Property(p => p.EmprestimoID)
              .HasPrecision(10, 0);

        builder.Entity<A2019128143.Models.BibliotecaDb.Emprestimo>()
              .Property(p => p.UtilizadorID)
              .HasPrecision(10, 0);

        builder.Entity<A2019128143.Models.BibliotecaDb.Emprestimo>()
              .Property(p => p.MaterialID)
              .HasPrecision(10, 0);

        builder.Entity<A2019128143.Models.BibliotecaDb.Emprestimo>()
              .Property(p => p.FuncionarioEmprestimoID)
              .HasPrecision(10, 0);

        builder.Entity<A2019128143.Models.BibliotecaDb.Emprestimo>()
              .Property(p => p.FuncionarioDevolucaoID)
              .HasPrecision(10, 0);

        builder.Entity<A2019128143.Models.BibliotecaDb.Funcionario>()
              .Property(p => p.FuncionarioID)
              .HasPrecision(10, 0);

        builder.Entity<A2019128143.Models.BibliotecaDb.Funcionario>()
              .Property(p => p.UtilizadorID)
              .HasPrecision(10, 0);

        builder.Entity<A2019128143.Models.BibliotecaDb.Funcionario>()
              .Property(p => p.Salario)
              .HasPrecision(10, 2);

        builder.Entity<A2019128143.Models.BibliotecaDb.Materiai>()
              .Property(p => p.MaterialID)
              .HasPrecision(10, 0);

        builder.Entity<A2019128143.Models.BibliotecaDb.Materiai>()
              .Property(p => p.AnoPublicacao)
              .HasPrecision(10, 0);

        builder.Entity<A2019128143.Models.BibliotecaDb.Materiai>()
              .Property(p => p.NumeroExemplares)
              .HasPrecision(10, 0);

        builder.Entity<A2019128143.Models.BibliotecaDb.MateriaisCategoria>()
              .Property(p => p.MaterialID)
              .HasPrecision(10, 0);

        builder.Entity<A2019128143.Models.BibliotecaDb.MateriaisCategoria>()
              .Property(p => p.CategoriaID)
              .HasPrecision(10, 0);

        builder.Entity<A2019128143.Models.BibliotecaDb.Multa>()
              .Property(p => p.MultaID)
              .HasPrecision(10, 0);

        builder.Entity<A2019128143.Models.BibliotecaDb.Multa>()
              .Property(p => p.EmprestimoID)
              .HasPrecision(10, 0);

        builder.Entity<A2019128143.Models.BibliotecaDb.Multa>()
              .Property(p => p.Valor)
              .HasPrecision(10, 2);

        builder.Entity<A2019128143.Models.BibliotecaDb.Notificaco>()
              .Property(p => p.NotificacaoID)
              .HasPrecision(10, 0);

        builder.Entity<A2019128143.Models.BibliotecaDb.Notificaco>()
              .Property(p => p.UtilizadorID)
              .HasPrecision(10, 0);

        builder.Entity<A2019128143.Models.BibliotecaDb.Reserva>()
              .Property(p => p.ReservaID)
              .HasPrecision(10, 0);

        builder.Entity<A2019128143.Models.BibliotecaDb.Reserva>()
              .Property(p => p.UtilizadorID)
              .HasPrecision(10, 0);

        builder.Entity<A2019128143.Models.BibliotecaDb.Reserva>()
              .Property(p => p.MaterialID)
              .HasPrecision(10, 0);

        builder.Entity<A2019128143.Models.BibliotecaDb.Utilizadore>()
              .Property(p => p.UtilizadorID)
              .HasPrecision(10, 0);

        builder.Entity<A2019128143.Models.BibliotecaDb.VwMateriaisDisponivei>()
              .Property(p => p.MaterialID)
              .HasPrecision(10, 0);

        builder.Entity<A2019128143.Models.BibliotecaDb.VwMateriaisDisponivei>()
              .Property(p => p.AnoPublicacao)
              .HasPrecision(10, 0);

        builder.Entity<A2019128143.Models.BibliotecaDb.VwMateriaisDisponivei>()
              .Property(p => p.ExemplaresDisponiveis)
              .HasPrecision(10, 0);
        this.OnModelBuilding(builder);
    }


    public DbSet<A2019128143.Models.BibliotecaDb.Avaliaco> Avaliacos
    {
      get;
      set;
    }

    public DbSet<A2019128143.Models.BibliotecaDb.CategoriasMateriai> CategoriasMateriais
    {
      get;
      set;
    }

    public DbSet<A2019128143.Models.BibliotecaDb.Emprestimo> Emprestimos
    {
      get;
      set;
    }

    public DbSet<A2019128143.Models.BibliotecaDb.Funcionario> Funcionarios
    {
      get;
      set;
    }

    public DbSet<A2019128143.Models.BibliotecaDb.Materiai> Materiais
    {
      get;
      set;
    }

    public DbSet<A2019128143.Models.BibliotecaDb.MateriaisCategoria> MateriaisCategoria
    {
      get;
      set;
    }

    public DbSet<A2019128143.Models.BibliotecaDb.Multa> Multa
    {
      get;
      set;
    }

    public DbSet<A2019128143.Models.BibliotecaDb.Notificaco> Notificacos
    {
      get;
      set;
    }

    public DbSet<A2019128143.Models.BibliotecaDb.Reserva> Reservas
    {
      get;
      set;
    }

    public DbSet<A2019128143.Models.BibliotecaDb.SpRegistrarDevolucao> SpRegistrarDevolucaos
    {
      get;
      set;
    }

    public DbSet<A2019128143.Models.BibliotecaDb.SpRegistrarEmprestimo> SpRegistrarEmprestimos
    {
      get;
      set;
    }

    public DbSet<A2019128143.Models.BibliotecaDb.SpRegistrarReserva> SpRegistrarReservas
    {
      get;
      set;
    }

    public DbSet<A2019128143.Models.BibliotecaDb.Utilizadore> Utilizadores
    {
      get;
      set;
    }

    public DbSet<A2019128143.Models.BibliotecaDb.VwMateriaisDisponivei> VwMateriaisDisponiveis
    {
      get;
      set;
    }

    protected override void ConfigureConventions(ModelConfigurationBuilder configurationBuilder)
    {
        configurationBuilder.Conventions.Add(_ => new BlankTriggerAddingConvention());
    }
  }
}
