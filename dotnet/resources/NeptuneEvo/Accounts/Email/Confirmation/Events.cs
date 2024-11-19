using System.Text.RegularExpressions;
using System.Threading.Tasks;
using GTANetworkAPI;
using NeptuneEvo.Accounts.Email.Confirmation.Models;
using NeptuneEvo.Handles;
using Redage.SDK;

namespace NeptuneEvo.Accounts.Email.Confirmation
{
    public class Events : Script
    {

        [Command("emailconfirm")]
        public void emailconfirm(ExtPlayer player, string email)
        {        
            EmailConfirm(player, email);
        }

        [RemoteEvent("server.email.confirm")]
        public void EmailConfirm(ExtPlayer player, string email)
        {                
            var accountData = player.GetAccountData();
            if (accountData == null) 
                return;
            
            var rg = new Regex(@"[0-9]{8,11}[.][0-9]{8,11}", RegexOptions.IgnoreCase);
            
            if (rg.IsMatch(accountData.Ga))
            {
                Notify.Send(player, NotifyType.Error, NotifyPosition.BottomCenter, "You have already confirmed the mail!", 3000);
                return;
            }

            Trigger.SetTask(async () =>
            {
                var result = await Repository.Confirm(player, email);

                if (result == EmailConfirmEnum.Error)
                    Notify.Send(player, NotifyType.Error, NotifyPosition.BottomCenter, "An unforeseen mistake!", 5000);
                else if (result == EmailConfirmEnum.LoadingError)
                    Notify.Send(player, NotifyType.Error, NotifyPosition.BottomCenter,
                        "Wait a few seconds and try again ...", 5000);
                else if (result == EmailConfirmEnum.EmailReg)
                    Notify.Send(player, NotifyType.Error, NotifyPosition.BottomCenter, "This email is already busy!", 5000);
                else if (result == EmailConfirmEnum.DataError)
                    Notify.Send(player, NotifyType.Error, NotifyPosition.BottomCenter, "A mistake to fill the fields!",
                        5000);
                else if (result == EmailConfirmEnum.Success)
                    Notify.Send(player, NotifyType.Info, NotifyPosition.BottomCenter,
                        "A letter was sent to the mail with reference to confirm the account, which will be valid for 15 minutes.",
                        5000);
            });
        }
    }
}