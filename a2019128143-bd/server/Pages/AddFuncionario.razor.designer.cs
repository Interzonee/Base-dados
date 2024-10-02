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
    public partial class AddFuncionarioComponent : ComponentBase
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

        A2019128143.Models.BibliotecaDb.Funcionario _funcionario;
        protected A2019128143.Models.BibliotecaDb.Funcionario funcionario
        {
            get
            {
                return _funcionario;
            }
            set
            {
                if (!object.Equals(_funcionario, value))
                {
                    var args = new PropertyChangedEventArgs(){ Name = "funcionario", NewValue = value, OldValue = _funcionario };
                    _funcionario = value;
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
            var bibliotecaDbGetUtilizadoresResult = await BibliotecaDb.GetUtilizadores();
            getUtilizadoresForUtilizadorIDResult = bibliotecaDbGetUtilizadoresResult;

            funcionario = new A2019128143.Models.BibliotecaDb.Funcionario(){};
        }

        protected async System.Threading.Tasks.Task Form0Submit(A2019128143.Models.BibliotecaDb.Funcionario args)
        {
            try
            {
                var bibliotecaDbCreateFuncionarioResult = await BibliotecaDb.CreateFuncionario(funcionario);
                DialogService.Close(funcionario);
            }
            catch (System.Exception bibliotecaDbCreateFuncionarioException)
            {
                NotificationService.Notify(new NotificationMessage(){ Severity = NotificationSeverity.Error,Summary = $"Error",Detail = $"Unable to create new Funcionario!" });
            }
        }

        protected async System.Threading.Tasks.Task Button2Click(MouseEventArgs args)
        {
            DialogService.Close(null);
        }
    }
}
