using Irrigation.Core;
using Irrigation.Core.Contracts;
using Irrigation.Core.Models;
using Irrigation.Core.ViewModels;
using Irrigation.Infra.Data;
using Microsoft.EntityFrameworkCore;

namespace Irrigation.Infra.Repositories.Database;

public class AreaRepository(IrrigationDataContext context) : IAreaRepository
{
    public async Task<OperationResult<dynamic>> GetAllAsync(int page, int pageSize)
    {
        var count = await context.Areas.AsNoTracking().CountAsync();
        
        var areas = await context
            .Areas
            .AsNoTracking()
            .Include(x => x.Sensors)
            .Select(area => new AreaViewModel
                (
                    area.Id,
                    area.Description,
                    area.Location,
                    area.Size,
                    area.Sensors.Select(x => x.Id).ToList()
                )
            )
            .Skip(page * pageSize)
            .Take(pageSize)
            .ToListAsync();

        if (areas.Count == 0)
            return OperationResult<dynamic>.FailureResult("Nenhuma área cadastrada!");
        
        return OperationResult<dynamic>.SuccessResult(new
        {
            total = count,
            page,
            pageSize,
            areas
        });
    }
    
    public async Task<OperationResult<AreaViewModel>> GetByIdAsync(int id)
    {
        var area = await context
            .Areas
            .AsNoTracking()
            .Where(x => x.Id == id)
            .Include(y => y.Sensors)
            .Select(area => new AreaViewModel
                (
                    area.Id,
                    area.Description,
                    area.Location,
                    area.Size,
                    area.Sensors.Select(x => x.Id).ToList()
                )
            )
            .FirstOrDefaultAsync();
        
        if (area is null)
            return OperationResult<AreaViewModel>.FailureResult($"Área '{id}' não encontrada!");
        
        return OperationResult<AreaViewModel>.SuccessResult(area);
    }
    
    public async Task<OperationResult<int>> InsertAsync(AreaViewModel model)
    {
        var area = new Area
        {
            Description = model.Description,
            Location = model.Location,
            Size = model.Size,
            Sensors = await context.Sensors.Where(x => model.SensorsId.Contains(x.Id)).ToListAsync()
        };
        
        context.Areas.Add(area);
        var id = await context.SaveChangesAsync();
        
        return id > 0 ? OperationResult<int>.SuccessResult(id) : OperationResult<int>.FailureResult("Não foi possível inserir a área!");
    }
    
    public async Task<OperationResult> UpdateAsync(AreaViewModel model)
    {
        var area = await context.Areas.Where(x => x.Id == model.Id).FirstOrDefaultAsync();
        area.Description = model.Description;
        area.Location = model.Location;
        area.Size = model.Size;
        area.Sensors = await context.Sensors.Where(x => model.SensorsId.Contains(x.Id)).ToListAsync();
        
        context.Areas.Update(area);
        var rowsAffected = await context.SaveChangesAsync();
        
        return rowsAffected > 0 ? OperationResult.SuccessResult() : OperationResult.FailureResult("Não foi possível alterar a área!");
    }

    public async Task<OperationResult> DeleteAsync(int id)
    {
        var area = await context.Areas.Where(x => x.Id == id).FirstOrDefaultAsync();
        
        context.Areas.Remove(area);
        var rowsAffected = await context.SaveChangesAsync();
        
        return rowsAffected > 0 ? OperationResult.SuccessResult() : OperationResult.FailureResult("Não foi possível excluir a área!");
    }
}