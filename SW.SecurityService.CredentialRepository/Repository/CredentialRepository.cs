namespace SW.SecurityService.CredentialRepository.Repository
{
    using Dapper;
    using SW.SecurityService.CredentialRepository.Models;
    using System;
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

        public CredentialsDb GetCredential(string userName)
        {
            using (IDbConnection db = new SqlConnection(this.connectionString))
            {
                var targetCredential = db
                    .Query<CredentialsDb>(
                        DapperQueriesStrings.GetCredential,
                        new { UserName = userName })
                    .FirstOrDefault();

                if (targetCredential is null)
                    throw new ApplicationException($"There are no credentials of '{userName}' in the database");

                return targetCredential;
            }
        }

        public void Create(CredentialsDb credential)
        {
            if (credential.UserId is null)
                credential.UserId = Guid.NewGuid().ToString();


            using (IDbConnection db = new SqlConnection(this.connectionString))
            {

                try
                {
                    db.Query<CredentialsDb>(
                        DapperQueriesStrings.GetCredential,
                        new { UserName = credential.UserName })
                        .FirstOrDefault();
                }
                catch (Exception Ex)
                {
                    throw new ApplicationException(Ex.Message);
                }

                db.Execute(DapperQueriesStrings.CreateCredential, credential);
            }
        }
    }
}
