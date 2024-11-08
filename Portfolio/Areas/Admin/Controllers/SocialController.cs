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
    public class SocialController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;
        public SocialController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public IActionResult Index()
        {

            List<Social> social = _unitOfWork.Social.GetAll(includeProperties: "Owner").ToList();

            return View(social);
        }



        public IActionResult Upsert(int? id)
        {
            SocialOwnerVM socialOwnerVM = new()
            {
                OwnerList = _unitOfWork.Owner.GetAll().Select(c => new SelectListItem
                {
                    Text = c.Name,
                    Value = c.Id.ToString()
                }),
                Social = new Social()
            };
            if (id == null || id == 0)
            {
                //Create
                return View(socialOwnerVM);
            }
            else
            {
                //Update
                socialOwnerVM.Social = _unitOfWork.Social.Get(p => p.Id == id);

                return View(socialOwnerVM);
            }
        }


        [HttpPost]
        public IActionResult Upsert(SocialOwnerVM socialOwnerVM)
        {
            if (ModelState.IsValid)
            {
                if (socialOwnerVM.Social.Id == 0)
                {
                    _unitOfWork.Social.Add(socialOwnerVM.Social);
                    TempData["success"] = "Social Created Successfully";
                }
                else
                {
                    _unitOfWork.Social.Update(socialOwnerVM.Social);
                    TempData["success"] = "Social Updated Successfully";
                }
                _unitOfWork.Save();
                return RedirectToAction(nameof(Index));
            }
            else
            {
                socialOwnerVM.OwnerList = _unitOfWork.Owner.GetAll().Select(c => new SelectListItem
                {
                    Text = c.Name,
                    Value = c.Id.ToString()
                });
                
                return View(socialOwnerVM);
            }
        }


        #region API CALLS     
        [HttpDelete]
        public IActionResult Delete(int? id)
        {
            var socialToBeDeleted = _unitOfWork.Social.Get(c => c.Id == id);
            if (socialToBeDeleted == null)
            {
                return Json(new { success = false, message = "Error while Deleting" });
            }
            _unitOfWork.Social.Remove(socialToBeDeleted);
            _unitOfWork.Save();

            return Json(new { success = true, message = "Delete Successful" });
        }
        #endregion
    }
}
