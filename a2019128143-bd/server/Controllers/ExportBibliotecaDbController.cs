using System;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using A2019128143.Data;

namespace A2019128143
{
    public partial class ExportBibliotecaDbController : ExportController
    {
        private readonly BibliotecaDbContext context;
        private readonly BibliotecaDbService service;
        public ExportBibliotecaDbController(BibliotecaDbContext context, BibliotecaDbService service)
        {
            this.service = service;
            this.context = context;
        }

        [HttpGet("/export/BibliotecaDb/avaliacos/csv")]
        [HttpGet("/export/BibliotecaDb/avaliacos/csv(fileName='{fileName}')")]
        public async System.Threading.Tasks.Task<FileStreamResult> ExportAvaliacosToCSV(string fileName = null)
        {
            return ToCSV(ApplyQuery(await service.GetAvaliacos(), Request.Query, false), fileName);
        }

        [HttpGet("/export/BibliotecaDb/avaliacos/excel")]
        [HttpGet("/export/BibliotecaDb/avaliacos/excel(fileName='{fileName}')")]
        public async System.Threading.Tasks.Task<FileStreamResult> ExportAvaliacosToExcel(string fileName = null)
        {
            return ToExcel(ApplyQuery(await service.GetAvaliacos(), Request.Query, false), fileName);
        }
        [HttpGet("/export/BibliotecaDb/categoriasmateriais/csv")]
        [HttpGet("/export/BibliotecaDb/categoriasmateriais/csv(fileName='{fileName}')")]
        public async System.Threading.Tasks.Task<FileStreamResult> ExportCategoriasMateriaisToCSV(string fileName = null)
        {
            return ToCSV(ApplyQuery(await service.GetCategoriasMateriais(), Request.Query, false), fileName);
        }

        [HttpGet("/export/BibliotecaDb/categoriasmateriais/excel")]
        [HttpGet("/export/BibliotecaDb/categoriasmateriais/excel(fileName='{fileName}')")]
        public async System.Threading.Tasks.Task<FileStreamResult> ExportCategoriasMateriaisToExcel(string fileName = null)
        {
            return ToExcel(ApplyQuery(await service.GetCategoriasMateriais(), Request.Query, false), fileName);
        }
        [HttpGet("/export/BibliotecaDb/emprestimos/csv")]
        [HttpGet("/export/BibliotecaDb/emprestimos/csv(fileName='{fileName}')")]
        public async System.Threading.Tasks.Task<FileStreamResult> ExportEmprestimosToCSV(string fileName = null)
        {
            return ToCSV(ApplyQuery(await service.GetEmprestimos(), Request.Query, false), fileName);
        }

        [HttpGet("/export/BibliotecaDb/emprestimos/excel")]
        [HttpGet("/export/BibliotecaDb/emprestimos/excel(fileName='{fileName}')")]
        public async System.Threading.Tasks.Task<FileStreamResult> ExportEmprestimosToExcel(string fileName = null)
        {
            return ToExcel(ApplyQuery(await service.GetEmprestimos(), Request.Query, false), fileName);
        }
        [HttpGet("/export/BibliotecaDb/funcionarios/csv")]
        [HttpGet("/export/BibliotecaDb/funcionarios/csv(fileName='{fileName}')")]
        public async System.Threading.Tasks.Task<FileStreamResult> ExportFuncionariosToCSV(string fileName = null)
        {
            return ToCSV(ApplyQuery(await service.GetFuncionarios(), Request.Query, false), fileName);
        }

        [HttpGet("/export/BibliotecaDb/funcionarios/excel")]
        [HttpGet("/export/BibliotecaDb/funcionarios/excel(fileName='{fileName}')")]
        public async System.Threading.Tasks.Task<FileStreamResult> ExportFuncionariosToExcel(string fileName = null)
        {
            return ToExcel(ApplyQuery(await service.GetFuncionarios(), Request.Query, false), fileName);
        }
        [HttpGet("/export/BibliotecaDb/materiais/csv")]
        [HttpGet("/export/BibliotecaDb/materiais/csv(fileName='{fileName}')")]
        public async System.Threading.Tasks.Task<FileStreamResult> ExportMateriaisToCSV(string fileName = null)
        {
            return ToCSV(ApplyQuery(await service.GetMateriais(), Request.Query, false), fileName);
        }

