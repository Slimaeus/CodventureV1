using CodventureV1.Domain.Common.Interfaces;
using CodventureV1.Domain.Skills;
using Microsoft.AspNetCore.Identity;

namespace CodventureV1.Domain.Players;

public class Player : IdentityUser<Guid>, IEntity<Guid>
{
    public Player()
    {

    }
    public Player(string userName, string email, int typingSpeed = PlayerConstants.DefaultTypingSpeed, int learningSpeed = PlayerConstants.DefaultLearningSpeed)
    {
        UserName = userName;
        Email = email;
        TypingSpeed = typingSpeed;
        LearningSpeed = learningSpeed;
    }

    public int TypingSpeed { get; set; } = PlayerConstants.DefaultTypingSpeed;
    public int LearningSpeed { get; set; } = PlayerConstants.DefaultLearningSpeed;

    public virtual ICollection<PlayerSkill> PlayerSkills { get; set; } = new HashSet<PlayerSkill>();
}
