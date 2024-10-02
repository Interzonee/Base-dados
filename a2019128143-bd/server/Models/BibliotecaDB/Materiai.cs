using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace A2019128143.Models.BibliotecaDb
{
  [Table("Materiais", Schema = "dbo")]
  public partial class Materiai
  {
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int MaterialID
    {
      get;
      set;
    }

    public ICollection<Emprestimo> Emprestimos { get; set; }
    public ICollection<Reserva> Reservas { get; set; }
    public ICollection<Avaliaco> Avaliacos { get; set; }
    public ICollection<MateriaisCategoria> MateriaisCategoria { get; set; }
    public string Titulo
    {
      get;
      set;
    }
    public string Tipo
    {
      get;
      set;
    }
    public string Autor
    {
      get;
      set;
    }
    public string Editora
    {
      get;
      set;
    }
    public int? AnoPublicacao
    {
      get;
      set;
    }
    public int NumeroExemplares
    {
      get;
      set;
    }
    public string ISBN
    {
      get;
      set;
    }
    public string Localizacao
    {
      get;
      set;
    }
    public DateTime? DataAquisicao
    {
      get;
      set;
    }
    public string Descricao
    {
      get;
      set;
    }
  }
}
