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

    public async Task<bool> InserirAsync(TEntidade registro)
    {
        await registros.AddAsync(registro);
        await context.GravarAsync();
        return true;
    }

    public async Task Editar(TEntidade registro)
    {
        registros.Update(registro);
        await context.GravarAsync();
    }

    public async Task Excluir(TEntidade registro)
    {
        registros.Remove(registro);
        await context.GravarAsync();
    }

    public virtual TEntidade SelecionarPorId(Guid id)
    {
        return registros.SingleOrDefault(x => x.Id == id);
    }

    public async virtual Task<TEntidade> SelecionarPorIdAsync(Guid id)
    {
        return await registros.SingleOrDefaultAsync(x => x.Id == id);
    }

    public async virtual Task<List<TEntidade>> SelecionarTodosAsync()
    {
        return await registros.ToListAsync();
    }
}