        [HttpGet("/export/BibliotecaDb/materiais/excel")]
        [HttpGet("/export/BibliotecaDb/materiais/excel(fileName='{fileName}')")]
        public async System.Threading.Tasks.Task<FileStreamResult> ExportMateriaisToExcel(string fileName = null)
        {
            return ToExcel(ApplyQuery(await service.GetMateriais(), Request.Query, false), fileName);
        }
        [HttpGet("/export/BibliotecaDb/materiaiscategoria/csv")]
        [HttpGet("/export/BibliotecaDb/materiaiscategoria/csv(fileName='{fileName}')")]
        public async System.Threading.Tasks.Task<FileStreamResult> ExportMateriaisCategoriaToCSV(string fileName = null)
        {
            return ToCSV(ApplyQuery(await service.GetMateriaisCategoria(), Request.Query, false), fileName);
        }

        [HttpGet("/export/BibliotecaDb/materiaiscategoria/excel")]
        [HttpGet("/export/BibliotecaDb/materiaiscategoria/excel(fileName='{fileName}')")]
        public async System.Threading.Tasks.Task<FileStreamResult> ExportMateriaisCategoriaToExcel(string fileName = null)
        {
            return ToExcel(ApplyQuery(await service.GetMateriaisCategoria(), Request.Query, false), fileName);
        }
        [HttpGet("/export/BibliotecaDb/multa/csv")]
        [HttpGet("/export/BibliotecaDb/multa/csv(fileName='{fileName}')")]
        public async System.Threading.Tasks.Task<FileStreamResult> ExportMultaToCSV(string fileName = null)
        {
            return ToCSV(ApplyQuery(await service.GetMulta(), Request.Query, false), fileName);
        }

        [HttpGet("/export/BibliotecaDb/multa/excel")]
        [HttpGet("/export/BibliotecaDb/multa/excel(fileName='{fileName}')")]
        public async System.Threading.Tasks.Task<FileStreamResult> ExportMultaToExcel(string fileName = null)
        {
            return ToExcel(ApplyQuery(await service.GetMulta(), Request.Query, false), fileName);
        }
        [HttpGet("/export/BibliotecaDb/notificacos/csv")]
        [HttpGet("/export/BibliotecaDb/notificacos/csv(fileName='{fileName}')")]
        public async System.Threading.Tasks.Task<FileStreamResult> ExportNotificacosToCSV(string fileName = null)
        {
            return ToCSV(ApplyQuery(await service.GetNotificacos(), Request.Query, false), fileName);
        }

        [HttpGet("/export/BibliotecaDb/notificacos/excel")]
        [HttpGet("/export/BibliotecaDb/notificacos/excel(fileName='{fileName}')")]
        public async System.Threading.Tasks.Task<FileStreamResult> ExportNotificacosToExcel(string fileName = null)
        {
            return ToExcel(ApplyQuery(await service.GetNotificacos(), Request.Query, false), fileName);
        }
        [HttpGet("/export/BibliotecaDb/reservas/csv")]
        [HttpGet("/export/BibliotecaDb/reservas/csv(fileName='{fileName}')")]
        public async System.Threading.Tasks.Task<FileStreamResult> ExportReservasToCSV(string fileName = null)
        {
            return ToCSV(ApplyQuery(await service.GetReservas(), Request.Query, false), fileName);
        }

        [HttpGet("/export/BibliotecaDb/reservas/excel")]
        [HttpGet("/export/BibliotecaDb/reservas/excel(fileName='{fileName}')")]
        public async System.Threading.Tasks.Task<FileStreamResult> ExportReservasToExcel(string fileName = null)
        {
            return ToExcel(ApplyQuery(await service.GetReservas(), Request.Query, false), fileName);
        }

        [HttpGet("/export/BibliotecaDb/spregistrardevolucaos/csv(EmprestimoID={EmprestimoID}, FuncionarioID={FuncionarioID}, fileName='{fileName}')")]
        public async System.Threading.Tasks.Task<FileStreamResult> ExportSpRegistrarDevolucaosToCSV(int? EmprestimoID, int? FuncionarioID, string fileName = null)
        {
            return ToCSV(ApplyQuery(await service.GetSpRegistrarDevolucaos(EmprestimoID, FuncionarioID), Request.Query, true), fileName);
        }

        [HttpGet("/export/BibliotecaDb/spregistrardevolucaos/excel(EmprestimoID={EmprestimoID}, FuncionarioID={FuncionarioID}, fileName='{fileName}')")]
        public async System.Threading.Tasks.Task<FileStreamResult> ExportSpRegistrarDevolucaosToExcel(int? EmprestimoID, int? FuncionarioID, string fileName = null)
        {
            return ToExcel(ApplyQuery(await service.GetSpRegistrarDevolucaos(EmprestimoID, FuncionarioID), Request.Query, true), fileName);
        }

