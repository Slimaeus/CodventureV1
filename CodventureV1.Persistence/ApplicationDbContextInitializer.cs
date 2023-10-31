using CodventureV1.Domain.Players;
using CodventureV1.Domain.Skills;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Serilog;

namespace CodventureV1.Persistence;

public sealed class ApplicationDbContextInitializer
{
    private readonly ApplicationDbContext _applicationDbContext;
    private readonly UserManager<Player> _userManager;

    public ApplicationDbContextInitializer(ApplicationDbContext applicationDbContext, UserManager<Player> userManager)
    {
        _applicationDbContext = applicationDbContext;
        _userManager = userManager;
    }

    public async Task InitialiseAsync()
    {
        try
        {
            await _applicationDbContext.Database.MigrateAsync();
        }
        catch (Exception ex)
        {
            Log.Error(ex, "Migration error");
        }
    }

    public async Task SeedAsync()
    {
        try
        {
            await TrySeedAsync();
        }
        catch (Exception ex)
        {
            Log.Error(ex, "Seeding error");
        }
    }

    public async Task TrySeedAsync()
    {
        if (await _userManager.Users.AnyAsync()
            || await _applicationDbContext.Skills.AnyAsync())
            return;

        var thai = new Player("thai", "thai@gmail.com");

        await _userManager.CreateAsync(thai, PlayerConstants.DefaultPassword);

        var programmingLanguageSkillType = new SkillType("Programming Language", "PL");
        var cloudProviderSkillType = new SkillType("Cloud Provider", "CP");
        var stateManagementSkillType = new SkillType("State Management", "SM");
        var projectManagementSkillType = new SkillType("Project Management", "PM");
        var databaseSkillType = new SkillType("Database", "DB");

        await _applicationDbContext
            .AddRangeAsync(
            programmingLanguageSkillType,
            cloudProviderSkillType,
            stateManagementSkillType,
            databaseSkillType,
            projectManagementSkillType);

        var cSharpSkill = new Skill("C#", "001") { Type = programmingLanguageSkillType };
        var typeScriptSkill = new Skill("TypeScript", "002") { Type = programmingLanguageSkillType };
        var dartSkill = new Skill("Dart", "003") { Type = programmingLanguageSkillType };

        var awsSkill = new Skill("AWS", "001") { Type = cloudProviderSkillType };
        var azureSkill = new Skill("Azure", "002") { Type = cloudProviderSkillType };

        var react = new Skill("React", "001") { Type = stateManagementSkillType };
        var flutter = new Skill("Flutter", "002") { Type = stateManagementSkillType };


        var postgreSQL = new Skill("Postgres", "001") { Type = databaseSkillType };
        var mongoDB = new Skill("MongoDB", "002") { Type = databaseSkillType };

        var github = new Skill("Github", "001") { Type = projectManagementSkillType };
        var jira = new Skill("Jira", "002") { Type = projectManagementSkillType };


        await _applicationDbContext
            .AddRangeAsync(
            cSharpSkill, typeScriptSkill, dartSkill,
            awsSkill, azureSkill,
            react, flutter,
            postgreSQL, mongoDB,
            github, jira);

        thai.PlayerSkills
            .Add(new PlayerSkill { Skill = cSharpSkill });

        await _applicationDbContext
            .SaveChangesAsync()
            .ConfigureAwait(false);
    }
}
