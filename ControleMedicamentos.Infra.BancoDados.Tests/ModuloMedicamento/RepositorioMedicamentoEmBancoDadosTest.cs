using ControleMedicamento.Infra.BancoDados.ModuloMedicamento;
using ControleMedicamentos.Dominio.ModuloFornecedor;
using ControleMedicamentos.Dominio.ModuloMedicamento;
using ControleMedicamentos.Infra.BancoDados.ModuloFornecedor;
using FluentValidation.Results;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;

namespace ControleMedicamento.Infra.BancoDados.Tests.ModuloMedicamento
{
    [TestClass]
    public class RepositorioMedicamentoEmBancoDadosTest
    {
        Medicamento med;
        RepositorioMedicamentoEmBancoDados _repositorioMed;
        RepositorioFornecedorEmBancoDados _repositorioForn;
        Fornecedor forn;
        Fornecedor fornEditar;
        [TestMethod]
        public void Deve_inserir_medicamento()
        {
            RepositorioFornecedorEmBancoDados repositorioFornecedor = new();

            Medicamento medicamento = new("nome", "10 caracteres", "Teste", DateTime.MaxValue);

            Fornecedor fornecedor = new Fornecedor("nome", "9999-8888", "duda@gmail.com", "lages", "sc");

            repositorioFornecedor.Inserir(fornecedor);

            medicamento.Fornecedor = fornecedor;

            RepositorioMedicamentoEmBancoDados _repositorioMed = new();

            ValidationResult vr = _repositorioMed.Inserir(medicamento);

            Assert.IsTrue(vr.IsValid);

        }
        [TestMethod]
        public void DeveEditarMedicamento()
        {
            _repositorioForn.Inserir(forn);
            _repositorioForn.Inserir(fornEditar);

            med.Fornecedor = forn;

            _repositorioMed.Inserir(med);

            med.Nome = "Dorflexxxxxx";
            med.Fornecedor = fornEditar;

            _repositorioMed.Editar(med);

            var medicamentoEncontrado = _repositorioMed.SelecionarPorId(med.Numero);

            Assert.IsNotNull(medicamentoEncontrado);

            Assert.AreEqual(med, medicamentoEncontrado);
        }
        [TestMethod]
        public void DeveSelecionarApenasUm()
        {
            _repositorioForn.Inserir(forn);

            med.Fornecedor = forn;

            _repositorioMed.Inserir(med);

            var medicamentoEncontrado = _repositorioMed.SelecionarPorId(med.Numero);

            medicamentoEncontrado.Validade = DateTime.SpecifyKind(medicamentoEncontrado.Validade, DateTimeKind.Local); 

            Assert.IsNotNull(medicamentoEncontrado);

            Assert.AreEqual(med, medicamentoEncontrado);
        }

        [TestMethod]
        public void Deve_Excluir_Medicamento()
        {
            Medicamento medicamento = new("nome", "10 caracteres", "Teste", DateTime.MaxValue);

        }
    }
}