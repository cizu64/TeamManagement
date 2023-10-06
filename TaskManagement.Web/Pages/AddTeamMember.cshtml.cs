using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.ComponentModel.DataAnnotations;
using System.Net;
using TaskManagement.Web.Attributes;
using TaskManagement.Web.Common;
using TaskManagement.Web.Services;

namespace TaskManagement.Web.Pages
{
    [TokenAuthorize(Role: "TeamLead")]
    public class AddTeamMemberModel : PageModel
    {
        private readonly TeamLead teamLead;
        private readonly Country country;

        public AddTeamMemberModel(TeamLead teamLead, Country country)
        {
            this.teamLead = teamLead;
            this.country = country;
        }

        public async Task<IActionResult> OnGet()
        {
            var countries = await country.Countries();
            ViewData["countries"] = countries.detail as VM.Countries[];
            return Page();
        }

        [BindProperty]
        public CreateTeamMemberDTO CreateTeamMemberDTO { get; set; }
        public async Task<IActionResult> OnPost()
        {
            var countries = await country.Countries();
            ViewData["countries"] = countries.detail as VM.Countries[];
            if (!ModelState.IsValid)
            {
                return Page();
            }
            string token = Request.Cookies["token"].ToString();
            APIResult teamMember = await teamLead.AddTeamMember(token, CreateTeamMemberDTO.FirstName, CreateTeamMemberDTO.LastName,CreateTeamMemberDTO.CountryId, CreateTeamMemberDTO.Email, CreateTeamMemberDTO.Password);
            if (teamMember.statusCode != (int)HttpStatusCode.OK)
            {
                ViewData["err"] = teamMember.detail; //display the detail of the error
                return Page();
            }
            ViewData["suc"] = teamMember.detail;
            return Page();
        }
    }

    public record CreateTeamMemberDTO
    {
        [Required]
        public required string LastName { get; set; }
        [Required]
        public required int CountryId { get; set; }
       
        [Required]
        public required string Email { get; set; }
        [Required]
        public required string FirstName { get; set; }
        [Required]
        public required string Password { get; set; }
    }
}