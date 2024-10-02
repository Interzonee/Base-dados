using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace A2019128143.Models.BibliotecaDb
{
  [Table("Reservas", Schema = "dbo")]
  public partial class Reserva
  {
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int ReservaID
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
    public DateTime? DataReserva
    {
      get;
      set;
    }
    public DateTime? DataExpiracao
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
