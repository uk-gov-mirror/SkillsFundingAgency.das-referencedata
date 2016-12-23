﻿using System.Threading.Tasks;
using MediatR;
using SFA.DAS.ReferenceData.Domain.Interfaces.Data;
using SFA.DAS.ReferenceData.Domain.Models;

namespace SFA.DAS.ReferenceData.Application.Queries.GetPublicOrganisations
{
    public class GetPublicSectorOrganisationsHandler : IAsyncRequestHandler<FindPublicSectorOrgainsationQuery, FindPublicSectorOrganisationResponse>
    {
        private readonly IPublicSectorOrganisationRepository _publicSectorOrganisationRepository;


        public GetPublicSectorOrganisationsHandler(IPublicSectorOrganisationRepository publicSectorOrganisationRepository)
        {
            _publicSectorOrganisationRepository = publicSectorOrganisationRepository;
        }

        public async Task<FindPublicSectorOrganisationResponse> Handle(FindPublicSectorOrgainsationQuery query)
        {
            var result = await _publicSectorOrganisationRepository.FindOrganisations(
                query.SearchTerm,
                query.PageSize,
                query.PageNumber);

            return new FindPublicSectorOrganisationResponse
            {
                Organisations = new Api.Client.Dto.PagedApiResponse<PublicSectorOrganisation>
                {
                    Data = result.Data,
                    PageNumber = result.Page,
                    TotalPages = result.TotalPages
                }
            };
        }
    }
}
