using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace A2019128143.Models.BibliotecaDb
{
  [Table("Funcionarios", Schema = "dbo")]
  public partial class Funcionario
  {
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int FuncionarioID
    {
      get;
      set;
    }
    public int? UtilizadorID
    {
      get;
      set;
    }
    public Utilizadore Utilizadore { get; set; }
    public string Cargo
    {
      get;
      set;
    }
    public DateTime DataContratacao
    {
      get;
      set;
    }
    public decimal? Salario
    {
      get;
      set;
    }
    public string Departamento
    {
      get;
      set;
    }
  }
}
