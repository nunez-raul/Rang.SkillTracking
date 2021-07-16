namespace Rang.SkillTracking.Domain.Common
{
    public enum OperationStatusCode
    {
        NotSet,
        Success,
        MissingTrackingPoint,
        MissingSkillGoal
    }

    public enum SkillLevel
    {
        NotSet,
        Noob,
        Average,
        Advanced,
        Expert,
        Guru
    }

    public enum ModelValidationStatusCode
    {
        RequiredInformationMissing,
        CapacityExceeded,
        InvalidDataSupplied,
        InternalMemberFailedValidation
    }
}
