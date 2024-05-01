namespace Erm.BusinessLayer;

public readonly record struct RiskProfileDtoOut(
    int Id,
    string Name,
    string Description,
    int BusinessProcessId,
    int OccurrenceProbability,
    int PotentialBusinessImpact,
    string PotentialSolution,
    int RiskLevel
);