using Microsoft.AspNetCore.Authorization;
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
    public class ExperienceController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;
        public ExperienceController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public IActionResult Index()
        {
            List<Experience> experience = _unitOfWork.Experience
                .GetAll(includeProperties: "Owner").ToList();
            return View(experience);
        }



        public IActionResult Upsert(int? id)
        {
            ExperienceOwnerVM experienceOwnerVM = new()
            {
                OwnerList = _unitOfWork.Owner.GetAll().Select(c => new SelectListItem
                {
                    Text = c.Name,
                    Value = c.Id.ToString()
                }),
                Experience = new Experience()
            };
            if (id == null || id == 0)
            {
                //Create
                return View(experienceOwnerVM);
            }
            else
            {
                //Update
                experienceOwnerVM.Experience = _unitOfWork.Experience.Get(p => p.Id == id);

                return View(experienceOwnerVM);
            }
        }


        [HttpPost]
        public IActionResult Upsert(ExperienceOwnerVM experienceOwnerVM)
        {
            if (ModelState.IsValid)
            {
                if (experienceOwnerVM.Experience.Id == 0)
                {
                    _unitOfWork.Experience.Add(experienceOwnerVM.Experience);
                    TempData["success"] = "Education Created Successfully";
                }
                else
                {
                    _unitOfWork.Experience.Update(experienceOwnerVM.Experience);
                    TempData["success"] = "Education Updated Successfully";
                }
                _unitOfWork.Save();
                return RedirectToAction(nameof(Index));
            }
            else
            {
                experienceOwnerVM.OwnerList = _unitOfWork.Owner.GetAll().Select(c => new SelectListItem
                {
                    Text = c.Name,
                    Value = c.Id.ToString()
                });
            
            return View(experienceOwnerVM);
            }
        }


        #region API CALLS     
        [HttpDelete]
        public IActionResult Delete(int? id)
        {
            var experienceToBeDeleted = _unitOfWork.Experience.Get(c => c.Id == id);
            if (experienceToBeDeleted == null)
            {
                return Json(new { success = false, message = "Error while Deleting" });
            }
            _unitOfWork.Experience.Remove(experienceToBeDeleted);
            _unitOfWork.Save();

            return Json(new { success = true, message = "Delete Successful" });
        }
        #endregion
    }
}
