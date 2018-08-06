using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using IntervueApp.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.WindowsAzure.Storage.Blob;

namespace IntervueApp.Pages
{
    public class IndexModel : PageModel
    {
        public async Task<IActionResult> OnGet()
        {
			Blob blob = new Blob();
			CloudBlobContainer blobContainer = await blob.GetContainer("newblobcontainer");
			return Page();
        }
    }
}
