using Radzen;
using System;
using System.Web;
using System.Linq;
using System.Linq.Dynamic.Core;
using System.Data;
using System.Text.Encodings.Web;
using Microsoft.Data.SqlClient;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Components;
using A2019128143.Data;

namespace A2019128143
{
    public partial class BibliotecaDbService
    {
        BibliotecaDbContext Context
        {
           get
           {
             return this.context;
           }
        }

        private readonly BibliotecaDbContext context;
        private readonly NavigationManager navigationManager;

        public BibliotecaDbService(BibliotecaDbContext context, NavigationManager navigationManager)
        {
            this.context = context;
            this.navigationManager = navigationManager;
        }

        public void Reset() => Context.ChangeTracker.Entries().Where(e => e.Entity != null).ToList().ForEach(e => e.State = EntityState.Detached);

        public async Task ExportAvaliacosToExcel(Query query = null, string fileName = null)
        {
            navigationManager.NavigateTo(query != null ? query.ToUrl($"export/bibliotecadb/avaliacos/excel(fileName='{(!string.IsNullOrEmpty(fileName) ? UrlEncoder.Default.Encode(fileName) : "Export")}')") : $"export/bibliotecadb/avaliacos/excel(fileName='{(!string.IsNullOrEmpty(fileName) ? UrlEncoder.Default.Encode(fileName) : "Export")}')", true);
        }

        public async Task ExportAvaliacosToCSV(Query query = null, string fileName = null)
        {
            navigationManager.NavigateTo(query != null ? query.ToUrl($"export/bibliotecadb/avaliacos/csv(fileName='{(!string.IsNullOrEmpty(fileName) ? UrlEncoder.Default.Encode(fileName) : "Export")}')") : $"export/bibliotecadb/avaliacos/csv(fileName='{(!string.IsNullOrEmpty(fileName) ? UrlEncoder.Default.Encode(fileName) : "Export")}')", true);
        }

        partial void OnAvaliacosRead(ref IQueryable<Models.BibliotecaDb.Avaliaco> items);

        public async Task<IQueryable<Models.BibliotecaDb.Avaliaco>> GetAvaliacos(Query query = null)
        {
            var items = Context.Avaliacos.AsQueryable();

            items = items.Include(i => i.Utilizadore);

            items = items.Include(i => i.Materiai);

            if (query != null)
            {
                if (!string.IsNullOrEmpty(query.Expand))
                {
                    var propertiesToExpand = query.Expand.Split(',');
                    foreach(var p in propertiesToExpand)
                    {
                        items = items.Include(p.Trim());
                    }
                }

                if (!string.IsNullOrEmpty(query.Filter))
                {
                    if (query.FilterParameters != null)
                    {
                        items = items.Where(query.Filter, query.FilterParameters);
                    }
                    else
                    {
                        items = items.Where(query.Filter);
                    }
                }

                if (!string.IsNullOrEmpty(query.OrderBy))
                {
                    items = items.OrderBy(query.OrderBy);
                }

                if (query.Skip.HasValue)
                {
                    items = items.Skip(query.Skip.Value);
                }

                if (query.Top.HasValue)
                {
                    items = items.Take(query.Top.Value);
                }
            }

            OnAvaliacosRead(ref items);

            return await Task.FromResult(items);
        }

        partial void OnAvaliacoCreated(Models.BibliotecaDb.Avaliaco item);
        partial void OnAfterAvaliacoCreated(Models.BibliotecaDb.Avaliaco item);

        public async Task<Models.BibliotecaDb.Avaliaco> CreateAvaliaco(Models.BibliotecaDb.Avaliaco avaliaco)
        {
            OnAvaliacoCreated(avaliaco);

            var existingItem = Context.Avaliacos
                              .Where(i => i.AvaliacaoID == avaliaco.AvaliacaoID)
                              .FirstOrDefault();

            if (existingItem != null)
            {
               throw new Exception("Item already available");
            }            

            try
            {
                Context.Avaliacos.Add(avaliaco);
                Context.SaveChanges();
            }
            catch
            {
                Context.Entry(avaliaco).State = EntityState.Detached;
                avaliaco.Utilizadore = null;
                avaliaco.Materiai = null;
                throw;
            }

            OnAfterAvaliacoCreated(avaliaco);

            return avaliaco;
        }
        public async Task ExportCategoriasMateriaisToExcel(Query query = null, string fileName = null)
        {
            navigationManager.NavigateTo(query != null ? query.ToUrl($"export/bibliotecadb/categoriasmateriais/excel(fileName='{(!string.IsNullOrEmpty(fileName) ? UrlEncoder.Default.Encode(fileName) : "Export")}')") : $"export/bibliotecadb/categoriasmateriais/excel(fileName='{(!string.IsNullOrEmpty(fileName) ? UrlEncoder.Default.Encode(fileName) : "Export")}')", true);
        }

        public async Task ExportCategoriasMateriaisToCSV(Query query = null, string fileName = null)
        {
            navigationManager.NavigateTo(query != null ? query.ToUrl($"export/bibliotecadb/categoriasmateriais/csv(fileName='{(!string.IsNullOrEmpty(fileName) ? UrlEncoder.Default.Encode(fileName) : "Export")}')") : $"export/bibliotecadb/categoriasmateriais/csv(fileName='{(!string.IsNullOrEmpty(fileName) ? UrlEncoder.Default.Encode(fileName) : "Export")}')", true);
        }

        partial void OnCategoriasMateriaisRead(ref IQueryable<Models.BibliotecaDb.CategoriasMateriai> items);

        public async Task<IQueryable<Models.BibliotecaDb.CategoriasMateriai>> GetCategoriasMateriais(Query query = null)
        {
            var items = Context.CategoriasMateriais.AsQueryable();

            if (query != null)
            {
                if (!string.IsNullOrEmpty(query.Expand))
                {
                    var propertiesToExpand = query.Expand.Split(',');
                    foreach(var p in propertiesToExpand)
                    {
                        items = items.Include(p.Trim());
                    }
                }

                if (!string.IsNullOrEmpty(query.Filter))
                {
                    if (query.FilterParameters != null)
                    {
                        items = items.Where(query.Filter, query.FilterParameters);
                    }
                    else
                    {
                        items = items.Where(query.Filter);
                    }
                }

                if (!string.IsNullOrEmpty(query.OrderBy))
                {
                    items = items.OrderBy(query.OrderBy);
                }

                if (query.Skip.HasValue)
                {
                    items = items.Skip(query.Skip.Value);
                }

                if (query.Top.HasValue)
                {
                    items = items.Take(query.Top.Value);
                }
            }

            OnCategoriasMateriaisRead(ref items);

            return await Task.FromResult(items);
        }

        partial void OnCategoriasMateriaiCreated(Models.BibliotecaDb.CategoriasMateriai item);
        partial void OnAfterCategoriasMateriaiCreated(Models.BibliotecaDb.CategoriasMateriai item);

        public async Task<Models.BibliotecaDb.CategoriasMateriai> CreateCategoriasMateriai(Models.BibliotecaDb.CategoriasMateriai categoriasMateriai)
        {
            OnCategoriasMateriaiCreated(categoriasMateriai);

            var existingItem = Context.CategoriasMateriais
                              .Where(i => i.CategoriaID == categoriasMateriai.CategoriaID)
                              .FirstOrDefault();

            if (existingItem != null)
            {
               throw new Exception("Item already available");
            }            

            try
            {
                Context.CategoriasMateriais.Add(categoriasMateriai);
                Context.SaveChanges();
            }
            catch
            {
                Context.Entry(categoriasMateriai).State = EntityState.Detached;
                throw;
            }

            OnAfterCategoriasMateriaiCreated(categoriasMateriai);

            return categoriasMateriai;
        }
        public async Task ExportEmprestimosToExcel(Query query = null, string fileName = null)
        {
            navigationManager.NavigateTo(query != null ? query.ToUrl($"export/bibliotecadb/emprestimos/excel(fileName='{(!string.IsNullOrEmpty(fileName) ? UrlEncoder.Default.Encode(fileName) : "Export")}')") : $"export/bibliotecadb/emprestimos/excel(fileName='{(!string.IsNullOrEmpty(fileName) ? UrlEncoder.Default.Encode(fileName) : "Export")}')", true);
        }

        public async Task ExportEmprestimosToCSV(Query query = null, string fileName = null)
        {
            navigationManager.NavigateTo(query != null ? query.ToUrl($"export/bibliotecadb/emprestimos/csv(fileName='{(!string.IsNullOrEmpty(fileName) ? UrlEncoder.Default.Encode(fileName) : "Export")}')") : $"export/bibliotecadb/emprestimos/csv(fileName='{(!string.IsNullOrEmpty(fileName) ? UrlEncoder.Default.Encode(fileName) : "Export")}')", true);
        }

        partial void OnEmprestimosRead(ref IQueryable<Models.BibliotecaDb.Emprestimo> items);

        public async Task<IQueryable<Models.BibliotecaDb.Emprestimo>> GetEmprestimos(Query query = null)
        {
            var items = Context.Emprestimos.AsQueryable();

            items = items.Include(i => i.Utilizadore);

            items = items.Include(i => i.Materiai);

            items = items.Include(i => i.Utilizadore1);

            items = items.Include(i => i.Utilizadore2);

            if (query != null)
            {
                if (!string.IsNullOrEmpty(query.Expand))
                {
                    var propertiesToExpand = query.Expand.Split(',');
                    foreach(var p in propertiesToExpand)
                    {
                        items = items.Include(p.Trim());
                    }
                }

                if (!string.IsNullOrEmpty(query.Filter))
                {
                    if (query.FilterParameters != null)
                    {
                        items = items.Where(query.Filter, query.FilterParameters);
                    }
                    else
                    {
                        items = items.Where(query.Filter);
                    }
                }

                if (!string.IsNullOrEmpty(query.OrderBy))
                {
                    items = items.OrderBy(query.OrderBy);
                }

                if (query.Skip.HasValue)
                {
                    items = items.Skip(query.Skip.Value);
                }

                if (query.Top.HasValue)
                {
                    items = items.Take(query.Top.Value);
                }
            }

            OnEmprestimosRead(ref items);

            return await Task.FromResult(items);
        }

