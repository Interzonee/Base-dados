using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace A2019128143.Models.BibliotecaDb
{
  [Table("Utilizadores", Schema = "dbo")]
  public partial class Utilizadore
  {
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int UtilizadorID
    {
      get;
      set;
    }

    public ICollection<Emprestimo> Emprestimos { get; set; }
    public ICollection<Emprestimo> Emprestimos1 { get; set; }
    public ICollection<Emprestimo> Emprestimos2 { get; set; }
    public ICollection<Reserva> Reservas { get; set; }
    public ICollection<Avaliaco> Avaliacos { get; set; }
    public ICollection<Funcionario> Funcionarios { get; set; }
    public ICollection<Notificaco> Notificacos { get; set; }
    public string Nome
    {
      get;
      set;
    }
    public string Email
    {
      get;
      set;
    }
    public string Tipo
    {
      get;
      set;
    }
    public DateTime? DataRegistro
    {
      get;
      set;
    }
    public bool? Ativo
    {
      get;
      set;
    } = true;
  }
}
