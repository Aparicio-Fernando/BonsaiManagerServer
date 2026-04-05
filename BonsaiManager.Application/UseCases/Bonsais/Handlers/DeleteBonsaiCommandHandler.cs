using BonsaiManager.Application.UseCases.Bonsais.Commands;
using BonsaiManager.Data.Context;
using BonsaiManager.Shared;
using BonsaiManager.Shared.Common;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace BonsaiManager.Application.UseCases.Bonsais.Handlers;

public class DeleteBonsaiCommandHandler : IRequestHandler<DeleteBonsaiCommand, ApiResponse<bool>>
{
    private readonly AppDbContext _context;

    public DeleteBonsaiCommandHandler(AppDbContext context)
    {
        _context = context;
    }

    public async Task<ApiResponse<bool>> Handle(DeleteBonsaiCommand request, CancellationToken cancellationToken)
    {
        var bonsai = await _context.Bonsais
            .FirstOrDefaultAsync(b => b.Id == request.Id && b.UserId == request.UserId, cancellationToken);

        if (bonsai is null)
            return ApiResponse<bool>.Fail("Bonsai no encontrado.");

        _context.Bonsais.Remove(bonsai);
        await _context.SaveChangesAsync(cancellationToken);

        return ApiResponse<bool>.Ok(true, "Bonsai eliminado correctamente.");
    }
}