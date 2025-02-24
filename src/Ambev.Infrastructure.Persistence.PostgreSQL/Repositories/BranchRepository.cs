﻿using Ambev.Core.Domain.Interfaces;
using Ambev.Infrastructure.Persistence.PostgreSQL.Context;
using Microsoft.EntityFrameworkCore;
using Ambev.Core.Domain.Common;
using System.Linq;
using Ambev.Core.Domain.Entities;
using System.Linq.Dynamic.Core;
using Ambev.Infrastructure.Persistence.PostgreSQL.Extensions;

namespace Ambev.Infrastructure.Persistence.PostgreSQL.Repositories;

public class BranchRepository : BaseRepository<Branch>, IBranchRepository
{
    private readonly AppDbContext _context;
    public BranchRepository(AppDbContext context) : base(context)
    {
        _context = context;
    }

    public async Task<List<string>> GetProductCategoriesAsync(CancellationToken cancellationToken)
    {
        return await _context.Products
            .Select(p => p.Category)
            .Distinct()
            .ToListAsync(cancellationToken);
    }

    public async Task<PaginatedResult<Branch>> GetBranchPagination(PaginationQuery paginationQuery, CancellationToken cancellationToken)
    {
        var query = _context.Branches.Where(x => true);
        // Aplicar filtros dinâmicos
        query = query.ApplyFilters(paginationQuery.Filter);

        paginationQuery.Order = paginationQuery.Order ?? "id asc";
        query = query.OrderBy(paginationQuery.Order);

        var totalCount = await query.CountAsync(cancellationToken); // Total de itens sem paginação
        var items = await query
            .Skip(paginationQuery.Skip)
            .Take(paginationQuery.Size)
            .ToListAsync(cancellationToken);

        return new PaginatedResult<Branch>
        {
            Data = items,
            TotalItems = totalCount,
            CurrentPage = paginationQuery.Page
        };
    }
}
