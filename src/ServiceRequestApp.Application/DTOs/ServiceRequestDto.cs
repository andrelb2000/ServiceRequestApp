using System;
namespace ServiceRequestApp.Application.DTOs;
public record ServiceRequestDto(
    int Id,
    string Title,
    string Description,
    int Status,
    DateTimeOffset? CreatedAt,
    DateTimeOffset? CompletedAt
);