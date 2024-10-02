using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace A2019128143.Models.BibliotecaDb
{
  [Table("Emprestimos", Schema = "dbo")]
  public partial class Emprestimo
  {
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int EmprestimoID
    {
      get;
      set;
    }

    public ICollection<Multa> Multa { get; set; }
    public int? UtilizadorID
    {
      get;
      set;
    }
    public Utilizadore Utilizadore { get; set; }
    public int? MaterialID
    {
      get;
      set;
    }
    public Materiai Materiai { get; set; }
    public DateTime? DataEmprestimo
    {
      get;
      set;
    }
    public DateTime? DataDevolucaoPrevista
    {
      get;
      set;
    }
    public DateTime? DataDevolucaoEfetiva
    {
      get;
      set;
    }
    public string Status
    {
      get;
      set;
    }
    public int? FuncionarioEmprestimoID
    {
      get;
      set;
    }
    public Utilizadore Utilizadore1 { get; set; }
    public int? FuncionarioDevolucaoID
    {
      get;
      set;
    }
    public Utilizadore Utilizadore2 { get; set; }
  }
}
