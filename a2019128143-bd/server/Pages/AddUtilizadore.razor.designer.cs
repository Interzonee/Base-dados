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
    public partial class AddUtilizadoreComponent : ComponentBase
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

        A2019128143.Models.BibliotecaDb.Utilizadore _utilizadore;
        protected A2019128143.Models.BibliotecaDb.Utilizadore utilizadore
        {
            get
            {
                return _utilizadore;
            }
            set
            {
                if (!object.Equals(_utilizadore, value))
                {
                    var args = new PropertyChangedEventArgs(){ Name = "utilizadore", NewValue = value, OldValue = _utilizadore };
                    _utilizadore = value;
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
            utilizadore = new A2019128143.Models.BibliotecaDb.Utilizadore(){};
        }

        protected async System.Threading.Tasks.Task Form0Submit(A2019128143.Models.BibliotecaDb.Utilizadore args)
        {
            try
            {
                var bibliotecaDbCreateUtilizadoreResult = await BibliotecaDb.CreateUtilizadore(utilizadore);
                DialogService.Close(utilizadore);
            }
            catch (System.Exception bibliotecaDbCreateUtilizadoreException)
            {
                NotificationService.Notify(new NotificationMessage(){ Severity = NotificationSeverity.Error,Summary = $"Error",Detail = $"Unable to create new Utilizadore!" });
            }
        }

        protected async System.Threading.Tasks.Task Button2Click(MouseEventArgs args)
        {
            DialogService.Close(null);
        }
    }
}
