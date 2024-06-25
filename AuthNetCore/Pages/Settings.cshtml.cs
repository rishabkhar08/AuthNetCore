using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace AuthNetCore.Pages
{
    [Authorize(Policy = "Admin_Policy")]
    public class SettingsModel : PageModel
    {
        public void OnGet()
        {
        }
    }
}
