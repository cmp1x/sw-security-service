namespace SW.SecurityService.CredentialRepository.Repository
{
    using Dapper;
    using SW.SecurityService.CredentialRepository.Models;
    using System.Data;
    using System.Data.SqlClient;
    using System.Linq;

    public class CredentialRepository : ICredentialRepository
    {
        private readonly string connectionString = null;

        public CredentialRepository(string connectionString)
        {
            this.connectionString = connectionString;
        }

        public CredentialsDb GetCredential(string user)
        {
            using (IDbConnection db = new SqlConnection(this.connectionString))
            {
                return db
                    .Query<CredentialsDb>(
                        DapperQueriesStrings.GetCredential,
                        new { User = user })
                    .FirstOrDefault();
            }
        }

        public void Create(CredentialsDb credential)
        {
            using (IDbConnection db = new SqlConnection(this.connectionString))
            {
                db.Execute(DapperQueriesStrings.CreateCredential, credential);
            }
        }
    }
}
