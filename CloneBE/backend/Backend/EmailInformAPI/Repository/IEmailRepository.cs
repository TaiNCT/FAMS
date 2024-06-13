using EmailInformAPI.DTO;

namespace EmailInformAPI.Repository
{
    public interface IEmailRepository
    {
        public IEnumerable<UserGetDTO> GetUser(string name);
        public bool SendEmail( string recipient, string subject, string firstmsg, string lastmsg, string title, Score scores, string email, string emailtoken, ExtraOption ?options);
    }
}
