using System.Threading.Tasks;
using Microsoft.Data.SqlClient;

namespace Mercuryiot.Web.Repositories
{
    public interface IDataAccess
    {
        Task<SqlConnection> GetSensorMonitoringSqlConnectionAsync();
    }
}