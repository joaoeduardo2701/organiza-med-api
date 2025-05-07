namespace OrganizaMed.Dominio.ModuloMedico;

public interface IRepositorioMedico
{
    Task<Guid> InserirAsync(Medico novaEntidade);
    Task<Guid> EditarAsync(Medico entidadeAtualizada);
    Task<Guid> ExcluirAsync(Medico entidadeParaRemover);
    Task<List<Medico>> SelecionarTodosAsync();
    Task<Medico?> SelecionarPorIdAsync(Guid id);
    Task<List<Medico>> SelecionarMuitosPorId(IEnumerable<Guid> requestMedicos);
}