        partial void OnEmprestimoCreated(Models.BibliotecaDb.Emprestimo item);
        partial void OnAfterEmprestimoCreated(Models.BibliotecaDb.Emprestimo item);

        public async Task<Models.BibliotecaDb.Emprestimo> CreateEmprestimo(Models.BibliotecaDb.Emprestimo emprestimo)
        {
            OnEmprestimoCreated(emprestimo);

            var existingItem = Context.Emprestimos
                              .Where(i => i.EmprestimoID == emprestimo.EmprestimoID)
                              .FirstOrDefault();

            if (existingItem != null)
            {
               throw new Exception("Item already available");
            }            

            try
            {
                Context.Emprestimos.Add(emprestimo);
                Context.SaveChanges();
            }
            catch
            {
                Context.Entry(emprestimo).State = EntityState.Detached;
                emprestimo.Utilizadore = null;
                emprestimo.Materiai = null;
                emprestimo.Utilizadore1 = null;
                emprestimo.Utilizadore2 = null;
                throw;
            }

            OnAfterEmprestimoCreated(emprestimo);

            return emprestimo;
        }
        public async Task ExportFuncionariosToExcel(Query query = null, string fileName = null)
        {
            navigationManager.NavigateTo(query != null ? query.ToUrl($"export/bibliotecadb/funcionarios/excel(fileName='{(!string.IsNullOrEmpty(fileName) ? UrlEncoder.Default.Encode(fileName) : "Export")}')") : $"export/bibliotecadb/funcionarios/excel(fileName='{(!string.IsNullOrEmpty(fileName) ? UrlEncoder.Default.Encode(fileName) : "Export")}')", true);
        }

        public async Task ExportFuncionariosToCSV(Query query = null, string fileName = null)
        {
            navigationManager.NavigateTo(query != null ? query.ToUrl($"export/bibliotecadb/funcionarios/csv(fileName='{(!string.IsNullOrEmpty(fileName) ? UrlEncoder.Default.Encode(fileName) : "Export")}')") : $"export/bibliotecadb/funcionarios/csv(fileName='{(!string.IsNullOrEmpty(fileName) ? UrlEncoder.Default.Encode(fileName) : "Export")}')", true);
        }

        partial void OnFuncionariosRead(ref IQueryable<Models.BibliotecaDb.Funcionario> items);

        public async Task<IQueryable<Models.BibliotecaDb.Funcionario>> GetFuncionarios(Query query = null)
        {
            var items = Context.Funcionarios.AsQueryable();

            items = items.Include(i => i.Utilizadore);

            if (query != null)
            {
                if (!string.IsNullOrEmpty(query.Expand))
                {
                    var propertiesToExpand = query.Expand.Split(',');
                    foreach(var p in propertiesToExpand)
                    {
                        items = items.Include(p.Trim());
                    }
                }

                if (!string.IsNullOrEmpty(query.Filter))
                {
                    if (query.FilterParameters != null)
                    {
                        items = items.Where(query.Filter, query.FilterParameters);
                    }
                    else
                    {
                        items = items.Where(query.Filter);
                    }
                }

                if (!string.IsNullOrEmpty(query.OrderBy))
                {
                    items = items.OrderBy(query.OrderBy);
                }

                if (query.Skip.HasValue)
                {
                    items = items.Skip(query.Skip.Value);
                }

                if (query.Top.HasValue)
                {
                    items = items.Take(query.Top.Value);
                }
            }

            OnFuncionariosRead(ref items);

            return await Task.FromResult(items);
        }

        partial void OnFuncionarioCreated(Models.BibliotecaDb.Funcionario item);
        partial void OnAfterFuncionarioCreated(Models.BibliotecaDb.Funcionario item);

        public async Task<Models.BibliotecaDb.Funcionario> CreateFuncionario(Models.BibliotecaDb.Funcionario funcionario)
        {
            OnFuncionarioCreated(funcionario);

            var existingItem = Context.Funcionarios
                              .Where(i => i.FuncionarioID == funcionario.FuncionarioID)
                              .FirstOrDefault();

            if (existingItem != null)
            {
               throw new Exception("Item already available");
            }            

            try
            {
                Context.Funcionarios.Add(funcionario);
                Context.SaveChanges();
            }
            catch
            {
                Context.Entry(funcionario).State = EntityState.Detached;
                funcionario.Utilizadore = null;
                throw;
            }

            OnAfterFuncionarioCreated(funcionario);

            return funcionario;
        }
        public async Task ExportMateriaisToExcel(Query query = null, string fileName = null)
        {
            navigationManager.NavigateTo(query != null ? query.ToUrl($"export/bibliotecadb/materiais/excel(fileName='{(!string.IsNullOrEmpty(fileName) ? UrlEncoder.Default.Encode(fileName) : "Export")}')") : $"export/bibliotecadb/materiais/excel(fileName='{(!string.IsNullOrEmpty(fileName) ? UrlEncoder.Default.Encode(fileName) : "Export")}')", true);
        }

        public async Task ExportMateriaisToCSV(Query query = null, string fileName = null)
        {
            navigationManager.NavigateTo(query != null ? query.ToUrl($"export/bibliotecadb/materiais/csv(fileName='{(!string.IsNullOrEmpty(fileName) ? UrlEncoder.Default.Encode(fileName) : "Export")}')") : $"export/bibliotecadb/materiais/csv(fileName='{(!string.IsNullOrEmpty(fileName) ? UrlEncoder.Default.Encode(fileName) : "Export")}')", true);
        }

        partial void OnMateriaisRead(ref IQueryable<Models.BibliotecaDb.Materiai> items);

        public async Task<IQueryable<Models.BibliotecaDb.Materiai>> GetMateriais(Query query = null)
        {
            var items = Context.Materiais.AsQueryable();

            if (query != null)
            {
                if (!string.IsNullOrEmpty(query.Expand))
                {
                    var propertiesToExpand = query.Expand.Split(',');
                    foreach(var p in propertiesToExpand)
                    {
                        items = items.Include(p.Trim());
                    }
                }

                if (!string.IsNullOrEmpty(query.Filter))
                {
                    if (query.FilterParameters != null)
                    {
                        items = items.Where(query.Filter, query.FilterParameters);
                    }
                    else
                    {
                        items = items.Where(query.Filter);
                    }
                }

                if (!string.IsNullOrEmpty(query.OrderBy))
                {
                    items = items.OrderBy(query.OrderBy);
                }

                if (query.Skip.HasValue)
                {
                    items = items.Skip(query.Skip.Value);
                }

                if (query.Top.HasValue)
                {
                    items = items.Take(query.Top.Value);
                }
            }

            OnMateriaisRead(ref items);

            return await Task.FromResult(items);
        }

        partial void OnMateriaiCreated(Models.BibliotecaDb.Materiai item);
        partial void OnAfterMateriaiCreated(Models.BibliotecaDb.Materiai item);

        public async Task<Models.BibliotecaDb.Materiai> CreateMateriai(Models.BibliotecaDb.Materiai materiai)
        {
            OnMateriaiCreated(materiai);

            var existingItem = Context.Materiais
                              .Where(i => i.MaterialID == materiai.MaterialID)
                              .FirstOrDefault();

            if (existingItem != null)
            {
               throw new Exception("Item already available");
            }            

            try
            {
                Context.Materiais.Add(materiai);
                Context.SaveChanges();
            }
            catch
            {
                Context.Entry(materiai).State = EntityState.Detached;
                throw;
            }

            OnAfterMateriaiCreated(materiai);

            return materiai;
        }
        public async Task ExportMateriaisCategoriaToExcel(Query query = null, string fileName = null)
        {
            navigationManager.NavigateTo(query != null ? query.ToUrl($"export/bibliotecadb/materiaiscategoria/excel(fileName='{(!string.IsNullOrEmpty(fileName) ? UrlEncoder.Default.Encode(fileName) : "Export")}')") : $"export/bibliotecadb/materiaiscategoria/excel(fileName='{(!string.IsNullOrEmpty(fileName) ? UrlEncoder.Default.Encode(fileName) : "Export")}')", true);
        }

        public async Task ExportMateriaisCategoriaToCSV(Query query = null, string fileName = null)
        {
            navigationManager.NavigateTo(query != null ? query.ToUrl($"export/bibliotecadb/materiaiscategoria/csv(fileName='{(!string.IsNullOrEmpty(fileName) ? UrlEncoder.Default.Encode(fileName) : "Export")}')") : $"export/bibliotecadb/materiaiscategoria/csv(fileName='{(!string.IsNullOrEmpty(fileName) ? UrlEncoder.Default.Encode(fileName) : "Export")}')", true);
        }

        partial void OnMateriaisCategoriaRead(ref IQueryable<Models.BibliotecaDb.MateriaisCategoria> items);

