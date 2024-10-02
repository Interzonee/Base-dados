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
    public partial class EditNotificacoComponent : ComponentBase
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
        public dynamic NotificacaoID { get; set; }

        A2019128143.Models.BibliotecaDb.Notificaco _notificaco;
        protected A2019128143.Models.BibliotecaDb.Notificaco notificaco
        {
            get
            {
                return _notificaco;
            }
            set
            {
                if (!object.Equals(_notificaco, value))
                {
                    var args = new PropertyChangedEventArgs(){ Name = "notificaco", NewValue = value, OldValue = _notificaco };
                    _notificaco = value;
                    OnPropertyChanged(args);
                    Reload();
                }
            }
        }

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

        protected override async System.Threading.Tasks.Task OnInitializedAsync()
        {
            await Load();
        }
        protected async System.Threading.Tasks.Task Load()
        {
            var bibliotecaDbGetNotificacoByNotificacaoIdResult = await BibliotecaDb.GetNotificacoByNotificacaoId(NotificacaoID);
            notificaco = bibliotecaDbGetNotificacoByNotificacaoIdResult;

            var bibliotecaDbGetUtilizadoresResult = await BibliotecaDb.GetUtilizadores();
            getUtilizadoresForUtilizadorIDResult = bibliotecaDbGetUtilizadoresResult;
        }

        protected async System.Threading.Tasks.Task Form0Submit(A2019128143.Models.BibliotecaDb.Notificaco args)
        {
            try
            {
                var bibliotecaDbUpdateNotificacoResult = await BibliotecaDb.UpdateNotificaco(NotificacaoID, notificaco);
                DialogService.Close(notificaco);
            }
            catch (System.Exception bibliotecaDbUpdateNotificacoException)
            {
                NotificationService.Notify(new NotificationMessage(){ Severity = NotificationSeverity.Error,Summary = $"Error",Detail = $"Unable to update Notificaco" });
            }
        }

        protected async System.Threading.Tasks.Task Button2Click(MouseEventArgs args)
        {
            DialogService.Close(null);
        }
    }
}
