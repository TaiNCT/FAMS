
using Microsoft.AspNetCore.SignalR;
using Microsoft.Identity.Client;
using StudentInfoManagementAPI.Models;

namespace StudentInfoManagementAPI.Hubs;


public class Signal : Hub
{
    public async Task RequestNotification(Credential data)
    {

        // Context.ConnectionId; // Unique ID of each connection

        // Step 1 : Verify that this JWT is legit

        // Step 2 : Add the user to a group

        string group_name = "USERNAME:UUID";

        await Groups.AddToGroupAsync(Context.ConnectionId, group_name);

        // Step 3 : Add a callback so it can be invoked later whenever there is a deletion or role degradation


        // Note : this is how to send a message in a group
        await Clients.Group(group_name).SendAsync("ReceiveMessage", "whatever", "args", "here");
    }
}
