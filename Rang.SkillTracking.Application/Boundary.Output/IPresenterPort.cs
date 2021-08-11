namespace Rang.SkillTracking.Application.Boundary.Output
{
    public interface IPresenterPort
    {
        public void PresentMessage(string message);
        public void PresentErrorMessage(string message);
        public void PresentSuccessOperationMessage(string message);
    }
}