        public async Task<IQueryable<Models.BibliotecaDb.MateriaisCategoria>> GetMateriaisCategoria(Query query = null)
        {
            var items = Context.MateriaisCategoria.AsQueryable();

            items = items.Include(i => i.Materiai);

            items = items.Include(i => i.CategoriasMateriai);

            if (query != null)
            {
                if (!string.IsNullOrEmpty(query.Expand))
                {
                    var propertiesToExpand = query.Expand.Split(',');
                    foreach(var p in propertiesToExpand)
                    {
                        items = items.Include(p.Trim());
                    }
                }

                if (!string.IsNullOrEmpty(query.Filter))
                {
                    if (query.FilterParameters != null)
                    {
                        items = items.Where(query.Filter, query.FilterParameters);
                    }
                    else
                    {
                        items = items.Where(query.Filter);
                    }
                }

                if (!string.IsNullOrEmpty(query.OrderBy))
                {
                    items = items.OrderBy(query.OrderBy);
                }

                if (query.Skip.HasValue)
                {
                    items = items.Skip(query.Skip.Value);
                }

                if (query.Top.HasValue)
                {
                    items = items.Take(query.Top.Value);
                }
            }

            OnMateriaisCategoriaRead(ref items);

            return await Task.FromResult(items);
        }

        partial void OnMateriaisCategoriaCreated(Models.BibliotecaDb.MateriaisCategoria item);
        partial void OnAfterMateriaisCategoriaCreated(Models.BibliotecaDb.MateriaisCategoria item);

        public async Task<Models.BibliotecaDb.MateriaisCategoria> CreateMateriaisCategoria(Models.BibliotecaDb.MateriaisCategoria materiaisCategoria)
        {
            OnMateriaisCategoriaCreated(materiaisCategoria);

            var existingItem = Context.MateriaisCategoria
                              .Where(i => i.MaterialID == materiaisCategoria.MaterialID && i.CategoriaID == materiaisCategoria.CategoriaID)
                              .FirstOrDefault();

            if (existingItem != null)
            {
               throw new Exception("Item already available");
            }            

            try
            {
                Context.MateriaisCategoria.Add(materiaisCategoria);
                Context.SaveChanges();
            }
            catch
            {
                Context.Entry(materiaisCategoria).State = EntityState.Detached;
                materiaisCategoria.Materiai = null;
                materiaisCategoria.CategoriasMateriai = null;
                throw;
            }

            OnAfterMateriaisCategoriaCreated(materiaisCategoria);

            return materiaisCategoria;
        }
        public async Task ExportMultaToExcel(Query query = null, string fileName = null)
        {
            navigationManager.NavigateTo(query != null ? query.ToUrl($"export/bibliotecadb/multa/excel(fileName='{(!string.IsNullOrEmpty(fileName) ? UrlEncoder.Default.Encode(fileName) : "Export")}')") : $"export/bibliotecadb/multa/excel(fileName='{(!string.IsNullOrEmpty(fileName) ? UrlEncoder.Default.Encode(fileName) : "Export")}')", true);
        }

        public async Task ExportMultaToCSV(Query query = null, string fileName = null)
        {
            navigationManager.NavigateTo(query != null ? query.ToUrl($"export/bibliotecadb/multa/csv(fileName='{(!string.IsNullOrEmpty(fileName) ? UrlEncoder.Default.Encode(fileName) : "Export")}')") : $"export/bibliotecadb/multa/csv(fileName='{(!string.IsNullOrEmpty(fileName) ? UrlEncoder.Default.Encode(fileName) : "Export")}')", true);
        }

        partial void OnMultaRead(ref IQueryable<Models.BibliotecaDb.Multa> items);

        public async Task<IQueryable<Models.BibliotecaDb.Multa>> GetMulta(Query query = null)
        {
            var items = Context.Multa.AsQueryable();

            items = items.Include(i => i.Emprestimo);

            if (query != null)
            {
                if (!string.IsNullOrEmpty(query.Expand))
                {
                    var propertiesToExpand = query.Expand.Split(',');
                    foreach(var p in propertiesToExpand)
                    {
                        items = items.Include(p.Trim());
                    }
                }

                if (!string.IsNullOrEmpty(query.Filter))
                {
                    if (query.FilterParameters != null)
                    {
                        items = items.Where(query.Filter, query.FilterParameters);
                    }
                    else
                    {
                        items = items.Where(query.Filter);
                    }
                }

                if (!string.IsNullOrEmpty(query.OrderBy))
                {
                    items = items.OrderBy(query.OrderBy);
                }

                if (query.Skip.HasValue)
                {
                    items = items.Skip(query.Skip.Value);
                }

                if (query.Top.HasValue)
                {
                    items = items.Take(query.Top.Value);
                }
            }

            OnMultaRead(ref items);

            return await Task.FromResult(items);
        }

        partial void OnMultaCreated(Models.BibliotecaDb.Multa item);
        partial void OnAfterMultaCreated(Models.BibliotecaDb.Multa item);

        public async Task<Models.BibliotecaDb.Multa> CreateMulta(Models.BibliotecaDb.Multa multa)
        {
            OnMultaCreated(multa);

            var existingItem = Context.Multa
                              .Where(i => i.MultaID == multa.MultaID)
                              .FirstOrDefault();

            if (existingItem != null)
            {
               throw new Exception("Item already available");
            }            

            try
            {
                Context.Multa.Add(multa);
                Context.SaveChanges();
            }
            catch
            {
                Context.Entry(multa).State = EntityState.Detached;
                multa.Emprestimo = null;
                throw;
            }

            OnAfterMultaCreated(multa);

            return multa;
        }
        public async Task ExportNotificacosToExcel(Query query = null, string fileName = null)
        {
            navigationManager.NavigateTo(query != null ? query.ToUrl($"export/bibliotecadb/notificacos/excel(fileName='{(!string.IsNullOrEmpty(fileName) ? UrlEncoder.Default.Encode(fileName) : "Export")}')") : $"export/bibliotecadb/notificacos/excel(fileName='{(!string.IsNullOrEmpty(fileName) ? UrlEncoder.Default.Encode(fileName) : "Export")}')", true);
        }

        public async Task ExportNotificacosToCSV(Query query = null, string fileName = null)
        {
            navigationManager.NavigateTo(query != null ? query.ToUrl($"export/bibliotecadb/notificacos/csv(fileName='{(!string.IsNullOrEmpty(fileName) ? UrlEncoder.Default.Encode(fileName) : "Export")}')") : $"export/bibliotecadb/notificacos/csv(fileName='{(!string.IsNullOrEmpty(fileName) ? UrlEncoder.Default.Encode(fileName) : "Export")}')", true);
        }

        partial void OnNotificacosRead(ref IQueryable<Models.BibliotecaDb.Notificaco> items);

        public async Task<IQueryable<Models.BibliotecaDb.Notificaco>> GetNotificacos(Query query = null)
        {
            var items = Context.Notificacos.AsQueryable();

            items = items.Include(i => i.Utilizadore);

            if (query != null)
            {
                if (!string.IsNullOrEmpty(query.Expand))
                {
                    var propertiesToExpand = query.Expand.Split(',');
                    foreach(var p in propertiesToExpand)
                    {
                        items = items.Include(p.Trim());
                    }
                }

                if (!string.IsNullOrEmpty(query.Filter))
                {
                    if (query.FilterParameters != null)
                    {
                        items = items.Where(query.Filter, query.FilterParameters);
                    }
                    else
                    {
                        items = items.Where(query.Filter);
                    }
                }

                if (!string.IsNullOrEmpty(query.OrderBy))
                {
                    items = items.OrderBy(query.OrderBy);
                }

                if (query.Skip.HasValue)
                {
                    items = items.Skip(query.Skip.Value);
                }

                if (query.Top.HasValue)
                {
                    items = items.Take(query.Top.Value);
                }
            }

            OnNotificacosRead(ref items);

            return await Task.FromResult(items);
        }

        partial void OnNotificacoCreated(Models.BibliotecaDb.Notificaco item);
        partial void OnAfterNotificacoCreated(Models.BibliotecaDb.Notificaco item);

        public async Task<Models.BibliotecaDb.Notificaco> CreateNotificaco(Models.BibliotecaDb.Notificaco notificaco)
        {
            OnNotificacoCreated(notificaco);

            var existingItem = Context.Notificacos
                              .Where(i => i.NotificacaoID == notificaco.NotificacaoID)
                              .FirstOrDefault();

            if (existingItem != null)
            {
               throw new Exception("Item already available");
            }            

            try
            {
                Context.Notificacos.Add(notificaco);
                Context.SaveChanges();
            }
            catch
            {
                Context.Entry(notificaco).State = EntityState.Detached;
                notificaco.Utilizadore = null;
                throw;
            }

            OnAfterNotificacoCreated(notificaco);

            return notificaco;
        }
        public async Task ExportReservasToExcel(Query query = null, string fileName = null)
        {
            navigationManager.NavigateTo(query != null ? query.ToUrl($"export/bibliotecadb/reservas/excel(fileName='{(!string.IsNullOrEmpty(fileName) ? UrlEncoder.Default.Encode(fileName) : "Export")}')") : $"export/bibliotecadb/reservas/excel(fileName='{(!string.IsNullOrEmpty(fileName) ? UrlEncoder.Default.Encode(fileName) : "Export")}')", true);
        }

        public async Task ExportReservasToCSV(Query query = null, string fileName = null)
        {
            navigationManager.NavigateTo(query != null ? query.ToUrl($"export/bibliotecadb/reservas/csv(fileName='{(!string.IsNullOrEmpty(fileName) ? UrlEncoder.Default.Encode(fileName) : "Export")}')") : $"export/bibliotecadb/reservas/csv(fileName='{(!string.IsNullOrEmpty(fileName) ? UrlEncoder.Default.Encode(fileName) : "Export")}')", true);
        }

