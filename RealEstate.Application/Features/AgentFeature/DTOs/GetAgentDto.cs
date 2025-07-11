namespace RealEstate.Application.Features.AgentFeature.DTOs;

public record GetAgentDto(string Name, string Email, string Phone, string ServiceArea,
    string Country, string City, string Street, string ZipCode, string AgencyName);