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
    public partial class VwMateriaisDisponiveisComponent : ComponentBase
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
        protected RadzenDataGrid<A2019128143.Models.BibliotecaDb.VwMateriaisDisponivei> grid0;

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

        IEnumerable<A2019128143.Models.BibliotecaDb.VwMateriaisDisponivei> _getVwMateriaisDisponiveisResult;
        protected IEnumerable<A2019128143.Models.BibliotecaDb.VwMateriaisDisponivei> getVwMateriaisDisponiveisResult
        {
            get
            {
                return _getVwMateriaisDisponiveisResult;
            }
            set
            {
                if (!object.Equals(_getVwMateriaisDisponiveisResult, value))
                {
                    var args = new PropertyChangedEventArgs(){ Name = "getVwMateriaisDisponiveisResult", NewValue = value, OldValue = _getVwMateriaisDisponiveisResult };
                    _getVwMateriaisDisponiveisResult = value;
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

            var bibliotecaDbGetVwMateriaisDisponiveisResult = await BibliotecaDb.GetVwMateriaisDisponiveis(new Query() { Filter = $@"i => i.Titulo.Contains(@0) || i.Tipo.Contains(@1) || i.Autor.Contains(@2) || i.Editora.Contains(@3) || i.ISBN.Contains(@4) || i.Localizacao.Contains(@5)", FilterParameters = new object[] { search, search, search, search, search, search } });
            getVwMateriaisDisponiveisResult = bibliotecaDbGetVwMateriaisDisponiveisResult;
        }
    }
}
