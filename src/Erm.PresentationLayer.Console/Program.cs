using AutoMapper;
using Erm.BusinessLayer;
using Erm.DataAccess;
using FluentValidation;

class Program
{
    internal static async Task MainAsync(IValidator<RiskProfileDto> validator,
        RiskProfileRepositoryProxy profileRepositoryProxy,
        IMapper mapper)
    {
        IValidator<RiskProfileDto> _validator = validator;
        RiskProfileRepositoryProxy _profileRepositoryProxy = profileRepositoryProxy;
        IMapper _mapper = mapper;
        IRiskProfileService riskProfileService = new RiskProfileService(_validator, _profileRepositoryProxy, _mapper);
        {
        string cmd = string.Empty;

        while (!cmd.Equals(CommandHelper.ExitCommand))
        {
            try
            {
                Console.ForegroundColor = ConsoleColor.DarkGray;
                Console.Write(CommandHelper.InputSymbol);
                cmd = Console.ReadLine();

                switch (cmd)
                {
                    case CommandHelper.CreateRiskProfileCommand:
                        string rpName = Console.ReadLine();
                        string rpDescription = Console.ReadLine();
                        string rpBusinessProcess = Console.ReadLine();
                        if (string.IsNullOrEmpty(rpName) || string.IsNullOrEmpty(rpBusinessProcess) || string.IsNullOrEmpty(rpBusinessProcess))
                        {
                            throw new ArgumentNullException();
                        }

                        int rpOccurrenceProbability;
                        int rpPotentialBusinessImpact;
                        bool rpOccurrenceProbabilityParse = int.TryParse(Console.ReadLine(), out rpOccurrenceProbability); // (1-10)
                        bool rpPotentialBusinessImpactParse = int.TryParse(Console.ReadLine(), out rpPotentialBusinessImpact); // (1-10)
                        if (rpOccurrenceProbability > 10 || rpOccurrenceProbability < 1 ||
                            rpPotentialBusinessImpact > 10 || rpPotentialBusinessImpact < 1)
                        {
                            throw new ArgumentOutOfRangeException();
                        }
                        RiskProfileDto riskProfileInfo = new(
                            rpName, rpDescription, rpBusinessProcess,
                            rpOccurrenceProbability, rpPotentialBusinessImpact
                        );
                        await riskProfileService.CreateAsync(riskProfileInfo);
                        break;
                    case CommandHelper.UpdateRiskProfileCommand:
                        Console.Write("ID: ");
                        int update_id = int.Parse(Console.ReadLine());
                        Console.Write("Name: ");
                        string rpName2 = Console.ReadLine();
                        Console.Write("Description: ");
                        string rpDescription2 = Console.ReadLine();
                        Console.Write("Business Process: ");
                        string rpBusinessProcess2 = Console.ReadLine();
                        Console.Write("Occurence Probability: ");
                        bool checkRpOcPr2 = int.TryParse(Console.ReadLine(), out int rpOccurrenceProbability2); // (1-10)
                        if (!checkRpOcPr2 || rpOccurrenceProbability2 < 1 || rpOccurrenceProbability2 > 10)
                        {
                            throw new Exception("Uncorrect input");
                        }

                        Console.Write("Potential Business Impact: ");
                        bool checkRpPoBsIm2 = int.TryParse(Console.ReadLine(), out int rpPotentialBusinessImpact2); // (1-10)
                        if (!checkRpPoBsIm2 || rpPotentialBusinessImpact2 < 1 || rpPotentialBusinessImpact2 > 10)
                        {
                            throw new Exception("Uncorrect input");
                        }

                        RiskProfileDto riskProfileInfo2 = new(
                            rpName2, rpDescription2, rpBusinessProcess2,
                            rpOccurrenceProbability2, rpPotentialBusinessImpact2
                        );


                        await riskProfileService.UpdateAsync(update_id, riskProfileInfo2);
                        break;
                    case CommandHelper.DeleteRiskProfileCommand:
                        Console.Write("ID: ");
                        int delete_id = int.Parse(Console.ReadLine());
                        await riskProfileService.DeleteAsync(delete_id);
                        break;
                    case CommandHelper.CreateRiskCommand:
                        string rType = Console.ReadLine();
                        string rDescription = Console.ReadLine();
                        if (string.IsNullOrEmpty(rType) || string.IsNullOrEmpty(rDescription))
                        {
                            throw new ArgumentNullException();
                        }

                        bool rProbabilityParse = int.TryParse(Console.ReadLine(), out int rProbability);
                        bool rBusinessImpactParse = int.TryParse(Console.ReadLine(), out int rBusinessImpact);
                        if (rProbability > 10 || rProbability < 1 ||
                           rBusinessImpact > 10 || rBusinessImpact < 1)
                            throw new ArgumentOutOfRangeException();
                        string rOccurenceData = Console.ReadLine();
                        if (string.IsNullOrEmpty(rOccurenceData))
                            throw new ArgumentNullException();

                        DateTime? rOccurenceDataTime = null;
                        if (DateTime.TryParseExact(rOccurenceData, "dd.MM.yyyy", null, System.Globalization.DateTimeStyles.None, out DateTime result))
                        {
                            if (rOccurenceDataTime >= DateTime.Now)
                            {
                                throw new DataException("Дата должна быть меньше текущей даты.");
                            }
                        }


                        RiskInfo riskInfo = new(
                            rType, rDescription, rProbability, rBusinessImpact, rOccurenceDataTime
                        );

                        await riskService.CreateRiskAsync(riskInfo);
                        break;

                    case CommandHelper.QueryRiskProfileCommand:
                        string query = Console.ReadLine();

                        IEnumerable<RiskProfileDto> riskProfileInfos = await riskProfileService.QueryAsync(query);

                        foreach (var item in riskProfileInfos)
                        {
                            Console.WriteLine(item.ToString());
                        }
                        break;
                    case CommandHelper.HelpCommand:
                        Console.WriteLine(CommandHelper.InputSymbol + CommandHelper.CreateRiskProfileCommand + " - " + CommandHelper.CreateRiskProfileDescription);
                        Console.WriteLine(CommandHelper.InputSymbol + CommandHelper.CreateRiskCommand + " - " + CommandHelper.CreateRiskDescription);
                        Console.WriteLine(CommandHelper.InputSymbol + CommandHelper.DeleteRiskProfileCommand + " - " + CommandHelper.DeleteRiskProfileDescription);
                        Console.WriteLine(CommandHelper.InputSymbol + CommandHelper.QueryRiskProfileCommand + " - " + CommandHelper.QueryRiskProfileDescription);
                        break;
                    case CommandHelper.ExitCommand:
                        break;
                    default:
                        Console.ForegroundColor = ConsoleColor.Red;
                        Console.WriteLine(CommandHelper.UknownCommandMessage);
                        break;
                }

            }
            catch (Exception ex)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine(CommandHelper.InputSymbol + ex.Message);
            }
        }
    }
}

