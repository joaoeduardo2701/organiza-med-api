using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using OrganizaMed.Dominio.ModuloMedico;

namespace OrganizaMed.Infraestrutura.Orm.ModuloMedico;

public class MapeadorMedicoEmOrm : IEntityTypeConfiguration<Medico>
{
    public void Configure(EntityTypeBuilder<Medico> modelBuilder)
    {
        modelBuilder.ToTable("TBMedico");

        modelBuilder.Property(x => x.Id)
            .ValueGeneratedNever();

        modelBuilder.Property(x => x.Nome)
            .HasColumnName("nvarchar(100)")
            .IsRequired();

        modelBuilder.Property(x => x.Crm)
            .HasColumnName("char(8)")
            .IsRequired();
    }
}