        partial void OnReservasRead(ref IQueryable<Models.BibliotecaDb.Reserva> items);

        public async Task<IQueryable<Models.BibliotecaDb.Reserva>> GetReservas(Query query = null)
        {
            var items = Context.Reservas.AsQueryable();

            items = items.Include(i => i.Utilizadore);

            items = items.Include(i => i.Materiai);

            if (query != null)
            {
                if (!string.IsNullOrEmpty(query.Expand))
                {
                    var propertiesToExpand = query.Expand.Split(',');
                    foreach(var p in propertiesToExpand)
                    {
                        items = items.Include(p.Trim());
                    }
                }

                if (!string.IsNullOrEmpty(query.Filter))
                {
                    if (query.FilterParameters != null)
                    {
                        items = items.Where(query.Filter, query.FilterParameters);
                    }
                    else
                    {
                        items = items.Where(query.Filter);
                    }
                }

                if (!string.IsNullOrEmpty(query.OrderBy))
                {
                    items = items.OrderBy(query.OrderBy);
                }

                if (query.Skip.HasValue)
                {
                    items = items.Skip(query.Skip.Value);
                }

                if (query.Top.HasValue)
                {
                    items = items.Take(query.Top.Value);
                }
            }

            OnReservasRead(ref items);

            return await Task.FromResult(items);
        }

        partial void OnReservaCreated(Models.BibliotecaDb.Reserva item);
        partial void OnAfterReservaCreated(Models.BibliotecaDb.Reserva item);

        public async Task<Models.BibliotecaDb.Reserva> CreateReserva(Models.BibliotecaDb.Reserva reserva)
        {
            OnReservaCreated(reserva);

            var existingItem = Context.Reservas
                              .Where(i => i.ReservaID == reserva.ReservaID)
                              .FirstOrDefault();

            if (existingItem != null)
            {
               throw new Exception("Item already available");
            }            

            try
            {
                Context.Reservas.Add(reserva);
                Context.SaveChanges();
            }
            catch
            {
                Context.Entry(reserva).State = EntityState.Detached;
                reserva.Utilizadore = null;
                reserva.Materiai = null;
                throw;
            }

            OnAfterReservaCreated(reserva);

            return reserva;
        }

      public async Task ExportSpRegistrarDevolucaosToExcel(int? EmprestimoID, int? FuncionarioID, Query query = null, string fileName = null)
      {
          navigationManager.NavigateTo(query != null ? query.ToUrl($"export/bibliotecadb/spregistrardevolucaos/excel(EmprestimoID={EmprestimoID}, FuncionarioID={FuncionarioID}, fileName='{(!string.IsNullOrEmpty(fileName) ? UrlEncoder.Default.Encode(fileName) : "Export")}')") : $"export/bibliotecadb/spregistrardevolucaos/excel(EmprestimoID={EmprestimoID}, FuncionarioID={FuncionarioID}, fileName='{(!string.IsNullOrEmpty(fileName) ? UrlEncoder.Default.Encode(fileName) : "Export")}')", true);
      }

      public async Task ExportSpRegistrarDevolucaosToCSV(int? EmprestimoID, int? FuncionarioID, Query query = null, string fileName = null)
      {
          navigationManager.NavigateTo(query != null ? query.ToUrl($"export/bibliotecadb/spregistrardevolucaos/csv(EmprestimoID={EmprestimoID}, FuncionarioID={FuncionarioID}, fileName='{(!string.IsNullOrEmpty(fileName) ? UrlEncoder.Default.Encode(fileName) : "Export")}')") : $"export/bibliotecadb/spregistrardevolucaos/csv(EmprestimoID={EmprestimoID}, FuncionarioID={FuncionarioID}, fileName='{(!string.IsNullOrEmpty(fileName) ? UrlEncoder.Default.Encode(fileName) : "Export")}')", true);
      }

      public async Task<IQueryable<Models.BibliotecaDb.SpRegistrarDevolucao>> GetSpRegistrarDevolucaos(int? EmprestimoID, int? FuncionarioID, Query query = null)
      {
          OnSpRegistrarDevolucaosDefaultParams(ref EmprestimoID, ref FuncionarioID);

          var items = Context.SpRegistrarDevolucaos.FromSqlRaw("EXEC [dbo].[sp_RegistrarDevolucao] @EmprestimoID={0}, @FuncionarioID={1}", EmprestimoID, FuncionarioID).ToList().AsQueryable();

          if (query != null)
          {
              if (!string.IsNullOrEmpty(query.Expand))
              {
                  var propertiesToExpand = query.Expand.Split(',');
                  foreach(var p in propertiesToExpand)
                  {
                      items = items.Include(p.Trim());
                  }
              }

              if (!string.IsNullOrEmpty(query.Filter))
              {
                  if (query.FilterParameters != null)
                  {
                      items = items.Where(query.Filter, query.FilterParameters);
                  }
                  else
                  {
                      items = items.Where(query.Filter);
                  }
              }

              if (!string.IsNullOrEmpty(query.OrderBy))
              {
                  items = items.OrderBy(query.OrderBy);
              }

              if (query.Skip.HasValue)
              {
                  items = items.Skip(query.Skip.Value);
              }

              if (query.Top.HasValue)
              {
                  items = items.Take(query.Top.Value);
              }
          }
          
          OnSpRegistrarDevolucaosInvoke(ref items);

          return await Task.FromResult(items);
      }

      partial void OnSpRegistrarDevolucaosDefaultParams(ref int? EmprestimoID, ref int? FuncionarioID);

      partial void OnSpRegistrarDevolucaosInvoke(ref IQueryable<Models.BibliotecaDb.SpRegistrarDevolucao> items);

      public async Task ExportSpRegistrarEmprestimosToExcel(int? UtilizadorID, int? MaterialID, int? FuncionarioID, int? DiasEmprestimo, Query query = null, string fileName = null)
      {
          navigationManager.NavigateTo(query != null ? query.ToUrl($"export/bibliotecadb/spregistraremprestimos/excel(UtilizadorID={UtilizadorID}, MaterialID={MaterialID}, FuncionarioID={FuncionarioID}, DiasEmprestimo={DiasEmprestimo}, fileName='{(!string.IsNullOrEmpty(fileName) ? UrlEncoder.Default.Encode(fileName) : "Export")}')") : $"export/bibliotecadb/spregistraremprestimos/excel(UtilizadorID={UtilizadorID}, MaterialID={MaterialID}, FuncionarioID={FuncionarioID}, DiasEmprestimo={DiasEmprestimo}, fileName='{(!string.IsNullOrEmpty(fileName) ? UrlEncoder.Default.Encode(fileName) : "Export")}')", true);
      }

      public async Task ExportSpRegistrarEmprestimosToCSV(int? UtilizadorID, int? MaterialID, int? FuncionarioID, int? DiasEmprestimo, Query query = null, string fileName = null)
      {
          navigationManager.NavigateTo(query != null ? query.ToUrl($"export/bibliotecadb/spregistraremprestimos/csv(UtilizadorID={UtilizadorID}, MaterialID={MaterialID}, FuncionarioID={FuncionarioID}, DiasEmprestimo={DiasEmprestimo}, fileName='{(!string.IsNullOrEmpty(fileName) ? UrlEncoder.Default.Encode(fileName) : "Export")}')") : $"export/bibliotecadb/spregistraremprestimos/csv(UtilizadorID={UtilizadorID}, MaterialID={MaterialID}, FuncionarioID={FuncionarioID}, DiasEmprestimo={DiasEmprestimo}, fileName='{(!string.IsNullOrEmpty(fileName) ? UrlEncoder.Default.Encode(fileName) : "Export")}')", true);
      }

      public async Task<IQueryable<Models.BibliotecaDb.SpRegistrarEmprestimo>> GetSpRegistrarEmprestimos(int? UtilizadorID, int? MaterialID, int? FuncionarioID, int? DiasEmprestimo, Query query = null)
      {
          OnSpRegistrarEmprestimosDefaultParams(ref UtilizadorID, ref MaterialID, ref FuncionarioID, ref DiasEmprestimo);

          var items = Context.SpRegistrarEmprestimos.FromSqlRaw("EXEC [dbo].[sp_RegistrarEmprestimo] @UtilizadorID={0}, @MaterialID={1}, @FuncionarioID={2}, @DiasEmprestimo={3}", UtilizadorID, MaterialID, FuncionarioID, DiasEmprestimo).ToList().AsQueryable();

          if (query != null)
          {
              if (!string.IsNullOrEmpty(query.Expand))
              {
                  var propertiesToExpand = query.Expand.Split(',');
                  foreach(var p in propertiesToExpand)
                  {
                      items = items.Include(p.Trim());
                  }
              }

              if (!string.IsNullOrEmpty(query.Filter))
              {
                  if (query.FilterParameters != null)
                  {
                      items = items.Where(query.Filter, query.FilterParameters);
                  }
                  else
                  {
                      items = items.Where(query.Filter);
                  }
              }

              if (!string.IsNullOrEmpty(query.OrderBy))
              {
                  items = items.OrderBy(query.OrderBy);
              }

              if (query.Skip.HasValue)
              {
                  items = items.Skip(query.Skip.Value);
              }

              if (query.Top.HasValue)
              {
                  items = items.Take(query.Top.Value);
              }
          }
          
          OnSpRegistrarEmprestimosInvoke(ref items);

          return await Task.FromResult(items);
      }

      partial void OnSpRegistrarEmprestimosDefaultParams(ref int? UtilizadorID, ref int? MaterialID, ref int? FuncionarioID, ref int? DiasEmprestimo);

