﻿using System.Collections.Generic;
using SFA.DAS.ReferenceData.Api.Client.Dto;

namespace SFA.DAS.ReferenceData.Application.Queries.GetPublicOrganisations
{
    public class FindPublicSectorOrganisationResponse
    {
        public PagedApiResponse<PublicSectorOrganisation> Organisations { get; set; }
    }
}
