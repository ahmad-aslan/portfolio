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
    public class SkillController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;
        public SkillController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public IActionResult Index()
        {

            List<Skill> skill = _unitOfWork.Skill.GetAll(includeProperties: "Owner").ToList();

            return View(skill);
        }



        public IActionResult Upsert(int? id)
        {
            SkillOwnerVM skillOwnerVM = new()
            {
                OwnerList = _unitOfWork.Owner.GetAll().Select(c => new SelectListItem
                {
                    Text = c.Name,
                    Value = c.Id.ToString()
                }),
                Skill = new Skill()
            };
            if (id == null || id == 0)
            {
                //Create
                return View(skillOwnerVM);
            }
            else
            {
                //Update
                skillOwnerVM.Skill = _unitOfWork.Skill.Get(p => p.Id == id);

                return View(skillOwnerVM);
            }
        }


        [HttpPost]
        public IActionResult Upsert(SkillOwnerVM skillOwnerVM)
        {
            if (ModelState.IsValid)
            {
                if (skillOwnerVM.Skill.Id == 0)
                {
                    _unitOfWork.Skill.Add(skillOwnerVM.Skill);
                    TempData["success"] = "Skill Created Successfully";
                }
                else
                {
                    _unitOfWork.Skill.Update(skillOwnerVM.Skill);
                    TempData["success"] = "Skill Updated Successfully";
                }
                _unitOfWork.Save();
                return RedirectToAction(nameof(Index));
            }
            else
            {
                skillOwnerVM.OwnerList = _unitOfWork.Owner.GetAll().Select(c => new SelectListItem
                {
                    Text = c.Name,
                    Value = c.Id.ToString()
                });
                
                return View(skillOwnerVM);
            }
        }


        #region API CALLS     
        [HttpDelete]
        public IActionResult Delete(int? id)
        {
            var skillToBeDeleted = _unitOfWork.Skill.Get(c => c.Id == id);
            if (skillToBeDeleted == null)
            {
                return Json(new { success = false, message = "Error while Deleting" });
            }
            _unitOfWork.Skill.Remove(skillToBeDeleted);
            _unitOfWork.Save();

            return Json(new { success = true, message = "Delete Successful" });
        }
        #endregion
    }
}
