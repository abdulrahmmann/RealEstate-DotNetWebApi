namespace RealEstate.Application.Features.AgentFeature.DTOs;

public record UpdateAgentDto(string Name, string Email, string Phone, string ServiceArea, string ImageUrl, string AgencyName,
    string Country, string City, string Street, string ZipCode);