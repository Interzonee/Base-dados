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
    public partial class AddMateriaiComponent : ComponentBase
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

        A2019128143.Models.BibliotecaDb.Materiai _materiai;
        protected A2019128143.Models.BibliotecaDb.Materiai materiai
        {
            get
            {
                return _materiai;
            }
            set
            {
                if (!object.Equals(_materiai, value))
                {
                    var args = new PropertyChangedEventArgs(){ Name = "materiai", NewValue = value, OldValue = _materiai };
                    _materiai = value;
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
            materiai = new A2019128143.Models.BibliotecaDb.Materiai(){};
        }

        protected async System.Threading.Tasks.Task Form0Submit(A2019128143.Models.BibliotecaDb.Materiai args)
        {
            try
            {
                var bibliotecaDbCreateMateriaiResult = await BibliotecaDb.CreateMateriai(materiai);
                DialogService.Close(materiai);
            }
            catch (System.Exception bibliotecaDbCreateMateriaiException)
            {
                NotificationService.Notify(new NotificationMessage(){ Severity = NotificationSeverity.Error,Summary = $"Error",Detail = $"Unable to create new Materiai!" });
            }
        }

        protected async System.Threading.Tasks.Task Button2Click(MouseEventArgs args)
        {
            DialogService.Close(null);
        }
    }
}
