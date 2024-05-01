namespace Erm.BusinessLayer;

public readonly record struct RiskProfileDto(
    string Name,
    string Description,
    int BusinessProcessId,
    int OccurrenceProbability,
    int PotentialBusinessImpact,
    string PotentialSolution
);