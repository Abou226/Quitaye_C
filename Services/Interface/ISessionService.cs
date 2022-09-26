using BaseVM;
using Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Services
{
    public interface IBaseSessionService
    {
        bool TokenExpired { get; set; }

        void SetTheme();
        DateTime TokenExpiry { get; set; }
        Entreprise Entreprise { get; set; }
        string AddImageIcon { get; set; }
        string ShareIcon { get; set; }
        string AddIcon { get; set; }
        string BackIcon { get; set; }
        string SettingsIcon { get; set; }
        string UsersIcon { get; set; }
        string NotificationIcon { get; set; }
        Task ResetTokens(Exception ex, Task task);
        Task<IBaseViewModel> CheckConnection();
        Task<Entreprise> GetEntreprise(string id);
        Task GetNewToken(string oldToken);
        Task SaveSource(string source);
        Task SaveLastEntreprise(string id);
        Task SaveEntrepriseType(string type);
        Task SaveKey(string key, string value);
        Task<string> GetToken();
        Task<string> GetKey(string key);
        Task<Secrets> RefreshToken(LogInModel token);
        Task<string> GetLastEntreprise();
        Task<string> GetSavedEmail();
        Task<string> GetSource();
        Task SaveToken(Secrets resul);
        Task<string> GetSavedProfilePic();
        Task<string> GetSavedSurName();
        Task<string> GetSavedFamilyName();
        Task<Secrets> GetNewTokenByEmail(string email);
        Task<bool> IsSecureStorageCompatible();
        Task<string> GetEntrepriseType();
        string Token { get; set; }
        Task<bool> HasTokenExpired();
    }
}
