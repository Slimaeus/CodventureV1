using CodventureV1.Domain.Common.Classes;

namespace CodventureV1.Domain.Skills;

public class Skill : BaseEntity<int>
{
    public Skill()
    {

    }
    public Skill(string name, string code)
    {
        Name = name;
        Code = code;
    }
    public override int Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public string Code { get; set; } = string.Empty;

    public int SkillTypeId { get; set; }
    public SkillType? Type { get; set; }

    public virtual ICollection<PlayerSkill> PlayerSkills { get; set; } = new HashSet<PlayerSkill>();
}
