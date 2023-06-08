namespace ApplicationCore.Contracts.Service
{
    public interface IUserService
    {
        string GetEmail();
        string GetUserId();
        string GetUsername();
        IList<string> GetUserRoles();
    }
}
