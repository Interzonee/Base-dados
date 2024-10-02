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
    public partial class AddMateriaisCategoriaComponent : ComponentBase
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

        IEnumerable<A2019128143.Models.BibliotecaDb.CategoriasMateriai> _getCategoriasMateriaisForCategoriaIDResult;
        protected IEnumerable<A2019128143.Models.BibliotecaDb.CategoriasMateriai> getCategoriasMateriaisForCategoriaIDResult
        {
            get
            {
                return _getCategoriasMateriaisForCategoriaIDResult;
            }
            set
            {
                if (!object.Equals(_getCategoriasMateriaisForCategoriaIDResult, value))
                {
                    var args = new PropertyChangedEventArgs(){ Name = "getCategoriasMateriaisForCategoriaIDResult", NewValue = value, OldValue = _getCategoriasMateriaisForCategoriaIDResult };
                    _getCategoriasMateriaisForCategoriaIDResult = value;
                    OnPropertyChanged(args);
                    Reload();
                }
            }
        }

        A2019128143.Models.BibliotecaDb.MateriaisCategoria _materiaiscategoria;
        protected A2019128143.Models.BibliotecaDb.MateriaisCategoria materiaiscategoria
        {
            get
            {
                return _materiaiscategoria;
            }
            set
            {
                if (!object.Equals(_materiaiscategoria, value))
                {
                    var args = new PropertyChangedEventArgs(){ Name = "materiaiscategoria", NewValue = value, OldValue = _materiaiscategoria };
                    _materiaiscategoria = value;
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
            var bibliotecaDbGetMateriaisResult = await BibliotecaDb.GetMateriais();
            getMateriaisForMaterialIDResult = bibliotecaDbGetMateriaisResult;

            var bibliotecaDbGetCategoriasMateriaisResult = await BibliotecaDb.GetCategoriasMateriais();
            getCategoriasMateriaisForCategoriaIDResult = bibliotecaDbGetCategoriasMateriaisResult;

            materiaiscategoria = new A2019128143.Models.BibliotecaDb.MateriaisCategoria(){};
        }

        protected async System.Threading.Tasks.Task Form0Submit(A2019128143.Models.BibliotecaDb.MateriaisCategoria args)
        {
            try
            {
                var bibliotecaDbCreateMateriaisCategoriaResult = await BibliotecaDb.CreateMateriaisCategoria(materiaiscategoria);
                DialogService.Close(materiaiscategoria);
            }
            catch (System.Exception bibliotecaDbCreateMateriaisCategoriaException)
            {
                NotificationService.Notify(new NotificationMessage(){ Severity = NotificationSeverity.Error,Summary = $"Error",Detail = $"Unable to create new MateriaisCategoria!" });
            }
        }

        protected async System.Threading.Tasks.Task Button2Click(MouseEventArgs args)
        {
            DialogService.Close(null);
        }
    }
}
