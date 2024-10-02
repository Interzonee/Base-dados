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
    public partial class AddAvaliacoComponent : ComponentBase
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

        A2019128143.Models.BibliotecaDb.Avaliaco _avaliaco;
        protected A2019128143.Models.BibliotecaDb.Avaliaco avaliaco
        {
            get
            {
                return _avaliaco;
            }
            set
            {
                if (!object.Equals(_avaliaco, value))
                {
                    var args = new PropertyChangedEventArgs(){ Name = "avaliaco", NewValue = value, OldValue = _avaliaco };
                    _avaliaco = value;
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

            var bibliotecaDbGetMateriaisResult = await BibliotecaDb.GetMateriais();
            getMateriaisForMaterialIDResult = bibliotecaDbGetMateriaisResult;

            avaliaco = new A2019128143.Models.BibliotecaDb.Avaliaco(){};
        }

        protected async System.Threading.Tasks.Task Form0Submit(A2019128143.Models.BibliotecaDb.Avaliaco args)
        {
            try
            {
                var bibliotecaDbCreateAvaliacoResult = await BibliotecaDb.CreateAvaliaco(avaliaco);
                DialogService.Close(avaliaco);
            }
            catch (System.Exception bibliotecaDbCreateAvaliacoException)
            {
                NotificationService.Notify(new NotificationMessage(){ Severity = NotificationSeverity.Error,Summary = $"Error",Detail = $"Unable to create new Avaliaco!" });
            }
        }

        protected async System.Threading.Tasks.Task Button2Click(MouseEventArgs args)
        {
            DialogService.Close(null);
        }
    }
}
