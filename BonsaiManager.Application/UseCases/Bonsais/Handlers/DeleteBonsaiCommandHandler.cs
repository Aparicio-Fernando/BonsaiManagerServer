using BonsaiManager.Application.Interfaces;
using BonsaiManager.Application.UseCases.Bonsais.Commands;
using BonsaiManager.Data.Context;
using BonsaiManager.Shared.Common;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace BonsaiManager.Application.UseCases.Bonsais.Handlers;

public class DeleteBonsaiCommandHandler : IRequestHandler<DeleteBonsaiCommand, ApiResponse<bool>>
{
    private readonly AppDbContext _context;
    private readonly IHttpContextService _httpContextService;

    public DeleteBonsaiCommandHandler(AppDbContext context, IHttpContextService httpContextService)
    {
        _context = context;
        _httpContextService = httpContextService;
    }

    public async Task<ApiResponse<bool>> Handle(DeleteBonsaiCommand request, CancellationToken cancellationToken)
    {
        var userId = _httpContextService.GetCurrentUserId();

        var bonsai = await _context.Bonsais
            .FirstOrDefaultAsync(b => b.Id == request.Id && b.UserId == userId, cancellationToken);
        if (bonsai is null)
            return ApiResponse<bool>.Fail("Bonsai no encontrado.");

        _context.Bonsais.Remove(bonsai);
        await _context.SaveChangesAsync(cancellationToken);

        return ApiResponse<bool>.Ok(true, "Bonsai eliminado correctamente.");
    }
}