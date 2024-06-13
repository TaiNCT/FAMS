using Entities.Context;
using Microsoft.EntityFrameworkCore;

namespace APIGateway.Utils;

public class ProcessUserPermission
{
    public static bool IsAllowCrossApplication(FamsContext context,
        string id,
        string actionName,
        string fullAccessName,
        string accessDeniedName)
    {
        // Retrieve user permission by ID
        var userPermission = context.UserPermissions.FirstOrDefault(x => x.UserPermissionId == id);

        // Check exist user permission
        if (userPermission is null) return false;

        // Is Full Access
        if (userPermission.Name.ToUpper().Equals(fullAccessName)) return true;

        // Is Access Denied
        if (userPermission.Name.ToUpper().Equals(accessDeniedName)) return false;

        // Other permission name match action name
        return userPermission.Name.ToUpper().Equals(actionName.ToUpper());
    }
}