using Dapper;
using MOCA.Core.Interfaces.Base;
using MOCA.Presistence.Contexts;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MOCA.Presistence.Repositories.Base
{
    public class GenericRepositoryAsync_Write : IGenericRepositoryAsync_Write
    {
        private readonly IApplicationDbContext _context;
        public GenericRepositoryAsync_Write(IApplicationDbContext context)
        {
            this._context = context;
        }

        public async Task<int> ExecuteAsync(string sql, object param = null, CommandType commandType = CommandType.StoredProcedure, IDbTransaction transaction = null, CancellationToken cancellationToken = default)
        {

            return await _context.Connection.ExecuteAsync(sql, param, transaction, commandType: commandType);
        }

        public async Task<T> ExecuteScalarAsync<T>(string sql, object param = null, CommandType commandType = CommandType.StoredProcedure, IDbTransaction transaction = null, CancellationToken cancellationToken = default)
        {
            return await _context.Connection.ExecuteScalarAsync<T>(sql, param, transaction, commandType: commandType);
        }

        public async Task<IReadOnlyList<T>> QueryAsync<T>(string sql, object param = null, CommandType commandType = CommandType.StoredProcedure, IDbTransaction transaction = null, CancellationToken cancellationToken = default)
        {
            return (await _context.Connection.QueryAsync<T>(sql, param, transaction, commandType: commandType)).AsList();
        }

        public async Task<T> QueryFirstOrDefaultAsync<T>(string sql, object param = null, CommandType commandType = CommandType.StoredProcedure, IDbTransaction transaction = null, CancellationToken cancellationToken = default)
        {
            return await _context.Connection.QueryFirstOrDefaultAsync<T>(sql, param, transaction, commandType: commandType);
        }

        public async Task<T> QuerySingleAsync<T>(string sql, object param = null, CommandType commandType = CommandType.StoredProcedure, IDbTransaction transaction = null, CancellationToken cancellationToken = default)
        {
            return await _context.Connection.QuerySingleOrDefaultAsync<T>(sql, param, transaction, commandType: commandType);
        }
    }
}
