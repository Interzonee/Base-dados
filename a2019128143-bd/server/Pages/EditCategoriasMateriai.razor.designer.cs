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
    public partial class EditCategoriasMateriaiComponent : ComponentBase
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
        public dynamic CategoriaID { get; set; }

        A2019128143.Models.BibliotecaDb.CategoriasMateriai _categoriasmateriai;
        protected A2019128143.Models.BibliotecaDb.CategoriasMateriai categoriasmateriai
        {
            get
            {
                return _categoriasmateriai;
            }
            set
            {
                if (!object.Equals(_categoriasmateriai, value))
                {
                    var args = new PropertyChangedEventArgs(){ Name = "categoriasmateriai", NewValue = value, OldValue = _categoriasmateriai };
                    _categoriasmateriai = value;
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
            var bibliotecaDbGetCategoriasMateriaiByCategoriaIdResult = await BibliotecaDb.GetCategoriasMateriaiByCategoriaId(CategoriaID);
            categoriasmateriai = bibliotecaDbGetCategoriasMateriaiByCategoriaIdResult;
        }

        protected async System.Threading.Tasks.Task Form0Submit(A2019128143.Models.BibliotecaDb.CategoriasMateriai args)
        {
            try
            {
                var bibliotecaDbUpdateCategoriasMateriaiResult = await BibliotecaDb.UpdateCategoriasMateriai(CategoriaID, categoriasmateriai);
                DialogService.Close(categoriasmateriai);
            }
            catch (System.Exception bibliotecaDbUpdateCategoriasMateriaiException)
            {
                NotificationService.Notify(new NotificationMessage(){ Severity = NotificationSeverity.Error,Summary = $"Error",Detail = $"Unable to update CategoriasMateriai" });
            }
        }

        protected async System.Threading.Tasks.Task Button2Click(MouseEventArgs args)
        {
            DialogService.Close(null);
        }
    }
}
