using Irrigation.Core;
using Irrigation.Core.Contracts;
using Irrigation.Core.Models;
using Irrigation.Core.ViewModels;
using Irrigation.Infra.Data;
using Microsoft.EntityFrameworkCore;

namespace Irrigation.Infra.Repositories.Database;

public class ScheduleRepository(IrrigationDataContext context) : IScheduleRepository
{
    public async Task<OperationResult<dynamic>> GetAllAsync(int page, int pageSize)
    {
        var count = await context.Schedules.AsNoTracking().CountAsync();
        
        var schedules = await context
            .Schedules
            .AsNoTracking()
            .Select(schedule => new ScheduleViewModel
                (
                    schedule.Id,
                    schedule.StartTime,
                    schedule.EndTime
                )
            )
            .Skip(page * pageSize)
            .Take(pageSize)
            .ToListAsync();

        if (schedules.Count == 0)
            return OperationResult<dynamic>.FailureResult("Nenhum agendamento cadastrado!");
        
        return OperationResult<dynamic>.SuccessResult(new
        {
            total = count,
            page,
            pageSize,
            schedules
        });
    }
    
    public async Task<OperationResult<ScheduleViewModel>> GetByIdAsync(int id)
    {
        var schedule = await context
            .Schedules
            .AsNoTracking()
            .Where(x => x.Id == id)
            .Select(schedule => new ScheduleViewModel
                (
                    schedule.Id,
                    schedule.StartTime,
                    schedule.EndTime
                )
            )
            .FirstOrDefaultAsync();
        
        if (schedule is null)
            return OperationResult<ScheduleViewModel>.FailureResult($"Agendamento '{id}' não encontrado!");
        
        return OperationResult<ScheduleViewModel>.SuccessResult(schedule);
    }
    
    public async Task<OperationResult<int>> InsertAsync(ScheduleViewModel model)
    {
        var schedule = new Schedule
        {
            StartTime = model.StartDate,
            EndTime = model.EndDate
        };
        
        context.Schedules.Add(schedule);
        var id = await context.SaveChangesAsync();
        
        return id > 0 ? OperationResult<int>.SuccessResult(id) : OperationResult<int>.FailureResult("Não foi possível inserir o agendamento!");
    }
    
    public async Task<OperationResult> UpdateAsync(ScheduleViewModel model)
    {
        var schedule = await context.Schedules.Where(x => x.Id == model.Id).FirstOrDefaultAsync();
        schedule.StartTime = model.StartDate;
        schedule.EndTime = model.EndDate;
        
        context.Schedules.Update(schedule);
        var rowsAffected = await context.SaveChangesAsync();
        
        return rowsAffected > 0 ? OperationResult.SuccessResult() : OperationResult.FailureResult("Não foi possível alterar o agendamento!");
    }

    public async Task<OperationResult> DeleteAsync(int id)
    {
        var schedule = await context.Schedules.Where(x => x.Id == id).FirstOrDefaultAsync();
        
        context.Schedules.Remove(schedule);
        var rowsAffected = await context.SaveChangesAsync();
        
        return rowsAffected > 0 ? OperationResult.SuccessResult() : OperationResult.FailureResult("Não foi possível excluir o agendamento!");
    }
    
}