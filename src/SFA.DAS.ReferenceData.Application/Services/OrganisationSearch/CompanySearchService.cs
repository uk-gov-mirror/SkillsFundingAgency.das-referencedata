﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using SFA.DAS.NLog.Logger;
using SFA.DAS.ReferenceData.Domain.Interfaces.Services;
using SFA.DAS.ReferenceData.Types.DTO;
using Address = SFA.DAS.ReferenceData.Types.DTO.Address;

namespace SFA.DAS.ReferenceData.Application.Services.OrganisationSearch
{
    public class CompanySearchService : IOrganisationReferenceSearchService, IOrganisationTextSearchService
    {
        private readonly ICompaniesHouseEmployerVerificationService _companyVerificationService;
        private readonly ILog _logger;

        public OrganisationType OrganisationType => OrganisationType.Company;

        public CompanySearchService(ICompaniesHouseEmployerVerificationService companyVerificationService, ILog logger)
        {
            _companyVerificationService = companyVerificationService;
            
            _logger = logger;
        }

        public bool IsSearchTermAReference(string searchTerm)
        {
            return Regex.IsMatch(searchTerm, "^[A-Za-z0-9]{2}[0-9]{6}$");
        }

        public async Task<Organisation> Search(string reference)
        {
            try
            {
                var info = await _companyVerificationService.GetInformation(reference);

                if (info == null)
                    return null;

                return new Organisation
                {
                    Name = info.CompanyName,
                    Address = FormatAddress(info.RegisteredAddress),
                    Code = info.CompanyNumber,
                    RegistrationDate = info.DateOfIncorporation,
                    Type = OrganisationType.Company,
                    SubType = OrganisationSubType.None
                };

            }

            catch (Exception e)
            {
                _logger.Error(e, "Could not get Company information from companies house");
            }

            return null;
        }

        public async Task<IEnumerable<Organisation>> Search(string searchTerm, int maximumRecords)
        {
            try
            {
                var results = await _companyVerificationService.FindCompany(searchTerm, maximumRecords);
              
                return results.Companies?.Select(c => new Organisation
                {
                    Name = c.CompanyName,
                    Address = FormatAddress(c.Address),
                    Code = c.CompanyNumber,
                    RegistrationDate = c.DateOfIncorporation,
                    Type = OrganisationType.Company,
                    SubType = OrganisationSubType.None
                });
            }

            catch (Exception e)
            {
                _logger.Error(e, "Could not find Company when searching companies house");
            }

            return null;
        }

        private static Address FormatAddress(Domain.Models.Company.Address address)
        {
            if (address == null)
            {
                return new Address();
            }

            return new Address
            {
                Line1 = address.Line1,
                Line2 = address.Line2,
                Line4 = address.TownOrCity,
                Line5 = address.County,
                Postcode = address.PostCode
            };
        }
    }
}
