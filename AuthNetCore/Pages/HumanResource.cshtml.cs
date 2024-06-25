using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace AuthNetCore.Pages
{
    [Authorize(Policy = "HRDepartment_Policy")]
    public class HumanResourceModel : PageModel
    {
        public void OnGet()
        {
        }
    }
}
