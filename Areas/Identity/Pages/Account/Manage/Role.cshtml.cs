using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;
using System.Text.Encodings.Web;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.WebUtilities;
using Microsoft.Extensions.Logging;
using Microsoft.AspNetCore.Identity.UI.V4.Pages.Account.Internal;

namespace LibApp.Areas.Identity.Pages.Account.Manage
{



    public partial class RoleModel : PageModel
    {
        private readonly UserManager<IdentityUser> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly SignInManager<IdentityUser> _signInManager;
        ILogger<LogoutModel> _logger;

        public RoleModel(UserManager<IdentityUser> userManager, RoleManager<IdentityRole> roleManager, SignInManager<IdentityUser> signInManager, ILogger<LogoutModel> logger)
        {
            _userManager = userManager;
            _roleManager = roleManager;
            _signInManager = signInManager;
            _logger = logger;
        }
        [Display(Name = "Select Role")]
        public string[] Roles { get; set; }
        public string CurrentRole { get; set; }
        public string Username { get; set; }

        public string Email { get; set; }

        public bool IsEmailConfirmed { get; set; }

        [TempData]
        public string StatusMessage { get; set; }

        [BindProperty]
        public InputModel Input { get; set; }

        public class InputModel
        {
            [Required]
            [Display(Name = "User Role")]
            public List<string> newRoles { get; set; }
        }

        private async Task LoadAsync(IdentityUser user)
        {
            var currentRole = await _userManager.GetRolesAsync(user);
            CurrentRole = currentRole[0];

            var roles = _roleManager.Roles.ToList();
            List<string> rolesToList = new List<string>();

            foreach (IdentityRole role in roles)
            {
                if (role.Name != "Admin" && role.Name != "Member")
                {
                    rolesToList.Add(role.Name);
                }
            }
            Roles = rolesToList.ToArray();

            Input = new InputModel
            {
                newRoles = rolesToList
            };
        }

        //CHANGE USER ROLE
        public async Task<IActionResult> OnPostChangeUserRoleAsync()
        {
            if (!String.IsNullOrEmpty(Input.newRoles[0]))
            {
                int newRoleIndex = int.Parse(Input.newRoles[0]);
                var roles = _roleManager.Roles.ToList();
                List<string> selectableRoles = new List<string>();
                foreach (IdentityRole role in roles)
                {
                    if (role.Name != "Admin" && role.Name != "Member")
                    {
                        selectableRoles.Add(role.Name);
                    }
                }
                string newRole = selectableRoles[newRoleIndex];

                var user = await _userManager.GetUserAsync(User);
                var currentRole = await _userManager.GetRolesAsync(user);

                await _userManager.AddToRoleAsync(user, newRole);
                if (currentRole != null)
                {
                    var roleToString = currentRole;
                    await _userManager.RemoveFromRoleAsync(user, roleToString[0]);
                }

                await LoadAsync(user);
                //return Page();
                //logout
                await _signInManager.SignOutAsync();
                _logger.LogInformation("User logged out.");
                return RedirectToPage();
            }
            return Page();
        }

        //IsEmailConfirmed = await _userManager.IsEmailConfirmedAsync(user);

        public async Task<IActionResult> OnGetAsync()
        {
            var user = await _userManager.GetUserAsync(User);
            if (user == null)
            {
                return NotFound($"Unable to load user with ID '{_userManager.GetUserId(User)}'.");
            }

            await LoadAsync(user);
            return Page();
        }
    }
}
