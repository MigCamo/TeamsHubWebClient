

using Microsoft.AspNetCore.Mvc.RazorPages;
using TeamsHubWebClient.Gateways.Interfaces;

namespace TeamsHubWebClient.Pages
{
    public class ActivitiesModule : PageModel
    {
         private readonly ILogger<ActivitiesModule> _logger;

        private readonly IProjectManager _projectManager;

        public ActivitiesModule(ILogger<ActivitiesModule> logger, IProjectManager projectManager)
        {
            _logger = logger;
            _projectManager = projectManager;
        }  

        public void OnGet()
        {
       
        }

    }
}