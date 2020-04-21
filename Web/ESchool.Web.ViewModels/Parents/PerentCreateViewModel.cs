namespace ESchool.Web.ViewModels.Parents
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.Text;

    using ESchool.Data.Models;
    using ESchool.Services.Mapping;
    using ESchool.Web.ViewModels.Students;

    public class PerentCreateViewModel : IMapFrom<Parent>
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Името е задължително")]
        [Display(Name = "Име")]
        [StringLength(10, ErrorMessage = "{0} имети трябва да е между {2} и {1}.", MinimumLength = 3)]
        public string FirstName { get; set; }

        [Required(ErrorMessage = "Името е задължително")]
        [Display(Name = "Име")]
        [StringLength(10, ErrorMessage = "{0} имети трябва да е между {2} и {1}.", MinimumLength = 3)]
        public string LastName { get; set; }

        [Required]
        [EmailAddress]
        public string Email { get; set; }

        public int StudentId { get; set; }

        public string UserId { get; set; }

        public IEnumerable<StudentViewModel> Students { get; set; }
    }
}
