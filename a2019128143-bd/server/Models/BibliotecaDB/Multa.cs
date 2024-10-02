using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace A2019128143.Models.BibliotecaDb
{
  [Table("Multas", Schema = "dbo")]
  public partial class Multa
  {
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int MultaID
    {
      get;
      set;
    }
    public int? EmprestimoID
    {
      get;
      set;
    }
    public Emprestimo Emprestimo { get; set; }
    public decimal Valor
    {
      get;
      set;
    }
    public DateTime? DataGeracao
    {
      get;
      set;
    }
    public DateTime? DataPagamento
    {
      get;
      set;
    }
    public string Status
    {
      get;
      set;
    }
  }
}
