using Npgsql;

namespace Eclo.DataAccess.Repositories;

public class BaseRepository
{
    protected readonly NpgsqlConnection _connection;

    public BaseRepository()
    {
        Dapper.DefaultTypeMap.MatchNamesWithUnderscores = true;

        //For Docker
         this._connection = new NpgsqlConnection("Host=eclo-database-host; Port=5432; " +
            "Database=eclo-db; User Id=postgres_admin; Password=AAaa@@22;");

        //Fro Server
        /*this._connection = new NpgsqlConnection("Host=db-postgresql-sgp1-54859-do-user-14651493-0.b.db.ondigitalocean.com; Port=25060; " +
            "Database=eclo-db; User Id=doadmin; Password=AVNS_KkiVspFxY5HjtY0VR4K;");*/
        
        //For localhost
        /*this._connection = new NpgsqlConnection("Host=localhost; Port=5432; " +
            "Database=eclo-db; User Id=postgres; Password=12345;");*/
        /*this._connection = new NpgsqlConnection("Host=localhost; Port=5432; " +
            "Database=eclo2-db; User Id=postgres; Password=12345;");*/
    }
}