        [HttpGet("/export/BibliotecaDb/spregistraremprestimos/csv(UtilizadorID={UtilizadorID}, MaterialID={MaterialID}, FuncionarioID={FuncionarioID}, DiasEmprestimo={DiasEmprestimo}, fileName='{fileName}')")]
        public async System.Threading.Tasks.Task<FileStreamResult> ExportSpRegistrarEmprestimosToCSV(int? UtilizadorID, int? MaterialID, int? FuncionarioID, int? DiasEmprestimo, string fileName = null)
        {
            return ToCSV(ApplyQuery(await service.GetSpRegistrarEmprestimos(UtilizadorID, MaterialID, FuncionarioID, DiasEmprestimo), Request.Query, true), fileName);
        }

        [HttpGet("/export/BibliotecaDb/spregistraremprestimos/excel(UtilizadorID={UtilizadorID}, MaterialID={MaterialID}, FuncionarioID={FuncionarioID}, DiasEmprestimo={DiasEmprestimo}, fileName='{fileName}')")]
        public async System.Threading.Tasks.Task<FileStreamResult> ExportSpRegistrarEmprestimosToExcel(int? UtilizadorID, int? MaterialID, int? FuncionarioID, int? DiasEmprestimo, string fileName = null)
        {
            return ToExcel(ApplyQuery(await service.GetSpRegistrarEmprestimos(UtilizadorID, MaterialID, FuncionarioID, DiasEmprestimo), Request.Query, true), fileName);
        }

        [HttpGet("/export/BibliotecaDb/spregistrarreservas/csv(UtilizadorID={UtilizadorID}, MaterialID={MaterialID}, fileName='{fileName}')")]
        public async System.Threading.Tasks.Task<FileStreamResult> ExportSpRegistrarReservasToCSV(int? UtilizadorID, int? MaterialID, string fileName = null)
        {
            return ToCSV(ApplyQuery(await service.GetSpRegistrarReservas(UtilizadorID, MaterialID), Request.Query, true), fileName);
        }

        [HttpGet("/export/BibliotecaDb/spregistrarreservas/excel(UtilizadorID={UtilizadorID}, MaterialID={MaterialID}, fileName='{fileName}')")]
        public async System.Threading.Tasks.Task<FileStreamResult> ExportSpRegistrarReservasToExcel(int? UtilizadorID, int? MaterialID, string fileName = null)
        {
            return ToExcel(ApplyQuery(await service.GetSpRegistrarReservas(UtilizadorID, MaterialID), Request.Query, true), fileName);
        }
        [HttpGet("/export/BibliotecaDb/utilizadores/csv")]
        [HttpGet("/export/BibliotecaDb/utilizadores/csv(fileName='{fileName}')")]
        public async System.Threading.Tasks.Task<FileStreamResult> ExportUtilizadoresToCSV(string fileName = null)
        {
            return ToCSV(ApplyQuery(await service.GetUtilizadores(), Request.Query, false), fileName);
        }

        [HttpGet("/export/BibliotecaDb/utilizadores/excel")]
        [HttpGet("/export/BibliotecaDb/utilizadores/excel(fileName='{fileName}')")]
        public async System.Threading.Tasks.Task<FileStreamResult> ExportUtilizadoresToExcel(string fileName = null)
        {
            return ToExcel(ApplyQuery(await service.GetUtilizadores(), Request.Query, false), fileName);
        }
        [HttpGet("/export/BibliotecaDb/vwmateriaisdisponiveis/csv")]
        [HttpGet("/export/BibliotecaDb/vwmateriaisdisponiveis/csv(fileName='{fileName}')")]
        public async System.Threading.Tasks.Task<FileStreamResult> ExportVwMateriaisDisponiveisToCSV(string fileName = null)
        {
            return ToCSV(ApplyQuery(await service.GetVwMateriaisDisponiveis(), Request.Query, true), fileName);
        }

        [HttpGet("/export/BibliotecaDb/vwmateriaisdisponiveis/excel")]
        [HttpGet("/export/BibliotecaDb/vwmateriaisdisponiveis/excel(fileName='{fileName}')")]
        public async System.Threading.Tasks.Task<FileStreamResult> ExportVwMateriaisDisponiveisToExcel(string fileName = null)
        {
            return ToExcel(ApplyQuery(await service.GetVwMateriaisDisponiveis(), Request.Query, true), fileName);
        }
    }
}
