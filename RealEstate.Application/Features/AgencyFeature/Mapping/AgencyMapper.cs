using RealEstate.Application.Features.AgencyFeature.DTOs;
using RealEstate.Domain.Entities;

namespace RealEstate.Application.Features.AgencyFeature.Mapping;

public static class AgencyMapper
{
    #region Map from Agency Entity to AgencyDto 
    public static AgencyDto To_AgencyDto(this Agency agency)
    {
        return new AgencyDto(agency.Id, agency.Name, agency.LicenseNumber, agency.TaxNumber);
    }

    public static IEnumerable<AgencyDto> To_AgencyDto_List(this IEnumerable<Agency> agencies)
    {
        return agencies.Select(a => a.To_AgencyDto());
    }
    #endregion


    #region Map from AgencyDto to Agency Entity 
    public static Agency To_AgencyDto(this AgencyDto agency)
    {
        return new Agency
        {
            Id = agency.Id,
            Name = agency.Name,
            LicenseNumber = agency.LicenseNumber,
            TaxNumber = agency.TaxNumber,
        };
    }

    public static IEnumerable<Agency> To_AgencyDto_List(this IEnumerable<AgencyDto> agencies)
    {
        return agencies.Select(a => a.To_AgencyDto());
    }
    #endregion
    
    
    #region Map from Agency Entity to AddAgencyDto 
    public static AddAgencyDto To_AddAgencyDto(this Agency agency)
    {
        return new AddAgencyDto(agency.Name, agency.LicenseNumber, agency.TaxNumber);
    }

    public static IEnumerable<AddAgencyDto> To_AddAgencyDto_List(this IEnumerable<Agency> agencies)
    {
        return agencies.Select(a => a.To_AddAgencyDto());
    }
    #endregion
    
    
    #region Map from AddAgencyDto to Agency Entity 
    public static Agency To_AddAgencyDto(this AddAgencyDto agency)
    {
        return new Agency
        {
            Name = agency.Name,
            LicenseNumber = agency.LicenseNumber,
            TaxNumber = agency.TaxNumber,
        };
    }

    public static IEnumerable<Agency> To_AddAgencyDto_List(this IEnumerable<AddAgencyDto> agencies)
    {
        return agencies.Select(a => a.To_AddAgencyDto());
    }
    #endregion


    #region Map Update Agency

    public static void UpdateAgencyDto(Agency agency, UpdateAgencyDto agencyDto)
    {
        agency.Name = agencyDto.Name ?? agency.Name;
        agency.LicenseNumber = agencyDto.LicenseNumber ?? agency.LicenseNumber;
        agency.TaxNumber = agencyDto.TaxNumber ?? agency.TaxNumber;
    }
    #endregion
}
