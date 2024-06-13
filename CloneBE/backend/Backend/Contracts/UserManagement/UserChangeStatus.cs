namespace Contracts.UserManagement;

public class UserChangeStatus
{
    public string Username { get; set; } = string.Empty;
    public bool Status { get; set; }
}