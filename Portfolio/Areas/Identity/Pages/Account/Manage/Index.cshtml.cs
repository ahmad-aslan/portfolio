// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.
#nullable disable

using System;
using System.ComponentModel.DataAnnotations;
using System.Text.Encodings.Web;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Hosting.Internal;
using Portfolio.Models;
using Portfolio.Models.ViewModels;

namespace Portfolio.Areas.Identity.Pages.Account.Manage
{
    public class IndexModel : PageModel
    {
        private readonly UserManager<IdentityUser> _userManager;
        private readonly SignInManager<IdentityUser> _signInManager;
        private readonly IWebHostEnvironment _webHostEnvironment;

        public IndexModel(
            UserManager<IdentityUser> userManager,
            SignInManager<IdentityUser> signInManager,
            IWebHostEnvironment webHostEnvironment)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _webHostEnvironment = webHostEnvironment;
        }

        /// <summary>
        ///     This API supports the ASP.NET Core Identity default UI infrastructure and is not intended to be used
        ///     directly from your code. This API may change or be removed in future releases.
        /// </summary>
        public string Username { get; set; }

        /// <summary>
        ///     This API supports the ASP.NET Core Identity default UI infrastructure and is not intended to be used
        ///     directly from your code. This API may change or be removed in future releases.
        /// </summary>
        [TempData]
        public string StatusMessage { get; set; }

        /// <summary>
        ///     This API supports the ASP.NET Core Identity default UI infrastructure and is not intended to be used
        ///     directly from your code. This API may change or be removed in future releases.
        /// </summary>
        [BindProperty]
        public InputModel Input { get; set; }

        /// <summary>
        ///     This API supports the ASP.NET Core Identity default UI infrastructure and is not intended to be used
        ///     directly from your code. This API may change or be removed in future releases.
        /// </summary>
        public class InputModel
        {
            /// <summary>
            ///     This API supports the ASP.NET Core Identity default UI infrastructure and is not intended to be used
            ///     directly from your code. This API may change or be removed in future releases.
            /// </summary>
            [Phone]
            [Display(Name = "Phone number")]
            public string PhoneNumber { get; set; }

            public string Name { get; set; }
            public string JobTitle { get; set; }
            public string Description { get; set; }
            public string Address { get; set; }
            public DateOnly BirthDate { get; set; }
            [Display(Name = "Profile Image")]
            public string ProfileImage { get; set; }
            [Display(Name = "About Image")]
            public string AboutImage { get; set; }
        }

        private async Task LoadAsync(Owner user)
        {
            var userName = await _userManager.GetUserNameAsync(user);
            var phoneNumber = await _userManager.GetPhoneNumberAsync(user);

            Username = userName;

            Input = new InputModel
            {
                PhoneNumber = phoneNumber,
                Name = user.Name,
                JobTitle = user.JobTitle,
                Description = user.Description,
                Address = user.Address,
                BirthDate = user.BirthDate,
                ProfileImage = user.ProfileImage,
                AboutImage = user.AboutImage,
            };
        }

        public async Task<IActionResult> OnGetAsync()
        {
            var user = await _userManager.GetUserAsync(User) as Owner;
            if (user == null)
            {
                return NotFound($"Unable to load user with ID '{_userManager.GetUserId(User)}'.");
            }

            await LoadAsync(user);
            return Page();
        }

        public async Task<IActionResult> OnPostAsync(IFormFile profileFile, IFormFile aboutFile)
        {
            var user = await _userManager.GetUserAsync(User) as Owner;
            if (user == null)
            {
                return NotFound($"Unable to load user with ID '{_userManager.GetUserId(User)}'.");
            }

            if (!ModelState.IsValid)
            {
                await LoadAsync(user);
                return Page();
            }

            var phoneNumber = await _userManager.GetPhoneNumberAsync(user);
            if (Input.PhoneNumber != phoneNumber)
            {
                var setPhoneResult = await _userManager.SetPhoneNumberAsync(user, Input.PhoneNumber);
                if (!setPhoneResult.Succeeded)
                {
                    StatusMessage = "Unexpected error when trying to set phone number.";
                    return RedirectToPage();
                }
            }

            user.Name = Input.Name;
            user.JobTitle = Input.JobTitle;
            user.Description = Input.Description;
            user.Address = Input.Address;
            user.BirthDate = Input.BirthDate;
            user.ProfileImage = UploadFile(profileFile, user.ProfileImage);
            user.AboutImage = UploadFile(aboutFile, user.AboutImage);

            await _userManager.UpdateAsync(user);

            await _signInManager.RefreshSignInAsync(user);
            StatusMessage = "Your profile has been updated";
            return RedirectToPage();
        }


        private string UploadFile(IFormFile file, string imageUrl)
        {
            string wwwRootPath = _webHostEnvironment.WebRootPath;
            if (file != null)
            {
                // path diractory
                string profilePath = @"images/profile/";

                // new file
                string directoryPath = Path.Combine(wwwRootPath, profilePath);
                if (!Directory.Exists(directoryPath))
                {
                    Directory.CreateDirectory(directoryPath);
                }   
                
                // old file path
                string oldPath = Path.Combine(wwwRootPath, imageUrl);
                // new file path
                string newPath = Path.Combine(wwwRootPath, directoryPath + file.FileName);

                if (oldPath != newPath)
                {
                    System.IO.File.Delete(oldPath);

                    // save mew file
                    using (var fileStream = new FileStream(Path.Combine(directoryPath, file.FileName), FileMode.Create))
                    {
                        file.CopyTo(fileStream);

                    }
                }
                return $"{profilePath}{file.FileName}";
            }
            return imageUrl;
        }
    }
}
