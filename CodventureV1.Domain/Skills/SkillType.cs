using CodventureV1.Domain.Common.Classes;

namespace CodventureV1.Domain.Skills;

public class SkillType : BaseEntity<int>
{
    public SkillType(string name, string code)
    {
        Name = name;
        Code = code;
    }
    public override int Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public string Code { get; set; } = string.Empty;

    public virtual ICollection<Skill> Skills { get; set; } = new HashSet<Skill>();
}
