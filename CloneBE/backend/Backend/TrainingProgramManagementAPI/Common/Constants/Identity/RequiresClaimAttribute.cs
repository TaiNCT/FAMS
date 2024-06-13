namespace TrainingProgramManagementAPI.Constants.Identity
{
    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Method, AllowMultiple = true)]
    public class RequiresClaimAttribute : Attribute, IAuthorizationFilter
    {
        private readonly string _claimName;
        private readonly string[] _claimsValue;

        public RequiresClaimAttribute(string claimName, params string[] claimsValue)
        {
            _claimName = claimName;
            _claimsValue = claimsValue;
        }

        public void OnAuthorization(AuthorizationFilterContext context)
        {
            bool isAuthorize = false;
            foreach(var claim in _claimsValue)
            {
                if(context.HttpContext.User.HasClaim(_claimName, claim))
                {
                    isAuthorize = true;
                }
            }

            if(!isAuthorize) context.Result = new ForbidResult();
        }
    }
}