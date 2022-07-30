
using FluentValidation;
using MOCA.Core.Interfaces.LocationManagment.Repositories;
using System.ComponentModel.DataAnnotations;

namespace MOCA.Core.DTOs.LocationManagment.Country
{
    public class CountryModel
    {
        public long Id { get; set; }
        public string CountryName { get; set; }
        [Required]
        public string CountryCode { get; set; }
        [Required]
        public string CountryCodeString { get; set; }
    }

    public class Country_Validator : AbstractValidator<CountryModel>
    {
        private readonly ICountryRepository _repo;

        public Country_Validator(ICountryRepository repo)
        {
            _repo = repo;

            RuleFor(x => x.CountryName)
                .NotNull()
                .NotEmpty().WithMessage("{PropertyName} is required.")
                .MaximumLength(255).WithMessage("{PropertyName} must not exceed 255 characters.")
                .MustAsync(IsUniqueCountryName).WithMessage("{PropertyName} already exists.");
        }

        public async Task<bool> IsUniqueCountryName(string BranchName, CancellationToken cancellationToken)
        {
            var IsUnique = await _repo.IsUniqueNameAsync(BranchName);
            return IsUnique;
        }
    }
}
