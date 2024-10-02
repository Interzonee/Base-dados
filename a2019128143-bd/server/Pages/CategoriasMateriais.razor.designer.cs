﻿using System;
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
    public partial class CategoriasMateriaisComponent : ComponentBase
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
        protected RadzenDataGrid<A2019128143.Models.BibliotecaDb.CategoriasMateriai> grid0;

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

        IEnumerable<A2019128143.Models.BibliotecaDb.CategoriasMateriai> _getCategoriasMateriaisResult;
        protected IEnumerable<A2019128143.Models.BibliotecaDb.CategoriasMateriai> getCategoriasMateriaisResult
        {
            get
            {
                return _getCategoriasMateriaisResult;
            }
            set
            {
                if (!object.Equals(_getCategoriasMateriaisResult, value))
                {
                    var args = new PropertyChangedEventArgs(){ Name = "getCategoriasMateriaisResult", NewValue = value, OldValue = _getCategoriasMateriaisResult };
                    _getCategoriasMateriaisResult = value;
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

            var bibliotecaDbGetCategoriasMateriaisResult = await BibliotecaDb.GetCategoriasMateriais(new Query() { Filter = $@"i => i.Nome.Contains(@0) || i.Descricao.Contains(@1)", FilterParameters = new object[] { search, search } });
            getCategoriasMateriaisResult = bibliotecaDbGetCategoriasMateriaisResult;
        }

        protected async System.Threading.Tasks.Task Button0Click(MouseEventArgs args)
        {
            var dialogResult = await DialogService.OpenAsync<AddCategoriasMateriai>("Add Categorias Materiai", null);
            await grid0.Reload();

            await InvokeAsync(() => { StateHasChanged(); });
        }

        protected async System.Threading.Tasks.Task Grid0RowDoubleClick(DataGridRowMouseEventArgs<A2019128143.Models.BibliotecaDb.CategoriasMateriai> args)
        {
            var dialogResult = await DialogService.OpenAsync<EditCategoriasMateriai>("Edit Categorias Materiai", new Dictionary<string, object>() { {"CategoriaID", args.Data.CategoriaID} });
            await InvokeAsync(() => { StateHasChanged(); });
        }

        protected async System.Threading.Tasks.Task GridDeleteButtonClick(MouseEventArgs args, dynamic data)
        {
            try
            {
                if (await DialogService.Confirm("Are you sure you want to delete this record?") == true)
                {
                    var bibliotecaDbDeleteCategoriasMateriaiResult = await BibliotecaDb.DeleteCategoriasMateriai(data.CategoriaID);
                    if (bibliotecaDbDeleteCategoriasMateriaiResult != null)
                    {
                        await grid0.Reload();
                    }
                }
            }
            catch (System.Exception bibliotecaDbDeleteCategoriasMateriaiException)
            {
                NotificationService.Notify(new NotificationMessage(){ Severity = NotificationSeverity.Error,Summary = $"Error",Detail = $"Unable to delete CategoriasMateriai" });
            }
        }
    }
}
