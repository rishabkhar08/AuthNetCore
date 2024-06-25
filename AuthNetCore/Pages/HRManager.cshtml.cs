using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace AuthNetCore.Pages
{
    [Authorize(Policy = "HRManager_Policy")]
    public class HRManagerModel : PageModel
    {
        public void OnGet()
        {
        }
    }
}