      partial void OnSpRegistrarEmprestimosInvoke(ref IQueryable<Models.BibliotecaDb.SpRegistrarEmprestimo> items);

      public async Task ExportSpRegistrarReservasToExcel(int? UtilizadorID, int? MaterialID, Query query = null, string fileName = null)
      {
          navigationManager.NavigateTo(query != null ? query.ToUrl($"export/bibliotecadb/spregistrarreservas/excel(UtilizadorID={UtilizadorID}, MaterialID={MaterialID}, fileName='{(!string.IsNullOrEmpty(fileName) ? UrlEncoder.Default.Encode(fileName) : "Export")}')") : $"export/bibliotecadb/spregistrarreservas/excel(UtilizadorID={UtilizadorID}, MaterialID={MaterialID}, fileName='{(!string.IsNullOrEmpty(fileName) ? UrlEncoder.Default.Encode(fileName) : "Export")}')", true);
      }

      public async Task ExportSpRegistrarReservasToCSV(int? UtilizadorID, int? MaterialID, Query query = null, string fileName = null)
      {
          navigationManager.NavigateTo(query != null ? query.ToUrl($"export/bibliotecadb/spregistrarreservas/csv(UtilizadorID={UtilizadorID}, MaterialID={MaterialID}, fileName='{(!string.IsNullOrEmpty(fileName) ? UrlEncoder.Default.Encode(fileName) : "Export")}')") : $"export/bibliotecadb/spregistrarreservas/csv(UtilizadorID={UtilizadorID}, MaterialID={MaterialID}, fileName='{(!string.IsNullOrEmpty(fileName) ? UrlEncoder.Default.Encode(fileName) : "Export")}')", true);
      }

      public async Task<IQueryable<Models.BibliotecaDb.SpRegistrarReserva>> GetSpRegistrarReservas(int? UtilizadorID, int? MaterialID, Query query = null)
      {
          OnSpRegistrarReservasDefaultParams(ref UtilizadorID, ref MaterialID);

          var items = Context.SpRegistrarReservas.FromSqlRaw("EXEC [dbo].[sp_RegistrarReserva] @UtilizadorID={0}, @MaterialID={1}", UtilizadorID, MaterialID).ToList().AsQueryable();

          if (query != null)
          {
              if (!string.IsNullOrEmpty(query.Expand))
              {
                  var propertiesToExpand = query.Expand.Split(',');
                  foreach(var p in propertiesToExpand)
                  {
                      items = items.Include(p.Trim());
                  }
              }

              if (!string.IsNullOrEmpty(query.Filter))
              {
                  if (query.FilterParameters != null)
                  {
                      items = items.Where(query.Filter, query.FilterParameters);
                  }
                  else
                  {
                      items = items.Where(query.Filter);
                  }
              }

              if (!string.IsNullOrEmpty(query.OrderBy))
              {
                  items = items.OrderBy(query.OrderBy);
              }

              if (query.Skip.HasValue)
              {
                  items = items.Skip(query.Skip.Value);
              }

              if (query.Top.HasValue)
              {
                  items = items.Take(query.Top.Value);
              }
          }
          
          OnSpRegistrarReservasInvoke(ref items);

          return await Task.FromResult(items);
      }

      partial void OnSpRegistrarReservasDefaultParams(ref int? UtilizadorID, ref int? MaterialID);

      partial void OnSpRegistrarReservasInvoke(ref IQueryable<Models.BibliotecaDb.SpRegistrarReserva> items);
        public async Task ExportUtilizadoresToExcel(Query query = null, string fileName = null)
        {
            navigationManager.NavigateTo(query != null ? query.ToUrl($"export/bibliotecadb/utilizadores/excel(fileName='{(!string.IsNullOrEmpty(fileName) ? UrlEncoder.Default.Encode(fileName) : "Export")}')") : $"export/bibliotecadb/utilizadores/excel(fileName='{(!string.IsNullOrEmpty(fileName) ? UrlEncoder.Default.Encode(fileName) : "Export")}')", true);
        }

        public async Task ExportUtilizadoresToCSV(Query query = null, string fileName = null)
        {
            navigationManager.NavigateTo(query != null ? query.ToUrl($"export/bibliotecadb/utilizadores/csv(fileName='{(!string.IsNullOrEmpty(fileName) ? UrlEncoder.Default.Encode(fileName) : "Export")}')") : $"export/bibliotecadb/utilizadores/csv(fileName='{(!string.IsNullOrEmpty(fileName) ? UrlEncoder.Default.Encode(fileName) : "Export")}')", true);
        }

        partial void OnUtilizadoresRead(ref IQueryable<Models.BibliotecaDb.Utilizadore> items);

        public async Task<IQueryable<Models.BibliotecaDb.Utilizadore>> GetUtilizadores(Query query = null)
        {
            var items = Context.Utilizadores.AsQueryable();

            if (query != null)
            {
                if (!string.IsNullOrEmpty(query.Expand))
                {
                    var propertiesToExpand = query.Expand.Split(',');
                    foreach(var p in propertiesToExpand)
                    {
                        items = items.Include(p.Trim());
                    }
                }

                if (!string.IsNullOrEmpty(query.Filter))
                {
                    if (query.FilterParameters != null)
                    {
                        items = items.Where(query.Filter, query.FilterParameters);
                    }
                    else
                    {
                        items = items.Where(query.Filter);
                    }
                }

                if (!string.IsNullOrEmpty(query.OrderBy))
                {
                    items = items.OrderBy(query.OrderBy);
                }

                if (query.Skip.HasValue)
                {
                    items = items.Skip(query.Skip.Value);
                }

                if (query.Top.HasValue)
                {
                    items = items.Take(query.Top.Value);
                }
            }

            OnUtilizadoresRead(ref items);

            return await Task.FromResult(items);
        }

        partial void OnUtilizadoreCreated(Models.BibliotecaDb.Utilizadore item);
        partial void OnAfterUtilizadoreCreated(Models.BibliotecaDb.Utilizadore item);

        public async Task<Models.BibliotecaDb.Utilizadore> CreateUtilizadore(Models.BibliotecaDb.Utilizadore utilizadore)
        {
            OnUtilizadoreCreated(utilizadore);

            var existingItem = Context.Utilizadores
                              .Where(i => i.UtilizadorID == utilizadore.UtilizadorID)
                              .FirstOrDefault();

            if (existingItem != null)
            {
               throw new Exception("Item already available");
            }            

            try
            {
                Context.Utilizadores.Add(utilizadore);
                Context.SaveChanges();
            }
            catch
            {
                Context.Entry(utilizadore).State = EntityState.Detached;
                throw;
            }

            OnAfterUtilizadoreCreated(utilizadore);

            return utilizadore;
        }
        public async Task ExportVwMateriaisDisponiveisToExcel(Query query = null, string fileName = null)
        {
            navigationManager.NavigateTo(query != null ? query.ToUrl($"export/bibliotecadb/vwmateriaisdisponiveis/excel(fileName='{(!string.IsNullOrEmpty(fileName) ? UrlEncoder.Default.Encode(fileName) : "Export")}')") : $"export/bibliotecadb/vwmateriaisdisponiveis/excel(fileName='{(!string.IsNullOrEmpty(fileName) ? UrlEncoder.Default.Encode(fileName) : "Export")}')", true);
        }

        public async Task ExportVwMateriaisDisponiveisToCSV(Query query = null, string fileName = null)
        {
            navigationManager.NavigateTo(query != null ? query.ToUrl($"export/bibliotecadb/vwmateriaisdisponiveis/csv(fileName='{(!string.IsNullOrEmpty(fileName) ? UrlEncoder.Default.Encode(fileName) : "Export")}')") : $"export/bibliotecadb/vwmateriaisdisponiveis/csv(fileName='{(!string.IsNullOrEmpty(fileName) ? UrlEncoder.Default.Encode(fileName) : "Export")}')", true);
        }

        partial void OnVwMateriaisDisponiveisRead(ref IQueryable<Models.BibliotecaDb.VwMateriaisDisponivei> items);

        public async Task<IQueryable<Models.BibliotecaDb.VwMateriaisDisponivei>> GetVwMateriaisDisponiveis(Query query = null)
        {
            var items = Context.VwMateriaisDisponiveis.AsQueryable();
            items = items.AsNoTracking();

            if (query != null)
            {
                if (!string.IsNullOrEmpty(query.Expand))
                {
                    var propertiesToExpand = query.Expand.Split(',');
                    foreach(var p in propertiesToExpand)
                    {
                        items = items.Include(p.Trim());
                    }
                }

                if (!string.IsNullOrEmpty(query.Filter))
                {
                    if (query.FilterParameters != null)
                    {
                        items = items.Where(query.Filter, query.FilterParameters);
                    }
                    else
                    {
                        items = items.Where(query.Filter);
                    }
                }

                if (!string.IsNullOrEmpty(query.OrderBy))
                {
                    items = items.OrderBy(query.OrderBy);
                }

                if (query.Skip.HasValue)
                {
                    items = items.Skip(query.Skip.Value);
                }

                if (query.Top.HasValue)
                {
                    items = items.Take(query.Top.Value);
                }
            }

            OnVwMateriaisDisponiveisRead(ref items);

            return await Task.FromResult(items);
        }

        partial void OnAvaliacoDeleted(Models.BibliotecaDb.Avaliaco item);
        partial void OnAfterAvaliacoDeleted(Models.BibliotecaDb.Avaliaco item);

