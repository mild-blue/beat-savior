using BildMlue.Application.Extensions;
using BildMlue.Application.Interfaces;
using BildMlue.Domain.Entities;
using FluentValidation;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace BildMlue.Application.Services;

public class CrudService<TEntity, TCreateDto, TUpdateDto, TDetailDto, TListDto>
    where TEntity : AppEntity
    where TCreateDto : class
    where TUpdateDto : class
    where TDetailDto : class
    where TListDto : class
{
    protected readonly IMapper Mapper;
    protected readonly IAppDbContext Context;
    protected readonly IValidator<TCreateDto>? CreateValidator;
    protected readonly IValidator<TUpdateDto>? UpdateValidator;

    public CrudService(IServiceProvider services)
    {
        Mapper = services.GetRequiredService<IMapper>();
        Context = services.GetRequiredService<IAppDbContext>();
        CreateValidator = services.GetService<IValidator<TCreateDto>>();
        UpdateValidator = services.GetService<IValidator<TUpdateDto>>();
    }

    public Task<List<TListDto>> GetAll() =>
        Context.Set<TEntity>().ProjectTo<TListDto>(Mapper).ToListAsync();

    public async Task<TDetailDto> GetDetail(Guid id)
    {
        var entity = await Context.Set<TEntity>().GetDetailOrThrow(id);
        return Mapper.Map<TDetailDto>(entity);
    }

    public async Task<TDetailDto> Create(TCreateDto dto)
    {
        if (CreateValidator is not null)
        {
            await CreateValidator.ValidateAndThrowAsync(dto);
        }

        var entity = Mapper.Map<TEntity>(dto);
        Context.Set<TEntity>().Add(entity);
        await Context.SaveChanges();
        return await GetDetail(entity.Id);
    }

    public async Task<TDetailDto> Update(Guid id, TUpdateDto dto)
    {
        if (UpdateValidator is not null)
        {
            await UpdateValidator.ValidateAndThrowAsync(dto);
        }

        var entity = await Context.Set<TEntity>().GetDetailOrThrow(id);
        Mapper.Map(dto, entity);
        await Context.SaveChanges();
        return await GetDetail(entity.Id);
    }

    public async Task Delete(Guid id)
    {
        var entity = await Context.Set<TEntity>().GetDetailOrThrow(id);
        Context.Set<TEntity>().Remove(entity);
        await Context.SaveChanges();
    }
}