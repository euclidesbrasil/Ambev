﻿using Ambev.Core.Domain.Interfaces;
using Ambev.Infrastructure.Persistence.PostgreSQL.Context;
using Microsoft.EntityFrameworkCore;
using Ambev.Core.Domain.Common;
using System.Linq;
using Ambev.Core.Domain.Entities;

namespace Ambev.Infrastructure.Persistence.PostgreSQL.Repositories;

public class SaleRepository : BaseRepository<Sale>, ISaleRepository
{
    private readonly AppDbContext _context;
    public SaleRepository(AppDbContext context) : base(context)
    {
        _context = context;
    }
    public async Task<PaginatedResult<Sale>> GetSalesPagination(PaginationQuery paginationQuery, CancellationToken cancellationToken)
    {
        var query = _context.Sales.Where(x => true);

        var totalCount = await query.CountAsync(cancellationToken); // Total de itens sem paginação
        var items = await query
            .Skip(paginationQuery.Skip)
            .Take(paginationQuery.Size)
            .ToListAsync(cancellationToken);

        return new PaginatedResult<Sale>
        {
            Data = items,
            TotalItems = totalCount,
            CurrentPage = paginationQuery.Page
        };
    }
}