file static class CommandHelper
{
    public const string InputSymbol = "> ";
    public const string ExitCommand = "exit";
    public const string HelpCommand = "help";
    public const string CreateRiskCommand = "create_risk";
    public const string CreateRiskDescription = "Creates Risk.";
    public const string CreateRiskProfileCommand = "create_profile";
    public const string CreateRiskProfileDescription = "Creates Risk Profile.";
    public const string UpdateRiskProfileCommand = "update_profile";
    public const string UpdateRiskProfileDescription = "Updates Risk Profile";
    public const string DeleteRiskProfileCommand = "delete_profile";
    public const string DeleteRiskProfileDescription = "Deletes Risk Profile";
    public const string QueryRiskProfileCommand = "search_profile";
    public const string QueryRiskProfileDescription = "Searches a Risk Profile";
    public const string UknownCommandMessage = "Unknown command, use help to see list of available commands.";
}


/*
   static async Task Main()
{
    using IHost host = CreateHostBuilder(args).Build();

    var controller = host.Services.GetRequiredService<RiskProfileController>();
    controller.ProcessUser();

    await host.RunAsync();
}

public static IHostBuilder CreateHostBuilder(string[] args) =>
Host.CreateDefaultBuilder(args)
    .ConfigureServices((_, services) =>
    {
        services.AddSingleton<IRiskProfileService, RiskProfileService>();
        services.AddTransient<RiskProfileController>();
    });
}
/*
HostApplicationBuilder builder = Host.CreateApplicationBuilder();
builder.Services.AddAutoMapper(options =>
{
options.AddProfile<RiskProfileInfoProfile>();
});

builder.Services.AddDbContext<ErmDbContext>(options
=> options.UseNpgsql(builder.Configuration.GetConnectionString("DefaultConnection")));

builder.Services.AddStackExchangeRedisCache(options
=> options.Configuration = builder.Configuration.GetConnectionString("RedisConnection"));

builder.Services.AddScoped<RiskProfileRepository>();
builder.Services.AddScoped<RiskProfileRepositoryProxy>();
builder.Services.AddScoped<RiskRepository>();
builder.Services.AddScoped<RiskRepositoryProxy>();
builder.Services.AddScoped<IRiskProfileService, RiskProfileService>();

builder.Services.AddScoped<IValidator<RiskProfileInfo>, RiskProfileInfoValidator>();
*/



