namespace RealEstate.Application.Features.AgentFeature.DTOs;

public record AddAgentDto(string Name, string Email, string Phone, string ServiceArea, string ImageUrl, 
    string Country, string City, string Street, string ZipCode, int AgencyId);