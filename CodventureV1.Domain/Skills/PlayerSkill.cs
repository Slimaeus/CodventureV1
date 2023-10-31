using CodventureV1.Domain.Common.Interfaces;
using CodventureV1.Domain.Players;

namespace CodventureV1.Domain.Skills;

public class PlayerSkill : IConjunctionEntity<Guid, int>
{
    public PlayerSkill() { }
    public PlayerSkill(Skill? skill)
        => Skill = skill;
    public PlayerSkill(Guid playerId, int skillId)
    {
        PlayerId = playerId;
        SkillId = skillId;
    }
    public Guid PlayerId { get; set; }
    public virtual Player? Player { get; set; }

    public int SkillId { get; set; }
    public virtual Skill? Skill { get; set; }

    public int Proficiency = SkillConstants.DefaultPlayerProficiency;

    public Guid Id1 => PlayerId;
    public int Id2 => SkillId;
}