        public async Task<Models.BibliotecaDb.Avaliaco> DeleteAvaliaco(int? avaliacaoId)
        {
            var itemToDelete = Context.Avaliacos
                              .Where(i => i.AvaliacaoID == avaliacaoId)
                              .FirstOrDefault();

            if (itemToDelete == null)
            {
               throw new Exception("Item no longer available");
            }

            OnAvaliacoDeleted(itemToDelete);

            Context.Avaliacos.Remove(itemToDelete);

            try
            {
                Context.SaveChanges();
            }
            catch
            {
                Context.Entry(itemToDelete).State = EntityState.Unchanged;
                throw;
            }

            OnAfterAvaliacoDeleted(itemToDelete);

            return itemToDelete;
        }

        partial void OnAvaliacoGet(Models.BibliotecaDb.Avaliaco item);

        public async Task<Models.BibliotecaDb.Avaliaco> GetAvaliacoByAvaliacaoId(int? avaliacaoId)
        {
            var items = Context.Avaliacos
                              .AsNoTracking()
                              .Where(i => i.AvaliacaoID == avaliacaoId);

            items = items.Include(i => i.Utilizadore);

            items = items.Include(i => i.Materiai);

            var itemToReturn = items.FirstOrDefault();

            OnAvaliacoGet(itemToReturn);

            return await Task.FromResult(itemToReturn);
        }

        public async Task<Models.BibliotecaDb.Avaliaco> CancelAvaliacoChanges(Models.BibliotecaDb.Avaliaco item)
        {
            var entityToCancel = Context.Entry(item);
            if (entityToCancel.State == EntityState.Modified)
            {
              entityToCancel.CurrentValues.SetValues(entityToCancel.OriginalValues);
              entityToCancel.State = EntityState.Unchanged;
            }

            return item;
        }

        partial void OnAvaliacoUpdated(Models.BibliotecaDb.Avaliaco item);
        partial void OnAfterAvaliacoUpdated(Models.BibliotecaDb.Avaliaco item);

        public async Task<Models.BibliotecaDb.Avaliaco> UpdateAvaliaco(int? avaliacaoId, Models.BibliotecaDb.Avaliaco avaliaco)
        {
            OnAvaliacoUpdated(avaliaco);

            var itemToUpdate = Context.Avaliacos
                              .Where(i => i.AvaliacaoID == avaliacaoId)
                              .FirstOrDefault();
            if (itemToUpdate == null)
            {
               throw new Exception("Item no longer available");
            }

            var entryToUpdate = Context.Entry(itemToUpdate);
            entryToUpdate.CurrentValues.SetValues(avaliaco);
            entryToUpdate.State = EntityState.Modified;
            Context.SaveChanges();       

            OnAfterAvaliacoUpdated(avaliaco);

            return avaliaco;
        }

        partial void OnCategoriasMateriaiDeleted(Models.BibliotecaDb.CategoriasMateriai item);
        partial void OnAfterCategoriasMateriaiDeleted(Models.BibliotecaDb.CategoriasMateriai item);

        public async Task<Models.BibliotecaDb.CategoriasMateriai> DeleteCategoriasMateriai(int? categoriaId)
        {
            var itemToDelete = Context.CategoriasMateriais
                              .Where(i => i.CategoriaID == categoriaId)
                              .Include(i => i.MateriaisCategoria)
                              .FirstOrDefault();

            if (itemToDelete == null)
            {
               throw new Exception("Item no longer available");
            }

            OnCategoriasMateriaiDeleted(itemToDelete);

            Context.CategoriasMateriais.Remove(itemToDelete);

            try
            {
                Context.SaveChanges();
            }
            catch
            {
                Context.Entry(itemToDelete).State = EntityState.Unchanged;
                throw;
            }

            OnAfterCategoriasMateriaiDeleted(itemToDelete);

            return itemToDelete;
        }

        partial void OnCategoriasMateriaiGet(Models.BibliotecaDb.CategoriasMateriai item);

        public async Task<Models.BibliotecaDb.CategoriasMateriai> GetCategoriasMateriaiByCategoriaId(int? categoriaId)
        {
            var items = Context.CategoriasMateriais
                              .AsNoTracking()
                              .Where(i => i.CategoriaID == categoriaId);

            var itemToReturn = items.FirstOrDefault();

            OnCategoriasMateriaiGet(itemToReturn);

            return await Task.FromResult(itemToReturn);
        }

        public async Task<Models.BibliotecaDb.CategoriasMateriai> CancelCategoriasMateriaiChanges(Models.BibliotecaDb.CategoriasMateriai item)
        {
            var entityToCancel = Context.Entry(item);
            if (entityToCancel.State == EntityState.Modified)
            {
              entityToCancel.CurrentValues.SetValues(entityToCancel.OriginalValues);
              entityToCancel.State = EntityState.Unchanged;
            }

            return item;
        }

        partial void OnCategoriasMateriaiUpdated(Models.BibliotecaDb.CategoriasMateriai item);
        partial void OnAfterCategoriasMateriaiUpdated(Models.BibliotecaDb.CategoriasMateriai item);

        public async Task<Models.BibliotecaDb.CategoriasMateriai> UpdateCategoriasMateriai(int? categoriaId, Models.BibliotecaDb.CategoriasMateriai categoriasMateriai)
        {
            OnCategoriasMateriaiUpdated(categoriasMateriai);

            var itemToUpdate = Context.CategoriasMateriais
                              .Where(i => i.CategoriaID == categoriaId)
                              .FirstOrDefault();
            if (itemToUpdate == null)
            {
               throw new Exception("Item no longer available");
            }

            var entryToUpdate = Context.Entry(itemToUpdate);
            entryToUpdate.CurrentValues.SetValues(categoriasMateriai);
            entryToUpdate.State = EntityState.Modified;
            Context.SaveChanges();       

            OnAfterCategoriasMateriaiUpdated(categoriasMateriai);

            return categoriasMateriai;
        }

        partial void OnEmprestimoDeleted(Models.BibliotecaDb.Emprestimo item);
        partial void OnAfterEmprestimoDeleted(Models.BibliotecaDb.Emprestimo item);

        public async Task<Models.BibliotecaDb.Emprestimo> DeleteEmprestimo(int? emprestimoId)
        {
            var itemToDelete = Context.Emprestimos
                              .Where(i => i.EmprestimoID == emprestimoId)
                              .Include(i => i.Multa)
                              .FirstOrDefault();

            if (itemToDelete == null)
            {
               throw new Exception("Item no longer available");
            }

            OnEmprestimoDeleted(itemToDelete);

            Context.Emprestimos.Remove(itemToDelete);

            try
            {
                Context.SaveChanges();
            }
            catch
            {
                Context.Entry(itemToDelete).State = EntityState.Unchanged;
                throw;
            }

            OnAfterEmprestimoDeleted(itemToDelete);

            return itemToDelete;
        }

        partial void OnEmprestimoGet(Models.BibliotecaDb.Emprestimo item);

        public async Task<Models.BibliotecaDb.Emprestimo> GetEmprestimoByEmprestimoId(int? emprestimoId)
        {
            var items = Context.Emprestimos
                              .AsNoTracking()
                              .Where(i => i.EmprestimoID == emprestimoId);

            items = items.Include(i => i.Utilizadore);

            items = items.Include(i => i.Materiai);

            items = items.Include(i => i.Utilizadore1);

            items = items.Include(i => i.Utilizadore2);

            var itemToReturn = items.FirstOrDefault();

            OnEmprestimoGet(itemToReturn);

            return await Task.FromResult(itemToReturn);
        }

        public async Task<Models.BibliotecaDb.Emprestimo> CancelEmprestimoChanges(Models.BibliotecaDb.Emprestimo item)
        {
            var entityToCancel = Context.Entry(item);
            if (entityToCancel.State == EntityState.Modified)
            {
              entityToCancel.CurrentValues.SetValues(entityToCancel.OriginalValues);
              entityToCancel.State = EntityState.Unchanged;
            }

            return item;
        }

        partial void OnEmprestimoUpdated(Models.BibliotecaDb.Emprestimo item);
        partial void OnAfterEmprestimoUpdated(Models.BibliotecaDb.Emprestimo item);

        public async Task<Models.BibliotecaDb.Emprestimo> UpdateEmprestimo(int? emprestimoId, Models.BibliotecaDb.Emprestimo emprestimo)
        {
            OnEmprestimoUpdated(emprestimo);

            var itemToUpdate = Context.Emprestimos
                              .Where(i => i.EmprestimoID == emprestimoId)
                              .FirstOrDefault();
            if (itemToUpdate == null)
            {
               throw new Exception("Item no longer available");
            }

            var entryToUpdate = Context.Entry(itemToUpdate);
            entryToUpdate.CurrentValues.SetValues(emprestimo);
            entryToUpdate.State = EntityState.Modified;
            Context.SaveChanges();       

            OnAfterEmprestimoUpdated(emprestimo);

            return emprestimo;
        }

        partial void OnFuncionarioDeleted(Models.BibliotecaDb.Funcionario item);
        partial void OnAfterFuncionarioDeleted(Models.BibliotecaDb.Funcionario item);

        public async Task<Models.BibliotecaDb.Funcionario> DeleteFuncionario(int? funcionarioId)
        {
            var itemToDelete = Context.Funcionarios
                              .Where(i => i.FuncionarioID == funcionarioId)
                              .FirstOrDefault();

            if (itemToDelete == null)
            {
               throw new Exception("Item no longer available");
            }

            OnFuncionarioDeleted(itemToDelete);

            Context.Funcionarios.Remove(itemToDelete);

            try
            {
                Context.SaveChanges();
            }
            catch
            {
                Context.Entry(itemToDelete).State = EntityState.Unchanged;
                throw;
            }

            OnAfterFuncionarioDeleted(itemToDelete);

            return itemToDelete;
        }

