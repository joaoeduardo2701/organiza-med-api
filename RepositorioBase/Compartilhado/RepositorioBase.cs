using Microsoft.EntityFrameworkCore;
using OrganizaMed.Dominio.Compartilhado;

namespace OrganizaMed.Infraestrutura.Orm.Compartilhado;

public class RepositorioBase<TEntidade> where TEntidade : EntidadeBase
{
    protected IContextoPersistencia context;
    protected DbSet<TEntidade> registros;

    public RepositorioBase(IContextoPersistencia context)
    {
        this.context = context;
        registros = ((DbContext)this.context).Set<TEntidade>();

    }

    public bool Inserir(TEntidade registro)
    {
        registros.AddAsync(registro);
        context.GravarAsync();
        return true;
    }

    public async Task<Guid> InserirAsync(TEntidade registro)
    {
        await registros.AddAsync(registro);

        return registro.Id;
    }

    public async Task<bool> EditarAsync(TEntidade registro)
    {
        var rastreador = registros.Update(registro);

        return await Task.Run(() => rastreador.State == EntityState.Modified);
    }

    public async Task<bool> ExcluirAsync(TEntidade registro)
    {
        var rastreador = registros.Remove(registro);
        
        return await Task.Run(() => rastreador.State == EntityState.Deleted);
    }

    public virtual TEntidade SelecionarPorId(Guid id)
    {
        return registros.SingleOrDefault(x => x.Id == id);
    }

    public async virtual Task<List<TEntidade>> SelecionarTodosAsync()
    {
        return await registros.ToListAsync();
    }

    public async virtual Task<TEntidade?> SelecionarPorIdAsync(Guid id)
    {
        return await registros.SingleOrDefaultAsync(x => x.Id == id);
    }
}
