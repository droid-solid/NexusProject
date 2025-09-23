using Microsoft.Extensions.Options;
using ResolveIQ.WebUI.Config;
using ResolveIQ.WebUI.Services.Auth;
using System.DirectoryServices.Protocols;
using System.Net;

namespace ResolveIQ.WebUI.Implementations
{
    public class LDAPAuthenticationService(IOptions<LDAPConfig> ldapConfig) : IAuthenticationService
    {
        private readonly LDAPConfig _config = ldapConfig.Value;
        public bool Login(string username, string password)
        {
            string ldapHost = "ldap://localhost";
            int ldapPort = 389; // Or 636 for LDAPS
            LdapConnection ldapConn = new LdapConnection(new LdapDirectoryIdentifier(ldapHost, ldapPort));
            ldapConn.AuthType = AuthType.Basic; // Or other appropriate authentication type
            ldapConn.Credential = new NetworkCredential("cn=read-only-admin,dc=example,dc=com", "password"); // Replace with your admin DN and password
            //ldapConn. = false; // Set to true for LDAPS
            ldapConn.Bind();
            return true;
        }
    }
}
