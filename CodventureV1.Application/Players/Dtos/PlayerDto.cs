namespace CodventureV1.Application.Players.Dtos;

public sealed record PlayerDto(Guid Id, string UserName, string Email, int TypingSpeed, int LearningSpeed);