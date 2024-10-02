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
    public partial class MateriaisCategoriaComponent : ComponentBase
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
        protected RadzenDataGrid<A2019128143.Models.BibliotecaDb.MateriaisCategoria> grid0;

        string _search;
        protected string search
        {
            get
            {
                return _search;
            }
            set
            {
                if (!object.Equals(_search, value))
                {
                    var args = new PropertyChangedEventArgs(){ Name = "search", NewValue = value, OldValue = _search };
                    _search = value;
                    OnPropertyChanged(args);
                    Reload();
                }
            }
        }

        IEnumerable<A2019128143.Models.BibliotecaDb.MateriaisCategoria> _getMateriaisCategoriaResult;
        protected IEnumerable<A2019128143.Models.BibliotecaDb.MateriaisCategoria> getMateriaisCategoriaResult
        {
            get
            {
                return _getMateriaisCategoriaResult;
            }
            set
            {
                if (!object.Equals(_getMateriaisCategoriaResult, value))
                {
                    var args = new PropertyChangedEventArgs(){ Name = "getMateriaisCategoriaResult", NewValue = value, OldValue = _getMateriaisCategoriaResult };
                    _getMateriaisCategoriaResult = value;
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
            if (string.IsNullOrEmpty(search)) {
                search = "";
            }

            var bibliotecaDbGetMateriaisCategoriaResult = await BibliotecaDb.GetMateriaisCategoria(new Query() { Expand = "Materiai,CategoriasMateriai" });
            getMateriaisCategoriaResult = bibliotecaDbGetMateriaisCategoriaResult;
        }

        protected async System.Threading.Tasks.Task Button0Click(MouseEventArgs args)
        {
            var dialogResult = await DialogService.OpenAsync<AddMateriaisCategoria>("Add Materiais Categoria", null);
            await grid0.Reload();

            await InvokeAsync(() => { StateHasChanged(); });
        }

        protected async System.Threading.Tasks.Task GridDeleteButtonClick(MouseEventArgs args, dynamic data)
        {
            try
            {
                if (await DialogService.Confirm("Are you sure you want to delete this record?") == true)
                {
                    var bibliotecaDbDeleteMateriaisCategoriaResult = await BibliotecaDb.DeleteMateriaisCategoria(data.MaterialID, data.CategoriaID);
                    if (bibliotecaDbDeleteMateriaisCategoriaResult != null)
                    {
                        await grid0.Reload();
                    }
                }
            }
            catch (System.Exception bibliotecaDbDeleteMateriaisCategoriaException)
            {
                NotificationService.Notify(new NotificationMessage(){ Severity = NotificationSeverity.Error,Summary = $"Error",Detail = $"Unable to delete MateriaisCategoria" });
            }
        }
    }
}
