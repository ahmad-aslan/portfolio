using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Portfolio.DataAccess.Repository.IRepository;
using Portfolio.Models;
using Portfolio.Models.ViewModels;
using Utility;

namespace Portfolio.Areas.Admin.Controllers
{
    [Area(nameof(Admin))]
    [Authorize(Roles = AppConstant.Role_Admin)]
    public class ProjectController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IWebHostEnvironment _webHostEnvironment;
        public ProjectController(IUnitOfWork unitOfWork, IWebHostEnvironment webHostEnvironment)
        {
            _unitOfWork = unitOfWork;
            _webHostEnvironment = webHostEnvironment;
        }

        public IActionResult Index()
        {

            List<Project> project = _unitOfWork.Project.GetAll(includeProperties: "Owner").ToList();

            return View(project);
        }



        public IActionResult Upsert(int? id)
        {
            ProjectOwnerVM projectOwnerVM = new()
            {
                OwnerList = _unitOfWork.Owner.GetAll().Select(c => new SelectListItem
                {
                    Text = c.Name,
                    Value = c.Id.ToString()
                }),
                Project = new Project()
            };
            if (id == null || id == 0)
            {
                //Create
                return View(projectOwnerVM);
            }
            else
            {
                //Update
                projectOwnerVM.Project = _unitOfWork.Project
                    .Get(p => p.Id == id, includeProperties: "ProjectImages");

                return View(projectOwnerVM);
            }
        }


        [HttpPost]
        public IActionResult Upsert(ProjectOwnerVM projectOwnerVM, List<IFormFile> files)
        {
            if (ModelState.IsValid)
            {
                if (projectOwnerVM.Project.Id == 0)
                {
                    _unitOfWork.Project.Add(projectOwnerVM.Project);
                    TempData["success"] = "Education Created Successfully";
                }
                else
                {
                    _unitOfWork.Project.Update(projectOwnerVM.Project);
                    TempData["success"] = "Education Updated Successfully";
                }
                _unitOfWork.Save();

                // Setting Images 
                string wwwRootPath = _webHostEnvironment.WebRootPath;
                if (files != null)
                {
                    foreach (var file in files)
                    {
                        //Rename Image
                        string fileName = $"{Guid.NewGuid().ToString()}" +
                            $"{Path.GetExtension(file.FileName)}";

                        // path diractory
                        string productPath = @"images/project/project-" + projectOwnerVM.Project.Id;
                        string finalPath = Path.Combine(wwwRootPath, productPath);

                        if (!Directory.Exists(finalPath))
                        {
                            Directory.CreateDirectory(finalPath);
                        }

                        using (var fileStream = new FileStream(Path.Combine(finalPath, fileName), FileMode.Create))
                        {
                            file.CopyTo(fileStream);
                        }

                        ProjectImage projectImage = new()
                        {
                            ImageUrl = $"/{productPath}/{fileName}",
                            ProjectId = projectOwnerVM.Project.Id,
                        };

                        if (projectOwnerVM.Project.ProjectImages == null)
                        {
                            projectOwnerVM.Project.ProjectImages = new List<ProjectImage>();
                        }

                        projectOwnerVM.Project.ProjectImages.Add(projectImage);
                    }
                    _unitOfWork.Project.Update(projectOwnerVM.Project);
                    _unitOfWork.Save();
                }
                return RedirectToAction(nameof(Index));
            }
            else
            {
                projectOwnerVM.OwnerList = _unitOfWork.Owner.GetAll().Select(c => new SelectListItem
                {
                    Text = c.Name,
                    Value = c.Id.ToString()
                });
                return View(projectOwnerVM);
            }
        }


        public IActionResult DeleteImage(int imageId)
        {
            var imageToBeDeleted = _unitOfWork.ProjectImage.Get(i => i.Id == imageId);
            var projectId = imageToBeDeleted.ProjectId;
            if (imageToBeDeleted != null)
            {
                if (!string.IsNullOrEmpty(imageToBeDeleted.ImageUrl))
                {
                    var oldImagePath =
                        Path.Combine(_webHostEnvironment.WebRootPath,
                        imageToBeDeleted.ImageUrl.TrimStart('\\'));

                    if (System.IO.File.Exists(oldImagePath))
                    {
                        System.IO.File.Delete(oldImagePath);
                    }
                }
                _unitOfWork.ProjectImage.Remove(imageToBeDeleted);
                _unitOfWork.Save();

                TempData["success"] = "Image Deleted Successfully";
            }
            return RedirectToAction(nameof(Upsert), new { id = projectId });
        }


        #region API CALLS     
        [HttpDelete]
        public IActionResult Delete(int? id)
        {
            var projectToBeDeleted = _unitOfWork.Project.Get(c => c.Id == id);
            if (projectToBeDeleted == null)
            {
                return Json(new { success = false, message = "Error while Deleting" });
            }

            // path diractory
            string projectPath = @"images/project/project-" + id;
            string finalPath = Path.Combine(_webHostEnvironment.WebRootPath, projectPath);

            if (Directory.Exists(finalPath))
            {
                string[] filePaths = Directory.GetFiles(finalPath);
                foreach (string filePath in filePaths)
                {
                    System.IO.File.Delete(filePath);
                }
                Directory.Delete(finalPath);
            }
            _unitOfWork.Project.Remove(projectToBeDeleted);
            _unitOfWork.Save();

            return Json(new { success = true, message = "Delete Successful" });
        }
        #endregion
    }
}
