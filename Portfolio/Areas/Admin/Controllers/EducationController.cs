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
    public class EducationController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;
        public EducationController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public IActionResult Index()
        {

            List<Education> education = _unitOfWork.Education.GetAll(includeProperties: "Owner").ToList();

            return View(education);
        }



        public IActionResult Upsert(int? id)
        {
            EducationOwnerVM educationOwnerVM = new()
            {
                OwnerList = _unitOfWork.Owner.GetAll().Select(c => new SelectListItem
                {
                    Text = c.Name,
                    Value = c.Id.ToString()
                }),
                Education = new Education()
            };
            if (id == null || id == 0)
            {
                //Create
                return View(educationOwnerVM);
            }
            else
            {
                //Update
                educationOwnerVM.Education = _unitOfWork.Education.Get(p => p.Id == id);

                return View(educationOwnerVM);
            }
        }


        [HttpPost]
        public IActionResult Upsert(EducationOwnerVM educationVM)
        {
            if (ModelState.IsValid)
            {
                if (educationVM.Education.Id == 0)
                {
                    _unitOfWork.Education.Add(educationVM.Education);
                    TempData["success"] = "Education Created Successfully";
                }
                else
                {
                    _unitOfWork.Education.Update(educationVM.Education);
                    TempData["success"] = "Education Updated Successfully";
                }
                _unitOfWork.Save();
                return RedirectToAction(nameof(Index));
            }
            else
            {
                educationVM.OwnerList = _unitOfWork.Owner.GetAll().Select(c => new SelectListItem
                {
                    Text = c.Name,
                    Value = c.Id.ToString()
                });
                return View(educationVM);
            }
        }


        #region API CALLS     
        [HttpDelete]
        public IActionResult Delete(int? id)
        {
            var educationToBeDeleted = _unitOfWork.Education.Get(c => c.Id == id);
            if (educationToBeDeleted == null)
            {
                return Json(new { success = false, message = "Error while Deleting" });
            }
            _unitOfWork.Education.Remove(educationToBeDeleted);
            _unitOfWork.Save();

            return Json(new { success = true, message = "Delete Successful" });
        }
        #endregion
    }
}
