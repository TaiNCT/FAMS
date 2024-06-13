using Firebase.Auth;
using Firebase.Auth.Providers;

namespace TrainingProgramManagementAPI.Services;

public class FirebaseAuthProviderCustom : FirebaseAuthProvider
{

    public FirebaseAuthProviderCustom(FirebaseAuthConfig config)
    {

    }

    public override FirebaseProviderType ProviderType => throw new NotImplementedException();



    protected override Task<UserCredential> LinkWithCredentialAsync(string idToken, AuthCredential credential)
    {
        throw new NotImplementedException();
    }

    protected override Task<UserCredential> SignInWithCredentialAsync(AuthCredential credential)
    {
        throw new NotImplementedException();
    }
}