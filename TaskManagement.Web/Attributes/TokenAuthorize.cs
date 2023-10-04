using Microsoft.AspNetCore.Mvc;
using TaskManagement.Web.Filters;

namespace TaskManagement.Web.Attributes
{
    public class TokenAuthorize : ServiceFilterAttribute
    {
        public TokenAuthorize(string Role="") : base(typeof(TokenAuth))
        {
            TokenAuth.Role = Role;
        }
    }
}
