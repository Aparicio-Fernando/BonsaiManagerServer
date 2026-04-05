using BonsaiManager.Application.Interfaces;
using BonsaiManager.Application.UseCases.Bonsais.Queries;
using BonsaiManager.Data.Context;
using BonsaiManager.DTOs.Bonsais.Responses;
using BonsaiManager.Shared;
using BonsaiManager.Shared.Common;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace BonsaiManager.Application.UseCases.Bonsais.Handlers;

public class GetBonsaisByUserQueryHandler : IRequestHandler<GetBonsaisByUserQuery, ApiResponse<PaginatedResponse<BonsaiResponse>>>
{
    private readonly AppDbContext _context;
    private readonly IHttpContextService _httpContextService;

    public GetBonsaisByUserQueryHandler(AppDbContext context, IHttpContextService httpContextService)
    {
        _context = context;
        _httpContextService = httpContextService;
    }

    public async Task<ApiResponse<PaginatedResponse<BonsaiResponse>>> Handle(GetBonsaisByUserQuery request, CancellationToken cancellationToken)
    {
        var userId = _httpContextService.GetCurrentUserId();

        var query = _context.Bonsais
            .AsNoTracking()
            .Where(b => b.UserId == userId);

        if (!string.IsNullOrWhiteSpace(request.Pagination.SearchTerm))
            query = query.Where(b => b.Name.Contains(request.Pagination.SearchTerm));

        var totalCount = await query.CountAsync(cancellationToken);

        var items = await query
            .Include(b => b.Species)
            .OrderBy(b => b.Name)
            .Skip((request.Pagination.Page - 1) * request.Pagination.PageSize)
            .Take(request.Pagination.PageSize)
            .Select(b => new BonsaiResponse
            {
                Id = b.Id,
                Name = b.Name,
                Style = b.Style,
                AcquisitionDate = b.AcquisitionDate,
                Notes = b.Notes,
                ImageUrl = b.ImageUrl,
                SpeciesId = b.SpeciesId,
                SpeciesName = b.Species.Name,
                UserId = b.UserId
            })
            .ToListAsync(cancellationToken);

        var paginated = new PaginatedResponse<BonsaiResponse>
        {
            Items = items,
            TotalCount = totalCount,
            Page = request.Pagination.Page,
            PageSize = request.Pagination.PageSize
        };

        return ApiResponse<PaginatedResponse<BonsaiResponse>>.Ok(paginated);
    }
}