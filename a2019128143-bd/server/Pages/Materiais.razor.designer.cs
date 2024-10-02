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
    public partial class MateriaisComponent : ComponentBase
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
        protected RadzenDataGrid<A2019128143.Models.BibliotecaDb.Materiai> grid0;

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

        IEnumerable<A2019128143.Models.BibliotecaDb.Materiai> _getMateriaisResult;
        protected IEnumerable<A2019128143.Models.BibliotecaDb.Materiai> getMateriaisResult
        {
            get
            {
                return _getMateriaisResult;
            }
            set
            {
                if (!object.Equals(_getMateriaisResult, value))
                {
                    var args = new PropertyChangedEventArgs(){ Name = "getMateriaisResult", NewValue = value, OldValue = _getMateriaisResult };
                    _getMateriaisResult = value;
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

            var bibliotecaDbGetMateriaisResult = await BibliotecaDb.GetMateriais(new Query() { Filter = $@"i => i.Titulo.Contains(@0) || i.Tipo.Contains(@1) || i.Autor.Contains(@2) || i.Editora.Contains(@3) || i.ISBN.Contains(@4) || i.Localizacao.Contains(@5) || i.Descricao.Contains(@6)", FilterParameters = new object[] { search, search, search, search, search, search, search } });
            getMateriaisResult = bibliotecaDbGetMateriaisResult;
        }

        protected async System.Threading.Tasks.Task Button0Click(MouseEventArgs args)
        {
            var dialogResult = await DialogService.OpenAsync<AddMateriai>("Add Materiai", null);
            await grid0.Reload();

            await InvokeAsync(() => { StateHasChanged(); });
        }

        protected async System.Threading.Tasks.Task Grid0RowDoubleClick(DataGridRowMouseEventArgs<A2019128143.Models.BibliotecaDb.Materiai> args)
        {
            var dialogResult = await DialogService.OpenAsync<EditMateriai>("Edit Materiai", new Dictionary<string, object>() { {"MaterialID", args.Data.MaterialID} });
            await InvokeAsync(() => { StateHasChanged(); });
        }

        protected async System.Threading.Tasks.Task GridDeleteButtonClick(MouseEventArgs args, dynamic data)
        {
            try
            {
                if (await DialogService.Confirm("Are you sure you want to delete this record?") == true)
                {
                    var bibliotecaDbDeleteMateriaiResult = await BibliotecaDb.DeleteMateriai(data.MaterialID);
                    if (bibliotecaDbDeleteMateriaiResult != null)
                    {
                        await grid0.Reload();
                    }
                }
            }
            catch (System.Exception bibliotecaDbDeleteMateriaiException)
            {
                NotificationService.Notify(new NotificationMessage(){ Severity = NotificationSeverity.Error,Summary = $"Error",Detail = $"Unable to delete Materiai" });
            }
        }
    }
}
