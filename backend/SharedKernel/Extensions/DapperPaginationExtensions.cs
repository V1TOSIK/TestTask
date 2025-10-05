using Dapper;
using SharedKernel.Pagination;
using System.Data;

namespace SharedKernel.Extensions
{
    public static class DapperPaginationExtensions
    {
        public static async Task<PaginationResponse<T>> QueryPagedAsync<T>(
            this IDbConnection connection,
            string baseSql,
            object parameters,
            int page,
            int pageSize,
            string countSql = null)
        {
            if (page <= 0) page = 1;
            if (pageSize <= 0) pageSize = 10;

            int offset = (page - 1) * pageSize;

            var pagedSql = $"{baseSql} LIMIT @PageSize OFFSET @Offset";

            var dynamicParams = new DynamicParameters(parameters);
            dynamicParams.Add("PageSize", pageSize);
            dynamicParams.Add("Offset", offset);

            var items = await connection.QueryAsync<T>(pagedSql, dynamicParams);

            var total = await connection.ExecuteScalarAsync<int>(
                countSql ?? $"SELECT COUNT(*) FROM ({baseSql}) AS CountQuery", parameters);

            return new PaginationResponse<T>(items, total, page, pageSize);
        }
    }
}
