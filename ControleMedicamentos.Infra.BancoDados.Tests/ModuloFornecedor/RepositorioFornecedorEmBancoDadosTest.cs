using ControleMedicamentos.Dominio.ModuloFornecedor;
using ControleMedicamentos.Infra.BancoDados.ModuloFornecedor;
using FluentValidation.Results;
using Microsoft.VisualStudio.TestTools.UnitTesting;


namespace ControleMedicamentos.Infra.BancoDados.Tests.ModuloFornecedor
{
    [TestClass]
    public class RepositorioFornecedorEmBancoDadosTest
    {
        [TestMethod]
        public void Deve_inserir_fornecedor()
        {

            RepositorioFornecedorEmBancoDados repositorioPaciente = new();

            Fornecedor fornecedor = new("visxo", "888888888", "visco@gmail.com", "lages", "SC");

            RepositorioFornecedorEmBancoDados _repositorioForn = new();

            ValidationResult vr = _repositorioForn.Inserir(fornecedor);

            Assert.IsTrue(vr.IsValid);

        }
    }
}
