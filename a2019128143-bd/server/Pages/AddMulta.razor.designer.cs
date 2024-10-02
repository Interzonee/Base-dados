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
    public partial class AddMultaComponent : ComponentBase
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

        IEnumerable<A2019128143.Models.BibliotecaDb.Emprestimo> _getEmprestimosForEmprestimoIDResult;
        protected IEnumerable<A2019128143.Models.BibliotecaDb.Emprestimo> getEmprestimosForEmprestimoIDResult
        {
            get
            {
                return _getEmprestimosForEmprestimoIDResult;
            }
            set
            {
                if (!object.Equals(_getEmprestimosForEmprestimoIDResult, value))
                {
                    var args = new PropertyChangedEventArgs(){ Name = "getEmprestimosForEmprestimoIDResult", NewValue = value, OldValue = _getEmprestimosForEmprestimoIDResult };
                    _getEmprestimosForEmprestimoIDResult = value;
                    OnPropertyChanged(args);
                    Reload();
                }
            }
        }

        A2019128143.Models.BibliotecaDb.Multa _multa;
        protected A2019128143.Models.BibliotecaDb.Multa multa
        {
            get
            {
                return _multa;
            }
            set
            {
                if (!object.Equals(_multa, value))
                {
                    var args = new PropertyChangedEventArgs(){ Name = "multa", NewValue = value, OldValue = _multa };
                    _multa = value;
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
            var bibliotecaDbGetEmprestimosResult = await BibliotecaDb.GetEmprestimos();
            getEmprestimosForEmprestimoIDResult = bibliotecaDbGetEmprestimosResult;

            multa = new A2019128143.Models.BibliotecaDb.Multa(){};
        }

        protected async System.Threading.Tasks.Task Form0Submit(A2019128143.Models.BibliotecaDb.Multa args)
        {
            try
            {
                var bibliotecaDbCreateMultaResult = await BibliotecaDb.CreateMulta(multa);
                DialogService.Close(multa);
            }
            catch (System.Exception bibliotecaDbCreateMultaException)
            {
                NotificationService.Notify(new NotificationMessage(){ Severity = NotificationSeverity.Error,Summary = $"Error",Detail = $"Unable to create new Multa!" });
            }
        }

        protected async System.Threading.Tasks.Task Button2Click(MouseEventArgs args)
        {
            DialogService.Close(null);
        }
    }
}
