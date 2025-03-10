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
using MovieForum2.Data;

namespace MovieForum2.Areas.Identity.Pages.Account.Manage
{
    public class IndexModel : PageModel
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly SignInManager<ApplicationUser> _signInManager;

        public IndexModel(
            UserManager<ApplicationUser> userManager,
            SignInManager<ApplicationUser> signInManager)
        {
            _userManager = userManager;
            _signInManager = signInManager;
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

            [Required]
            [DataType(DataType.Text)]
            [Display(Name = "Name")]
            public string Name { get; set; }

            [Required]
            [DataType(DataType.Text)]
            [Display(Name = "Location")]
            public string Location { get; set; }
            
            [DataType(DataType.ImageUrl)]
            [Display(Name = "ImageFile")]
            public IFormFile ImageFile { get; set; }

            [Phone]
            [Display(Name = "Phone number")]
            public string PhoneNumber { get; set; }

            public string ImageFilename { get; set; } = string.Empty;
        }

        private async Task LoadAsync(ApplicationUser user)
        {
            if (user == null)
            {
                throw new InvalidOperationException("User is not logged in.");
            }

            var userName = await _userManager.GetUserNameAsync(user);
            var phoneNumber = await _userManager.GetPhoneNumberAsync(user);

            Username = userName;

            Input = new InputModel
            {
                Name = user.Name ?? string.Empty, // Avoid null values
                Location = user.Location ?? string.Empty, // Avoid null values
                ImageFilename = user.ImageFilename ?? string.Empty, // Avoid null values
                ImageFile = user.ImageFile ?? null, // Prevent crash if ImageFile is null
                PhoneNumber = phoneNumber ?? string.Empty // Avoid null values
            };
        }

        public async Task<IActionResult> OnGetAsync()
        {
            var user = await _userManager.GetUserAsync(User);

            if (user == null)
            {
                return NotFound($"Unable to load user with ID '{_userManager.GetUserId(User)}'.");
            }

            if (user.ImageFile == null)
            {
                user.ImageFilename = string.Empty;
            }

            await LoadAsync(user);
            return Page();
        }

        public async Task<IActionResult> OnPostAsync()
        {
            var user = await _userManager.GetUserAsync(User);
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

            if (Input.Name != user.Name)
            {
                user.Name = Input.Name;
            }

            if (Input.Location != user.Location)
            {
                user.Location = Input.Location;
            }

            // Handle profile image upload
            if (Input.ImageFile != null)
            {
                if (!string.IsNullOrEmpty(user.ImageFilename))
                {
                    var oldImagePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "images", user.ImageFilename);
                    if (System.IO.File.Exists(oldImagePath))
                    {
                        System.IO.File.Delete(oldImagePath);
                    }
                }

                // Generate new image filename
                user.ImageFilename = Guid.NewGuid().ToString() + Path.GetExtension(Input.ImageFile.FileName);
                string filePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "images", user.ImageFilename);

                using (var fileStream = new FileStream(filePath, FileMode.Create))
                {
                    await Input.ImageFile.CopyToAsync(fileStream);
                }
            }

            // Update user in the database
            var result = await _userManager.UpdateAsync(user);

            if (result.Succeeded)
            {
                await _signInManager.RefreshSignInAsync(user);
                StatusMessage = "Your profile has been updated";
                return RedirectToPage();
            }
            else
            {
                StatusMessage = "Unexpected error when trying to update profile.";
                return RedirectToPage();
            }
        }

    }
}
