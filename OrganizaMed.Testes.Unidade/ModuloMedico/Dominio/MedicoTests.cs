using FluentValidation.TestHelper;
using OrganizaMed.Dominio.ModuloMedico;

namespace OrganizaMed.Testes.Unidade.ModuloMedico.Dominio;

[TestClass]
[TestCategory("Testes de Unidade")]
public class MedicoTests
{
    private ValidadorMedico _validator;

    [TestInitialize]
    public void Inicializar()
    {
        _validator = new ValidadorMedico();
    }

    [TestMethod]
    public void Deve_Passar_Quando_Nome_E_Crm_Sao_Validos()
    {
        var medico = new Medico("Teste da Silva", "57892-SC");

        var result = _validator.TestValidate(medico);

        result.ShouldNotHaveAnyValidationErrors();
    }

    [TestMethod]
    public void Deve_Falhar_Quando_Nome_Estiver_Vazio()
    {
        var medico = new Medico("", "12345-SC");

        var result = _validator.TestValidate(medico);

        result.ShouldHaveValidationErrorFor(m => m.Nome)
            .WithErrorMessage("O campo Nome é obrigatório");
    }

    [TestMethod]
    public void Deve_Falhar_Quando_Nome_Menor_Que_Tres_Caracteres()
    {
        var medico = new Medico("Aa", "12345-SC");

        var result = _validator.TestValidate(medico);

        result.ShouldHaveValidationErrorFor(m => m.Nome)
            .WithErrorMessage("O campo Nome deve conter no mínimo 3 caracteres");
    }

    [TestMethod]
    public void Deve_Falhar_Quando_Crm_Estiver_Vazio()
    {
        var medico = new Medico("Teste da Silva", "");

        var result = _validator.TestValidate(medico);

        result.ShouldHaveValidationErrorFor(m => m.Crm)
            .WithErrorMessage("O campo Crm é obrigatório");
    }

    [TestMethod]
    public void Deve_Falhar_Quando_Crm_Nao_Estiver_Formato_Correto()
    {
        var medico = new Medico("Teste da Silva", "1234-SC");

        var result = _validator.TestValidate(medico);

        result.ShouldHaveValidationErrorFor(m => m.Crm)
            .WithErrorMessage("O campo Crm deve seguir o formato 00000-UF");
    }

    [TestMethod]
    public void Deve_Falhar_Quando_Crm_Tiver_Letras_Minusculas_No_Estado()
    {
        var medico = new Medico("Teste da Silva", "12345-sc");

        var result = _validator.TestValidate(medico);

        result.ShouldHaveValidationErrorFor(m => m.Crm)
            .WithErrorMessage("O campo Crm deve seguir o formato 00000-UF");
    }
}
