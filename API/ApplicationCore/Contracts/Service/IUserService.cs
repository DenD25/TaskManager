namespace ApplicationCore.Contracts.Service
{
    public interface IUserService
    {
        string GetEmail();
        string GetUserId();
        IList<string> GetUserRoles();
    }
}
