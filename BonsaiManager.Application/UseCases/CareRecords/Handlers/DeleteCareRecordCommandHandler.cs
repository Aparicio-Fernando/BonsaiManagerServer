using BonsaiManager.Application.UseCases.CareRecords.Commands;
using BonsaiManager.Data.Context;
using BonsaiManager.Shared;
using BonsaiManager.Shared.Common;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace BonsaiManager.Application.UseCases.CareRecords.Handlers;

public class DeleteCareRecordCommandHandler : IRequestHandler<DeleteCareRecordCommand, ApiResponse<bool>>
{
    private readonly AppDbContext _context;

    public DeleteCareRecordCommandHandler(AppDbContext context)
    {
        _context = context;
    }

    public async Task<ApiResponse<bool>> Handle(DeleteCareRecordCommand request, CancellationToken cancellationToken)
    {
        var record = await _context.CareRecords
            .Include(c => c.Bonsai)
            .FirstOrDefaultAsync(c => c.Id == request.Id, cancellationToken);

        if (record is null)
            return ApiResponse<bool>.Fail("Registro de cuidado no encontrado.");

        if (record.Bonsai.UserId != request.UserId)
            return ApiResponse<bool>.Fail("No tenés permiso para eliminar este registro.");

        _context.CareRecords.Remove(record);
        await _context.SaveChangesAsync(cancellationToken);

        return ApiResponse<bool>.Ok(true, "Registro de cuidado eliminado correctamente.");
    }
}