using CodventureV1.Domain.Players;

namespace CodventureV1.Presentation.Players.Constants;

public static class PlayerRoutes
{
    public const string GroupPattern = "api/Players";
    public const string Tag = nameof(Player);

    public const string Get = "";
    public const string GetById = "/{id:guid}";
}
