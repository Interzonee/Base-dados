using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace A2019128143.Models.BibliotecaDb
{
  [Table("Notificacoes", Schema = "dbo")]
  public partial class Notificaco
  {
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int NotificacaoID
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
    public string Mensagem
    {
      get;
      set;
    }
    public DateTime? DataEnvio
    {
      get;
      set;
    }
    public bool? Lida
    {
      get;
      set;
    }
    public string Tipo
    {
      get;
      set;
    }
  }
}
