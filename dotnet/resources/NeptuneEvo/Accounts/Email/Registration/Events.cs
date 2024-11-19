using System.Threading.Tasks;
using GTANetworkAPI;
using NeptuneEvo.Accounts.Registration.Models;
using NeptuneEvo.Handles;
using NeptuneEvo.Players;
using Redage.SDK;

namespace NeptuneEvo.Accounts.Email.Registration
{
    public class Events : Script
    {
        private static readonly nLog Log = new nLog("NeptuneEvo.Accounts.Email.Registration");
        
        [RemoteEvent("signup")]
        public void ClientEvent_signup(ExtPlayer player, string login_, string pass_, string email_, string promo_)
        {            
            if (Players.Queue.Repository.List.Contains(player))
                return;
            
            Trigger.SetTask(async () =>
            {
                if (!Main.ServerSettings.IsEmailConfirmed)
                {
                    var sessionData = player.GetSessionData();
                    if (sessionData == null) return;
                    
                    var result = await Accounts.Registration.Repository.Register(player, login_, pass_, email_, promo_, "");
                        
                    if (result == RegistrationEnum.Error) Accounts.Registration.Repository.MessageError(player,  "An unforeseen mistake!");
                    else if (result == RegistrationEnum.LoadingError) Accounts.Registration.Repository.MessageError(player,  "Wait a few seconds and try again ...");
                    else if (result == RegistrationEnum.SocialReg) Accounts.Registration.Repository.MessageError(player,  "There is already a game account registered to this SocialClub!");
                    else if (result == RegistrationEnum.UserReg) Accounts.Registration.Repository.MessageError(player,  "This username is already taken!");
                    else if (result == RegistrationEnum.EmailReg) Accounts.Registration.Repository.MessageError(player,  "This email is already taken!");
                    else if (result == RegistrationEnum.DataError) Accounts.Registration.Repository.MessageError(player,  "Error in filling in the fields!");
                    else if (result == RegistrationEnum.PromoError) Accounts.Registration.Repository.MessageError(player,  "This promo code does not exist at the moment, enter the correct one or clear the field!");
                    else if (result == RegistrationEnum.ReffError) Accounts.Registration.Repository.MessageError(player,  "We see that you have entered a friend's ref.code instead of a streamer's promo code, so we ask you to leave the promo code field blank now, and after creating a character, find the right menu in your phone.");
                    else if (result == RegistrationEnum.PromoLimitError) Accounts.Registration.Repository.MessageError(player,  "We're sorry, but the promo code has exceeded the activation limit, enter another one!");
                    else if (result == RegistrationEnum.ABError) Accounts.Registration.Repository.MessageError(player,  "Registration error, use your main SocialClub to enter the game.");
                    Log.Write($"{sessionData.Name} ({sessionData.SocialClubName} | {sessionData.RealSocialClub}) tryed to signup.");
                }
                else
                {
                    var result = await Repository.Verification(player, login_, pass_, email_, promo_);
                
                    if (result == RegistrationEnum.Error) Accounts.Registration.Repository.MessageError(player, "An unforeseen mistake!");
                    else if (result == RegistrationEnum.LoadingError) Accounts.Registration.Repository.MessageError(player, "Wait a few seconds and try again...");
                    else if (result == RegistrationEnum.SocialReg) Accounts.Registration.Repository.MessageError(player, "There is already a game account registered to this SocialClub!");
                    else if (result == RegistrationEnum.UserReg) Accounts.Registration.Repository.MessageError(player, "This username is already taken!");
                    else if (result == RegistrationEnum.EmailReg) Accounts.Registration.Repository.MessageError(player, "This email is already taken!");
                    else if (result == RegistrationEnum.DataError) Accounts.Registration.Repository.MessageError(player, "Error in filling in the fields!");
                    else if (result == RegistrationEnum.PromoError) Accounts.Registration.Repository.MessageError(player, "This promo code does not exist at the moment, enter the correct one or clear the field!");
                    else if (result == RegistrationEnum.ReffError) Accounts.Registration.Repository.MessageError(player, "We see that you have entered a friend's ref.code instead of a streamer's promo code, so we ask you to leave the promo code field nowYes blank, and after creating a character, find the right menu on the phone.");
                    else if (result == RegistrationEnum.PromoLimitError) Accounts.Registration.Repository.MessageError(player, "We're sorry, but the promo code has exceeded the activation limit, enter another one!");
                    else if (result == RegistrationEnum.ABError) Accounts.Registration.Repository.MessageError(player, "Registration error, use your primary SocialClub to log in to the game.");
                }
            });
        }
    }
}