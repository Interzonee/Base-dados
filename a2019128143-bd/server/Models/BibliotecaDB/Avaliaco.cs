using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace A2019128143.Models.BibliotecaDb
{
  [Table("Avaliacoes", Schema = "dbo")]
  public partial class Avaliaco
  {
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int AvaliacaoID
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
    public int? MaterialID
    {
      get;
      set;
    }
    public Materiai Materiai { get; set; }
    public int? Nota
    {
      get;
      set;
    }
    public string Comentario
    {
      get;
      set;
    }
    public DateTime? DataAvaliacao
    {
      get;
      set;
    }
  }
}
