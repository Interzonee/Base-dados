using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace A2019128143.Models.BibliotecaDb
{
  [Table("sp_RegistrarEmprestimo", Schema = "dbo")]
  public partial class SpRegistrarEmprestimo
  {
    public string Mensagem
    {
      get;
      set;
    }
  }
}
