namespace Rang.SkillTracking.Application.Common
{
    public enum UseCaseResultStatusCode
    {
        NotSet,
        Success,
        InvalidInputModel,
        EvaluateeNotFound,
        EvaluationPeriodNotFound,
        EvaluationPeriodOverlap
    }
}
