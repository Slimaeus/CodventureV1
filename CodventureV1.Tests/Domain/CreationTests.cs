using CodventureV1.Domain.Players;
using CodventureV1.Domain.Skills;

namespace CodventureV1.Tests.Domain;

[TestFixture]
public class CreationTests
{
    [SetUp]
    public void Setup()
    {
    }

    [Test]
    public void PlayerConstrutor_NoParametersPassed_ReturnsDefaultValue()
    {
        // Arrange
        var player = new Player();

        // Act
        var actualUserName = player.UserName;
        var actualEmail = player.Email;
        var actualTypingSpeed = player.TypingSpeed;
        var actualLearningSpeed = player.LearningSpeed;
        var actualPlayerSkills = player.PlayerSkills;

        // Assert
        Assert.Multiple(() =>
        {
            Assert.That(actualUserName, Is.EqualTo(null));
            Assert.That(actualEmail, Is.EqualTo(null));
            Assert.That(actualTypingSpeed, Is.EqualTo(PlayerConstants.DefaultTypingSpeed));
            Assert.That(actualLearningSpeed, Is.EqualTo(PlayerConstants.DefaultLearningSpeed));
            Assert.That(actualPlayerSkills, Is.EqualTo(new HashSet<PlayerSkill>()));
        });
    }

    [Test]
    public void PlayerConstrutor_NoTypingAndLearningSpeedPassed_ReturnsDefaultValue()
    {
        // Arrange
        var player = new Player("thai", "thai@gmail.com");

        // Act
        var actualTypingSpeed = player.TypingSpeed;
        var actualLearningSpeed = player.LearningSpeed;
        var actualPlayerSkills = player.PlayerSkills;

        // Assert
        Assert.Multiple(() =>
        {
            Assert.That(actualTypingSpeed, Is.EqualTo(PlayerConstants.DefaultTypingSpeed));
            Assert.That(actualLearningSpeed, Is.EqualTo(PlayerConstants.DefaultLearningSpeed));
            Assert.That(actualPlayerSkills, Is.EqualTo(new HashSet<PlayerSkill>()));
        });
    }

    [Test]
    public void SkillTypeConstrutor_NoParametersPassed_ReturnsDefaultValue()
    {
        // Arrange
        var skillType = new SkillType();

        // Act
        var actualName = skillType.Name;
        var actualCode = skillType.Code;

        // Assert
        Assert.Multiple(() =>
        {
            Assert.That(actualName, Is.EqualTo(string.Empty));
            Assert.That(actualCode, Is.EqualTo(string.Empty));
        });
    }

    [Test]
    public void SkillConstrutor_NoParametersPassed_ReturnsDefaultValue()
    {
        // Arrange
        var skill = new Skill();

        // Act
        var actualName = skill.Name;
        var actualCode = skill.Code;
        var actualPlayerSkills = skill.PlayerSkills;

        // Assert
        Assert.Multiple(() =>
        {
            Assert.That(actualName, Is.EqualTo(string.Empty));
            Assert.That(actualCode, Is.EqualTo(string.Empty));
            Assert.That(actualPlayerSkills, Is.EqualTo(new HashSet<PlayerSkill>()));
        });
    }
}
