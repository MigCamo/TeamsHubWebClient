

using System.Text.Json;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using TeamsHubWebClient.DTOs;
using TeamsHubWebClient.Gateways.Interfaces;
using TeamsHubWebClient.SinglentonClasses;

namespace TeamsHubWebClient.Pages
{
    public class ActivitiesModule : PageModel
    {
         private readonly ILogger<ActivitiesModule> _logger;

        private readonly ITaskManager _TaskManager;
        
        [BindProperty]
        public TaskDTO Task {get; set;}
        public List<TaskDTO> TaskList {get; set;}  

        public ActivitiesModule(ILogger<ActivitiesModule> logger, ITaskManager taskManager)
        {
            _logger = logger;
            _TaskManager = taskManager;
            Task = new TaskDTO();
        }  

        public void OnGet()
        {
            TaskList = _TaskManager.GetAllTaskByProject(ProjectSinglenton.Id);
        }

        public void OnPost() {

            Task.IdProject = ProjectSinglenton.Id;
            _TaskManager.UpdateTask(Task);

        }
    }
}