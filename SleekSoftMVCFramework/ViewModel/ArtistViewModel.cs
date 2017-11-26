using SleekSoftMVCFramework.Data.IdentityModel;
using SleekSoftMVCFramework.Utilities;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace SleekSoftMVCFramework.ViewModel
{
    public class ArtistViewModel
    {
        public Int64 Id { get; set; }

        [Required(ErrorMessage = "* Required")]
        [DisplayName("First Name")]
        [StringLength(50)]
        public string FirstName { get; set; }

        [Required(ErrorMessage = "* Required")]
        [DisplayName("Last Name")]
        [StringLength(50)]
        public string LastName { get; set; }

        [DisplayName("Middle Name")]
        [StringLength(50)]
        public string MiddleName { get; set; }


        [Required(ErrorMessage = "* Required")]
        [DisplayName("User Name")]
        [StringLength(50)]
        public string UserName { get; set; }

        [Required(ErrorMessage = "* Required")]
        [DisplayName("Mobile Number")]
        [StringLength(50)]
        public string MobileNumber { get; set; }

        [DisplayName("Phone Number")]
        [StringLength(50)]
        public string PhoneNumber { get; set; }

        [DisplayName("Date of Birth")]
        public string DOB { get; set; }

        [DisplayName("Home Address")]
        public string Address { get; set; }

        [Required(ErrorMessage = "* Required")]
        [DisplayName("Email")]
        [RegularExpression(@"[A-Za-z0-9._%+-]+@[A-Za-z0-9.-]+\.[A-Za-z]{2,4}", ErrorMessage = "Email is not valid.")]
        [DataType(DataType.EmailAddress)]
        public string Email { get; set; }

        [DisplayName("FaceBook URL")]
        public string FacebookURL { get; set; }

        public string PicturePath { get; set; }


        public static ApplicationUser ModeltoEntity(ArtistViewModel model)
        {
            return model == null
               ? null
               : new ApplicationUser
               {
                   FirstName = model.FirstName,
                   LastName = model.LastName,
                   MiddleName = model.MiddleName,
                   UserName = model.UserName,
                   MobileNumber = model.MobileNumber,
                   PhoneNumber = model.PhoneNumber,
                   Email = model.Email,
                   DOB = !string.IsNullOrEmpty(model.DOB) ? ExtentionUtility.ConvertDateValue(model.DOB) : DateTime.MinValue,
                   Address = model.Address,
                   FacebookURL=model.FacebookURL,
                   EmailConfirmed = true,
                   PhoneNumberConfirmed = true,
                   TwoFactorEnabled = false,
                   LockoutEnabled = false,
                   AccessFailedCount = 0,
                   DateCreated = DateTime.Now,
                   IsFirstLogin = true
               };
        }


        public static ArtistViewModel EntityToModels(ApplicationUser dbmodel)
        {
            return dbmodel == null
                ? null
                : new ArtistViewModel
                {
                    Id = dbmodel.Id,
                    FirstName = dbmodel.FirstName,
                    LastName = dbmodel.LastName,
                    MiddleName = dbmodel.MiddleName,
                    UserName = dbmodel.UserName,
                    MobileNumber = dbmodel.MobileNumber,
                    PhoneNumber = dbmodel.PhoneNumber,
                    Email = dbmodel.Email,
                    DOB = dbmodel.DOB.HasValue ? dbmodel.DOB.GetValueOrDefault().ToString("dd/MM/yyyy") : string.Empty,
                    Address = dbmodel.Address,
                    FacebookURL = dbmodel.FacebookURL,
                    PicturePath=dbmodel.PicturePath,
                };
        }
    }
}