        partial void OnFuncionarioGet(Models.BibliotecaDb.Funcionario item);

        public async Task<Models.BibliotecaDb.Funcionario> GetFuncionarioByFuncionarioId(int? funcionarioId)
        {
            var items = Context.Funcionarios
                              .AsNoTracking()
                              .Where(i => i.FuncionarioID == funcionarioId);

            items = items.Include(i => i.Utilizadore);

            var itemToReturn = items.FirstOrDefault();

            OnFuncionarioGet(itemToReturn);

            return await Task.FromResult(itemToReturn);
        }

        public async Task<Models.BibliotecaDb.Funcionario> CancelFuncionarioChanges(Models.BibliotecaDb.Funcionario item)
        {
            var entityToCancel = Context.Entry(item);
            if (entityToCancel.State == EntityState.Modified)
            {
              entityToCancel.CurrentValues.SetValues(entityToCancel.OriginalValues);
              entityToCancel.State = EntityState.Unchanged;
            }

            return item;
        }

        partial void OnFuncionarioUpdated(Models.BibliotecaDb.Funcionario item);
        partial void OnAfterFuncionarioUpdated(Models.BibliotecaDb.Funcionario item);

        public async Task<Models.BibliotecaDb.Funcionario> UpdateFuncionario(int? funcionarioId, Models.BibliotecaDb.Funcionario funcionario)
        {
            OnFuncionarioUpdated(funcionario);

            var itemToUpdate = Context.Funcionarios
                              .Where(i => i.FuncionarioID == funcionarioId)
                              .FirstOrDefault();
            if (itemToUpdate == null)
            {
               throw new Exception("Item no longer available");
            }

            var entryToUpdate = Context.Entry(itemToUpdate);
            entryToUpdate.CurrentValues.SetValues(funcionario);
            entryToUpdate.State = EntityState.Modified;
            Context.SaveChanges();       

            OnAfterFuncionarioUpdated(funcionario);

            return funcionario;
        }

        partial void OnMateriaiDeleted(Models.BibliotecaDb.Materiai item);
        partial void OnAfterMateriaiDeleted(Models.BibliotecaDb.Materiai item);

        public async Task<Models.BibliotecaDb.Materiai> DeleteMateriai(int? materialId)
        {
            var itemToDelete = Context.Materiais
                              .Where(i => i.MaterialID == materialId)
                              .Include(i => i.Emprestimos)
                              .Include(i => i.Reservas)
                              .Include(i => i.Avaliacos)
                              .Include(i => i.MateriaisCategoria)
                              .FirstOrDefault();

            if (itemToDelete == null)
            {
               throw new Exception("Item no longer available");
            }

            OnMateriaiDeleted(itemToDelete);

            Context.Materiais.Remove(itemToDelete);

            try
            {
                Context.SaveChanges();
            }
            catch
            {
                Context.Entry(itemToDelete).State = EntityState.Unchanged;
                throw;
            }

            OnAfterMateriaiDeleted(itemToDelete);

            return itemToDelete;
        }

        partial void OnMateriaiGet(Models.BibliotecaDb.Materiai item);

        public async Task<Models.BibliotecaDb.Materiai> GetMateriaiByMaterialId(int? materialId)
        {
            var items = Context.Materiais
                              .AsNoTracking()
                              .Where(i => i.MaterialID == materialId);

            var itemToReturn = items.FirstOrDefault();

            OnMateriaiGet(itemToReturn);

            return await Task.FromResult(itemToReturn);
        }

        public async Task<Models.BibliotecaDb.Materiai> CancelMateriaiChanges(Models.BibliotecaDb.Materiai item)
        {
            var entityToCancel = Context.Entry(item);
            if (entityToCancel.State == EntityState.Modified)
            {
              entityToCancel.CurrentValues.SetValues(entityToCancel.OriginalValues);
              entityToCancel.State = EntityState.Unchanged;
            }

            return item;
        }

        partial void OnMateriaiUpdated(Models.BibliotecaDb.Materiai item);
        partial void OnAfterMateriaiUpdated(Models.BibliotecaDb.Materiai item);

        public async Task<Models.BibliotecaDb.Materiai> UpdateMateriai(int? materialId, Models.BibliotecaDb.Materiai materiai)
        {
            OnMateriaiUpdated(materiai);

            var itemToUpdate = Context.Materiais
                              .Where(i => i.MaterialID == materialId)
                              .FirstOrDefault();
            if (itemToUpdate == null)
            {
               throw new Exception("Item no longer available");
            }

            var entryToUpdate = Context.Entry(itemToUpdate);
            entryToUpdate.CurrentValues.SetValues(materiai);
            entryToUpdate.State = EntityState.Modified;
            Context.SaveChanges();       

            OnAfterMateriaiUpdated(materiai);

            return materiai;
        }

        partial void OnMateriaisCategoriaDeleted(Models.BibliotecaDb.MateriaisCategoria item);
        partial void OnAfterMateriaisCategoriaDeleted(Models.BibliotecaDb.MateriaisCategoria item);

        public async Task<Models.BibliotecaDb.MateriaisCategoria> DeleteMateriaisCategoria(int? materialId, int? categoriaId)
        {
            var itemToDelete = Context.MateriaisCategoria
                              .Where(i => i.MaterialID == materialId && i.CategoriaID == categoriaId)
                              .FirstOrDefault();

            if (itemToDelete == null)
            {
               throw new Exception("Item no longer available");
            }

            OnMateriaisCategoriaDeleted(itemToDelete);

            Context.MateriaisCategoria.Remove(itemToDelete);

            try
            {
                Context.SaveChanges();
            }
            catch
            {
                Context.Entry(itemToDelete).State = EntityState.Unchanged;
                throw;
            }

            OnAfterMateriaisCategoriaDeleted(itemToDelete);

            return itemToDelete;
        }

        partial void OnMateriaisCategoriaGet(Models.BibliotecaDb.MateriaisCategoria item);

        public async Task<Models.BibliotecaDb.MateriaisCategoria> GetMateriaisCategoriaByMaterialIdAndCategoriaId(int? materialId, int? categoriaId)
        {
            var items = Context.MateriaisCategoria
                              .AsNoTracking()
                              .Where(i => i.MaterialID == materialId && i.CategoriaID == categoriaId);

            items = items.Include(i => i.Materiai);

            items = items.Include(i => i.CategoriasMateriai);

            var itemToReturn = items.FirstOrDefault();

            OnMateriaisCategoriaGet(itemToReturn);

            return await Task.FromResult(itemToReturn);
        }

        public async Task<Models.BibliotecaDb.MateriaisCategoria> CancelMateriaisCategoriaChanges(Models.BibliotecaDb.MateriaisCategoria item)
        {
            var entityToCancel = Context.Entry(item);
            if (entityToCancel.State == EntityState.Modified)
            {
              entityToCancel.CurrentValues.SetValues(entityToCancel.OriginalValues);
              entityToCancel.State = EntityState.Unchanged;
            }

            return item;
        }

        partial void OnMateriaisCategoriaUpdated(Models.BibliotecaDb.MateriaisCategoria item);
        partial void OnAfterMateriaisCategoriaUpdated(Models.BibliotecaDb.MateriaisCategoria item);

        public async Task<Models.BibliotecaDb.MateriaisCategoria> UpdateMateriaisCategoria(int? materialId, int? categoriaId, Models.BibliotecaDb.MateriaisCategoria materiaisCategoria)
        {
            OnMateriaisCategoriaUpdated(materiaisCategoria);

            var itemToUpdate = Context.MateriaisCategoria
                              .Where(i => i.MaterialID == materialId && i.CategoriaID == categoriaId)
                              .FirstOrDefault();
            if (itemToUpdate == null)
            {
               throw new Exception("Item no longer available");
            }

            var entryToUpdate = Context.Entry(itemToUpdate);
            entryToUpdate.CurrentValues.SetValues(materiaisCategoria);
            entryToUpdate.State = EntityState.Modified;
            Context.SaveChanges();       

            OnAfterMateriaisCategoriaUpdated(materiaisCategoria);

            return materiaisCategoria;
        }

        partial void OnMultaDeleted(Models.BibliotecaDb.Multa item);
        partial void OnAfterMultaDeleted(Models.BibliotecaDb.Multa item);

        public async Task<Models.BibliotecaDb.Multa> DeleteMulta(int? multaId)
        {
            var itemToDelete = Context.Multa
                              .Where(i => i.MultaID == multaId)
                              .FirstOrDefault();

            if (itemToDelete == null)
            {
               throw new Exception("Item no longer available");
            }

            OnMultaDeleted(itemToDelete);

            Context.Multa.Remove(itemToDelete);

            try
            {
                Context.SaveChanges();
            }
            catch
            {
                Context.Entry(itemToDelete).State = EntityState.Unchanged;
                throw;
            }

            OnAfterMultaDeleted(itemToDelete);

            return itemToDelete;
        }

        partial void OnMultaGet(Models.BibliotecaDb.Multa item);

        public async Task<Models.BibliotecaDb.Multa> GetMultaByMultaId(int? multaId)
        {
            var items = Context.Multa
                              .AsNoTracking()
                              .Where(i => i.MultaID == multaId);

            items = items.Include(i => i.Emprestimo);

            var itemToReturn = items.FirstOrDefault();

            OnMultaGet(itemToReturn);

            return await Task.FromResult(itemToReturn);
        }

        public async Task<Models.BibliotecaDb.Multa> CancelMultaChanges(Models.BibliotecaDb.Multa item)
        {
            var entityToCancel = Context.Entry(item);
            if (entityToCancel.State == EntityState.Modified)
            {
              entityToCancel.CurrentValues.SetValues(entityToCancel.OriginalValues);
              entityToCancel.State = EntityState.Unchanged;
            }

            return item;
        }

