using Microsoft.AspNetCore.DataProtection;
using Microsoft.EntityFrameworkCore.Storage;

namespace TaskManagement.WebAPI.Security
{
    public class DataProtector
    {
        IDataProtector _protector;
        public DataProtector(IDataProtectionProvider provider)
        {
            _protector = provider.CreateProtector("ProtectCritical");
        }
        public string Protect(string plainText)
        {
            string protect = _protector.Protect(plainText);
            return protect;
        }

        public string UnProtect(string protectedText)
        {
            string unprotect = _protector.Unprotect(protectedText);
            return unprotect;
        }
    }
}
