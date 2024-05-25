
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using TeamsHubWebClient.DTOs;
using TeamsHubWebClient.Gateways.Interfaces;
using TeamsHubWebClient.SinglentonClasses;

namespace TeamsHubWebClient.Pages
{
    public class IndexModel : PageModel
    {
        private readonly ILogger<IndexModel> _logger;

        private readonly IProjectManager _projectManager;
        
        [BindProperty]
        public DateTime StartDate {get; set;}
        [BindProperty]
        public DateTime EndDate {get; set;}
        [BindProperty]
        public ProjectDTO Project {get; set;}  
        public bool errorGuardandoCurso { get; set; }
        public bool mostrarCurso { get; set; }
        public List<ProjectDTO> listaCursos {get; set;}       

        public IndexModel(ILogger<IndexModel> logger, IProjectManager projectManager)
        {
            _logger = logger;
            _projectManager = projectManager;
            Project = new ProjectDTO() {Name="Proyecto Prueba Piloto", StartDate=DateTime.Now.AddDays(1), EndDate=DateTime.Now.AddDays(31)};
        }  
        
        public void OnGet()
        {
            StartDate = DateTime.Now.AddDays(-30);
            EndDate = DateTime.Now;
            listaCursos = _projectManager.GetAllMyProjects(StudentSinglenton.Id);            
        }

        public void OnPostFechas() {                        
            listaCursos = _projectManager.GetProjectsbyDate(StartDate, EndDate);        
        }

        public IActionResult OnPostMove(int IdProject, string NameProject)
        {
            ProjectSinglenton.Id = IdProject;
            ProjectSinglenton.Name = NameProject;
            return RedirectToPage("/ActivitiesModule");
        }

        
        public void OnPostCurso() {
            bool resultado = false;
            
            if (Project.IdProject == 0)
                resultado = _projectManager.AddProject(Project);
            else {
                resultado = _projectManager.UpdateProject(Project);                
            }
            _logger.LogInformation("El resultado de la operación es "+resultado);
            if (!resultado)
                    errorGuardandoCurso = true;
            else {
                    Project = new ProjectDTO() {Name="Proyecto Prueba Piloto", StartDate=DateTime.Now.AddDays(1), EndDate=DateTime.Now.AddDays(31)};                    
            }           
            StartDate = DateTime.Now.AddDays(-30);
            EndDate = DateTime.Now;
            listaCursos = _projectManager.GetAllMyProjects(StudentSinglenton.Id);           
        }

        public void OnPostConsultarCurso() {                                                        
            StartDate = DateTime.Now.AddDays(-30);
            EndDate = DateTime.Now;
            listaCursos = _projectManager.GetAllMyProjects(StudentSinglenton.Id);     
            
            Project = listaCursos.FirstOrDefault(c => c.IdProject == Project.IdProject);
            
            if (Project == null)
            {
                mostrarCurso = false;
                Project = new ProjectDTO() {Name="Proyecto Prueba Piloto", StartDate=DateTime.Now.AddDays(1), EndDate=DateTime.Now.AddDays(31)};
            }
            else
                mostrarCurso = true;
        }
    }
}