        partial void OnMultaUpdated(Models.BibliotecaDb.Multa item);
        partial void OnAfterMultaUpdated(Models.BibliotecaDb.Multa item);

        public async Task<Models.BibliotecaDb.Multa> UpdateMulta(int? multaId, Models.BibliotecaDb.Multa multa)
        {
            OnMultaUpdated(multa);

            var itemToUpdate = Context.Multa
                              .Where(i => i.MultaID == multaId)
                              .FirstOrDefault();
            if (itemToUpdate == null)
            {
               throw new Exception("Item no longer available");
            }

            var entryToUpdate = Context.Entry(itemToUpdate);
            entryToUpdate.CurrentValues.SetValues(multa);
            entryToUpdate.State = EntityState.Modified;
            Context.SaveChanges();       

            OnAfterMultaUpdated(multa);

            return multa;
        }

        partial void OnNotificacoDeleted(Models.BibliotecaDb.Notificaco item);
        partial void OnAfterNotificacoDeleted(Models.BibliotecaDb.Notificaco item);

        public async Task<Models.BibliotecaDb.Notificaco> DeleteNotificaco(int? notificacaoId)
        {
            var itemToDelete = Context.Notificacos
                              .Where(i => i.NotificacaoID == notificacaoId)
                              .FirstOrDefault();

            if (itemToDelete == null)
            {
               throw new Exception("Item no longer available");
            }

            OnNotificacoDeleted(itemToDelete);

            Context.Notificacos.Remove(itemToDelete);

            try
            {
                Context.SaveChanges();
            }
            catch
            {
                Context.Entry(itemToDelete).State = EntityState.Unchanged;
                throw;
            }

            OnAfterNotificacoDeleted(itemToDelete);

            return itemToDelete;
        }

        partial void OnNotificacoGet(Models.BibliotecaDb.Notificaco item);

        public async Task<Models.BibliotecaDb.Notificaco> GetNotificacoByNotificacaoId(int? notificacaoId)
        {
            var items = Context.Notificacos
                              .AsNoTracking()
                              .Where(i => i.NotificacaoID == notificacaoId);

            items = items.Include(i => i.Utilizadore);

            var itemToReturn = items.FirstOrDefault();

            OnNotificacoGet(itemToReturn);

            return await Task.FromResult(itemToReturn);
        }

        public async Task<Models.BibliotecaDb.Notificaco> CancelNotificacoChanges(Models.BibliotecaDb.Notificaco item)
        {
            var entityToCancel = Context.Entry(item);
            if (entityToCancel.State == EntityState.Modified)
            {
              entityToCancel.CurrentValues.SetValues(entityToCancel.OriginalValues);
              entityToCancel.State = EntityState.Unchanged;
            }

            return item;
        }

        partial void OnNotificacoUpdated(Models.BibliotecaDb.Notificaco item);
        partial void OnAfterNotificacoUpdated(Models.BibliotecaDb.Notificaco item);

        public async Task<Models.BibliotecaDb.Notificaco> UpdateNotificaco(int? notificacaoId, Models.BibliotecaDb.Notificaco notificaco)
        {
            OnNotificacoUpdated(notificaco);

            var itemToUpdate = Context.Notificacos
                              .Where(i => i.NotificacaoID == notificacaoId)
                              .FirstOrDefault();
            if (itemToUpdate == null)
            {
               throw new Exception("Item no longer available");
            }

            var entryToUpdate = Context.Entry(itemToUpdate);
            entryToUpdate.CurrentValues.SetValues(notificaco);
            entryToUpdate.State = EntityState.Modified;
            Context.SaveChanges();       

            OnAfterNotificacoUpdated(notificaco);

            return notificaco;
        }

        partial void OnReservaDeleted(Models.BibliotecaDb.Reserva item);
        partial void OnAfterReservaDeleted(Models.BibliotecaDb.Reserva item);

        public async Task<Models.BibliotecaDb.Reserva> DeleteReserva(int? reservaId)
        {
            var itemToDelete = Context.Reservas
                              .Where(i => i.ReservaID == reservaId)
                              .FirstOrDefault();

            if (itemToDelete == null)
            {
               throw new Exception("Item no longer available");
            }

            OnReservaDeleted(itemToDelete);

            Context.Reservas.Remove(itemToDelete);

            try
            {
                Context.SaveChanges();
            }
            catch
            {
                Context.Entry(itemToDelete).State = EntityState.Unchanged;
                throw;
            }

            OnAfterReservaDeleted(itemToDelete);

            return itemToDelete;
        }

        partial void OnReservaGet(Models.BibliotecaDb.Reserva item);

        public async Task<Models.BibliotecaDb.Reserva> GetReservaByReservaId(int? reservaId)
        {
            var items = Context.Reservas
                              .AsNoTracking()
                              .Where(i => i.ReservaID == reservaId);

            items = items.Include(i => i.Utilizadore);

            items = items.Include(i => i.Materiai);

            var itemToReturn = items.FirstOrDefault();

            OnReservaGet(itemToReturn);

            return await Task.FromResult(itemToReturn);
        }

        public async Task<Models.BibliotecaDb.Reserva> CancelReservaChanges(Models.BibliotecaDb.Reserva item)
        {
            var entityToCancel = Context.Entry(item);
            if (entityToCancel.State == EntityState.Modified)
            {
              entityToCancel.CurrentValues.SetValues(entityToCancel.OriginalValues);
              entityToCancel.State = EntityState.Unchanged;
            }

            return item;
        }

        partial void OnReservaUpdated(Models.BibliotecaDb.Reserva item);
        partial void OnAfterReservaUpdated(Models.BibliotecaDb.Reserva item);

        public async Task<Models.BibliotecaDb.Reserva> UpdateReserva(int? reservaId, Models.BibliotecaDb.Reserva reserva)
        {
            OnReservaUpdated(reserva);

            var itemToUpdate = Context.Reservas
                              .Where(i => i.ReservaID == reservaId)
                              .FirstOrDefault();
            if (itemToUpdate == null)
            {
               throw new Exception("Item no longer available");
            }

            var entryToUpdate = Context.Entry(itemToUpdate);
            entryToUpdate.CurrentValues.SetValues(reserva);
            entryToUpdate.State = EntityState.Modified;
            Context.SaveChanges();       

            OnAfterReservaUpdated(reserva);

            return reserva;
        }

        partial void OnUtilizadoreDeleted(Models.BibliotecaDb.Utilizadore item);
        partial void OnAfterUtilizadoreDeleted(Models.BibliotecaDb.Utilizadore item);

        public async Task<Models.BibliotecaDb.Utilizadore> DeleteUtilizadore(int? utilizadorId)
        {
            var itemToDelete = Context.Utilizadores
                              .Where(i => i.UtilizadorID == utilizadorId)
                              .Include(i => i.Emprestimos)
                              .Include(i => i.Emprestimos1)
                              .Include(i => i.Emprestimos2)
                              .Include(i => i.Reservas)
                              .Include(i => i.Avaliacos)
                              .Include(i => i.Funcionarios)
                              .Include(i => i.Notificacos)
                              .FirstOrDefault();

            if (itemToDelete == null)
            {
               throw new Exception("Item no longer available");
            }

            OnUtilizadoreDeleted(itemToDelete);

            Context.Utilizadores.Remove(itemToDelete);

            try
            {
                Context.SaveChanges();
            }
            catch
            {
                Context.Entry(itemToDelete).State = EntityState.Unchanged;
                throw;
            }

            OnAfterUtilizadoreDeleted(itemToDelete);

            return itemToDelete;
        }

        partial void OnUtilizadoreGet(Models.BibliotecaDb.Utilizadore item);

        public async Task<Models.BibliotecaDb.Utilizadore> GetUtilizadoreByUtilizadorId(int? utilizadorId)
        {
            var items = Context.Utilizadores
                              .AsNoTracking()
                              .Where(i => i.UtilizadorID == utilizadorId);

            var itemToReturn = items.FirstOrDefault();

            OnUtilizadoreGet(itemToReturn);

            return await Task.FromResult(itemToReturn);
        }

        public async Task<Models.BibliotecaDb.Utilizadore> CancelUtilizadoreChanges(Models.BibliotecaDb.Utilizadore item)
        {
            var entityToCancel = Context.Entry(item);
            if (entityToCancel.State == EntityState.Modified)
            {
              entityToCancel.CurrentValues.SetValues(entityToCancel.OriginalValues);
              entityToCancel.State = EntityState.Unchanged;
            }

            return item;
        }

        partial void OnUtilizadoreUpdated(Models.BibliotecaDb.Utilizadore item);
        partial void OnAfterUtilizadoreUpdated(Models.BibliotecaDb.Utilizadore item);

        public async Task<Models.BibliotecaDb.Utilizadore> UpdateUtilizadore(int? utilizadorId, Models.BibliotecaDb.Utilizadore utilizadore)
        {
            OnUtilizadoreUpdated(utilizadore);

            var itemToUpdate = Context.Utilizadores
                              .Where(i => i.UtilizadorID == utilizadorId)
                              .FirstOrDefault();
            if (itemToUpdate == null)
            {
               throw new Exception("Item no longer available");
            }

            var entryToUpdate = Context.Entry(itemToUpdate);
            entryToUpdate.CurrentValues.SetValues(utilizadore);
            entryToUpdate.State = EntityState.Modified;
            Context.SaveChanges();       

            OnAfterUtilizadoreUpdated(utilizadore);

            return utilizadore;
        }
    }
}
