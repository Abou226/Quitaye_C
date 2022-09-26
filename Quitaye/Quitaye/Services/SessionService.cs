using Models;
using Services;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Essentials;
using Xamarin.Forms;

[assembly: Dependency(typeof(DataService<Secrets>))]
[assembly: Dependency(typeof(InitialService))]
namespace Quitaye.Services
{
    public class SessionService : ISessionService
    {
        public IDataService<Secrets> SecretService { get; }
        public IInitialService Initial { get; }
        public bool TokenExpired { get; set; }
        public DateTime TokenExpiry { get; set; }
        public string Token { get; set; }

        public SessionService()
        {
            SecretService = DependencyService.Get<IDataService<Secrets>>();
            Initial = DependencyService.Get<IInitialService>();
        }

        public async Task<string> GetLastEntrepriseId()
        {

            if (await IsSecureStorageCompatible())
            {
                return await SecureStorage.GetAsync("LastEntrepriseId");
            }
            else return Preferences.Get("LastEntrepriseId", "");
        }

        public async Task GetNewToken(string oldToken)
        {
            if (!string.IsNullOrWhiteSpace(oldToken))
            {
                if(await IsSecureStorageCompatible())
                {
                    var resul = await Initial.Get(new LogInModel() { Token = await SecureStorage.GetAsync("Token") });
                    if (resul != null)
                    {
                        await SaveToken(resul);
                    }
                }else
                {
                    var resul = await Initial.Get(new LogInModel() { Token = Preferences.Get("Token", "") });
                    if (resul != null)
                    {
                        await SaveToken(resul);
                    }
                }
            }
        }

        private async Task SaveToken(Secrets resul)
        {
            if (await IsSecureStorageCompatible())
            {
                await SecureStorage.SetAsync("Token", resul.Token);
                await SecureStorage.SetAsync("Email", resul.Email);
                await SecureStorage.SetAsync("Prenom", resul.Prenom);
                await SecureStorage.SetAsync("TokenExpiry", resul.TokenExpiry.ToString());
                await SecureStorage.SetAsync("Nom", resul.Nom);
                await SecureStorage.SetAsync("ProfilePic", resul.ProfilePic);
            }
            else
            {
                Preferences.Set("Token", resul.Token);
                Preferences.Set("Email", resul.Email);
                Preferences.Set("Prenom", resul.Prenom);
                Preferences.Set("TokenExpiry", resul.TokenExpiry.ToString());
                Preferences.Set("Nom", resul.Nom);
                Preferences.Set("ProfilePic", resul.ProfilePic);
            }
        }

        public async Task<Secrets> GetNewTokenByEmail(string email)
        {
            if (!string.IsNullOrWhiteSpace(email))
            {
                var secret = await SecretService.GetItemAsync($"auth/useremail/{email}");
                if (secret != null)
                {
                    await SaveToken(secret);
                    return secret;
                }
                else return null;
            }
            else return null;
        }

        public async Task<string> GetSavedEmail()
        {
            if (await IsSecureStorageCompatible())
            {
                return await SecureStorage.GetAsync("Email");
            }
            else
            {
                return Preferences.Get("Email", "");
            }
        }

        public async Task<string> GetSavedFamilyName()
        {
            if (await IsSecureStorageCompatible())
            {
                return await SecureStorage.GetAsync("Nom");
            }
            else
            {
                return Preferences.Get("Nom", "");
            }
        }

        public async Task<string> GetSavedProfilePic()
        {
            if (await IsSecureStorageCompatible())
            {
                return await SecureStorage.GetAsync("ProfilePic");
            }
            else
            {
                return Preferences.Get("ProfilePic", "");
            }
        }

        public async Task<string> GetSavedSurName()
        {
            if (await IsSecureStorageCompatible())
            {
                return await SecureStorage.GetAsync("Prenom");
            }
            else
            {
                return Preferences.Get("Prenom", "");
            }
        }

        public async Task<string> GetToken()
        {
            if (await IsSecureStorageCompatible())
            {
                return Token = await SecureStorage.GetAsync("Token");
            }else
            {
                return Token = Preferences.Get("Token", "");
            }
        }

        public async Task<bool> HasTokenExpired()
        {
            try
            {
                if (await IsSecureStorageCompatible())
                {
                    if (!string.IsNullOrWhiteSpace(await SecureStorage.GetAsync("TokenExpiry")))
                    {
                        DateTime date = Convert.ToDateTime(await SecureStorage.GetAsync("TokenExpiry"));
                        if (DateTime.Now >= date.AddMinutes(-10))
                            return true;
                        else return false;
                    }
                    else return true;
                }
                else
                {
                    if (!string.IsNullOrWhiteSpace(Preferences.Get("TokenExpiry", "")))
                    {
                        DateTime date = Convert.ToDateTime(Preferences.Get("TokenExpiry", ""));
                        if (DateTime.Now >= date.AddMinutes(-10))
                            return true;
                        else return false;
                    }
                    else return true;
                }
            }
            catch (Exception)
            {
                return true;
            }
        }

        public async Task<bool> IsSecureStorageCompatible()
        {
            try
            {
                await SecureStorage.SetAsync("test", "test");
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public async Task SaveLastEntreprise(string id)
        {
            try
            {
                if (await IsSecureStorageCompatible())
                {
                    await SecureStorage.SetAsync("LastEntrepriseId", id);
                }
                else Preferences.Set("LastEntrepriseId", id);
            }
            catch (Exception)
            {

            }
        }
    }
}
