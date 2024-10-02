using System;
using System.Linq;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.JSInterop;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Web;
using Radzen;
using Radzen.Blazor;
using A2019128143.Models.BibliotecaDb;
using Microsoft.EntityFrameworkCore;

namespace A2019128143.Pages
{
    public partial class EditEmprestimoComponent : ComponentBase
    {
        [Parameter(CaptureUnmatchedValues = true)]
        public IReadOnlyDictionary<string, dynamic> Attributes { get; set; }

        public void Reload()
        {
            InvokeAsync(StateHasChanged);
        }

        public void OnPropertyChanged(PropertyChangedEventArgs args)
        {
        }

        [Inject]
        protected IJSRuntime JSRuntime { get; set; }

        [Inject]
        protected NavigationManager UriHelper { get; set; }

        [Inject]
        protected DialogService DialogService { get; set; }

        [Inject]
        protected TooltipService TooltipService { get; set; }

        [Inject]
        protected ContextMenuService ContextMenuService { get; set; }

        [Inject]
        protected NotificationService NotificationService { get; set; }

        [Inject]
        protected BibliotecaDbService BibliotecaDb { get; set; }

        [Parameter]
        public dynamic EmprestimoID { get; set; }

        A2019128143.Models.BibliotecaDb.Emprestimo _emprestimo;
        protected A2019128143.Models.BibliotecaDb.Emprestimo emprestimo
        {
            get
            {
                return _emprestimo;
            }
            set
            {
                if (!object.Equals(_emprestimo, value))
                {
                    var args = new PropertyChangedEventArgs(){ Name = "emprestimo", NewValue = value, OldValue = _emprestimo };
                    _emprestimo = value;
                    OnPropertyChanged(args);
                    Reload();
                }
            }
        }

        IEnumerable<A2019128143.Models.BibliotecaDb.Utilizadore> _getUtilizadoresForUtilizadorIDResult;
        protected IEnumerable<A2019128143.Models.BibliotecaDb.Utilizadore> getUtilizadoresForUtilizadorIDResult
        {
            get
            {
                return _getUtilizadoresForUtilizadorIDResult;
            }
            set
            {
                if (!object.Equals(_getUtilizadoresForUtilizadorIDResult, value))
                {
                    var args = new PropertyChangedEventArgs(){ Name = "getUtilizadoresForUtilizadorIDResult", NewValue = value, OldValue = _getUtilizadoresForUtilizadorIDResult };
                    _getUtilizadoresForUtilizadorIDResult = value;
                    OnPropertyChanged(args);
                    Reload();
                }
            }
        }

        IEnumerable<A2019128143.Models.BibliotecaDb.Materiai> _getMateriaisForMaterialIDResult;
        protected IEnumerable<A2019128143.Models.BibliotecaDb.Materiai> getMateriaisForMaterialIDResult
        {
            get
            {
                return _getMateriaisForMaterialIDResult;
            }
            set
            {
                if (!object.Equals(_getMateriaisForMaterialIDResult, value))
                {
                    var args = new PropertyChangedEventArgs(){ Name = "getMateriaisForMaterialIDResult", NewValue = value, OldValue = _getMateriaisForMaterialIDResult };
                    _getMateriaisForMaterialIDResult = value;
                    OnPropertyChanged(args);
                    Reload();
                }
            }
        }

        IEnumerable<A2019128143.Models.BibliotecaDb.Utilizadore> _getUtilizadoresForFuncionarioEmprestimoIDResult;
        protected IEnumerable<A2019128143.Models.BibliotecaDb.Utilizadore> getUtilizadoresForFuncionarioEmprestimoIDResult
        {
            get
            {
                return _getUtilizadoresForFuncionarioEmprestimoIDResult;
            }
            set
            {
                if (!object.Equals(_getUtilizadoresForFuncionarioEmprestimoIDResult, value))
                {
                    var args = new PropertyChangedEventArgs(){ Name = "getUtilizadoresForFuncionarioEmprestimoIDResult", NewValue = value, OldValue = _getUtilizadoresForFuncionarioEmprestimoIDResult };
                    _getUtilizadoresForFuncionarioEmprestimoIDResult = value;
                    OnPropertyChanged(args);
                    Reload();
                }
            }
        }

        IEnumerable<A2019128143.Models.BibliotecaDb.Utilizadore> _getUtilizadoresForFuncionarioDevolucaoIDResult;
        protected IEnumerable<A2019128143.Models.BibliotecaDb.Utilizadore> getUtilizadoresForFuncionarioDevolucaoIDResult
        {
            get
            {
                return _getUtilizadoresForFuncionarioDevolucaoIDResult;
            }
            set
            {
                if (!object.Equals(_getUtilizadoresForFuncionarioDevolucaoIDResult, value))
                {
                    var args = new PropertyChangedEventArgs(){ Name = "getUtilizadoresForFuncionarioDevolucaoIDResult", NewValue = value, OldValue = _getUtilizadoresForFuncionarioDevolucaoIDResult };
                    _getUtilizadoresForFuncionarioDevolucaoIDResult = value;
                    OnPropertyChanged(args);
                    Reload();
                }
            }
        }

        protected override async System.Threading.Tasks.Task OnInitializedAsync()
        {
            await Load();
        }
        protected async System.Threading.Tasks.Task Load()
        {
            var bibliotecaDbGetEmprestimoByEmprestimoIdResult = await BibliotecaDb.GetEmprestimoByEmprestimoId(EmprestimoID);
            emprestimo = bibliotecaDbGetEmprestimoByEmprestimoIdResult;

            var bibliotecaDbGetUtilizadoresResult = await BibliotecaDb.GetUtilizadores();
            getUtilizadoresForUtilizadorIDResult = bibliotecaDbGetUtilizadoresResult;

            var bibliotecaDbGetMateriaisResult = await BibliotecaDb.GetMateriais();
            getMateriaisForMaterialIDResult = bibliotecaDbGetMateriaisResult;

            var bibliotecaDbGetUtilizadoresResult0 = await BibliotecaDb.GetUtilizadores();
            getUtilizadoresForFuncionarioEmprestimoIDResult = bibliotecaDbGetUtilizadoresResult0;

            var bibliotecaDbGetUtilizadoresResult01 = await BibliotecaDb.GetUtilizadores();
            getUtilizadoresForFuncionarioDevolucaoIDResult = bibliotecaDbGetUtilizadoresResult01;
        }

        protected async System.Threading.Tasks.Task Form0Submit(A2019128143.Models.BibliotecaDb.Emprestimo args)
        {
            try
            {
                var bibliotecaDbUpdateEmprestimoResult = await BibliotecaDb.UpdateEmprestimo(EmprestimoID, emprestimo);
                DialogService.Close(emprestimo);
            }
            catch (System.Exception bibliotecaDbUpdateEmprestimoException)
            {
                NotificationService.Notify(new NotificationMessage(){ Severity = NotificationSeverity.Error,Summary = $"Error",Detail = $"Unable to update Emprestimo" });
            }
        }

        protected async System.Threading.Tasks.Task Button2Click(MouseEventArgs args)
        {
            DialogService.Close(null);
        }
    }
}
