using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace A2019128143.Models.BibliotecaDb
{
  [Table("MateriaisCategorias", Schema = "dbo")]
  public partial class MateriaisCategoria
  {
    [Key]
    public int MaterialID
    {
      get;
      set;
    }
    public Materiai Materiai { get; set; }
    [Key]
    public int CategoriaID
    {
      get;
      set;
    }
    public CategoriasMateriai CategoriasMateriai { get; set; }
  }
}
