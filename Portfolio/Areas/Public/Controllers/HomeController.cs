using Microsoft.AspNetCore.Mvc;
using Portfolio.DataAccess.Repository.IRepository;
using Portfolio.Models;
using Portfolio.Models.ViewModels;
using System.Diagnostics;

namespace Portfolio.Areas.Public.Controllers
{
    [Area(nameof(Public))]
    public class HomeController : Controller
    {
        [BindProperty]
        public PortfolioVM portfolioVM { get; set; }

        private readonly ILogger<HomeController> _logger;
        private readonly IUnitOfWork _unitOfWork;

        public HomeController(ILogger<HomeController> logger, IUnitOfWork unitOfWork)
        {
            _logger = logger;
            _unitOfWork = unitOfWork;
        }

        public IActionResult Index()
        {
            portfolioVM = new()
            {
                Owner = _unitOfWork.Owner.GetAll().FirstOrDefault(),
                Educations = _unitOfWork.Education.GetAll(),
                Experiences = _unitOfWork.Experience.GetAll(),
                Skills = _unitOfWork.Skill.GetAll(),
                Projects = _unitOfWork.Project.GetAll(includeProperties: "ProjectImages"),
                Socials = _unitOfWork.Social.GetAll()
            };
            return View(portfolioVM);
        }








        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
