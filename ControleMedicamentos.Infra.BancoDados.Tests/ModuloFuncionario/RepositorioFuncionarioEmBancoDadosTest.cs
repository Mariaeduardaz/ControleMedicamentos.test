using ControleMedicamentos.Dominio.ModuloFuncionario;
using ControleMedicamentos.Infra.BancoDados.ModuloFuncionario;
using FluentValidation.Results;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ControleMedicamentos.Infra.BancoDados.Tests.ModuloFuncionario
{
    [TestClass]
    public class RepositorioFuncionarioEmBancoDeDadosTest
    {
        Funcionario func;
        RepositorioFuncionarioEmBancoDados _repositorioFuncionario;
        [TestMethod]
        public void Deve_inserir_funcionario()
        {

            RepositorioFuncionarioEmBancoDados repositorioFuncionario = new();

            Funcionario funcionario = new("Gabi", "gabi1034", "657364");

            RepositorioFuncionarioEmBancoDados _repositorioFunc = new();

            ValidationResult vr = _repositorioFunc.Inserir(funcionario);

            Assert.IsTrue(vr.IsValid);

        }
        [TestMethod]
        public void DeveEditarFuncionario()
        {
            _repositorioFuncionario.Inserir(func);

            func.Nome = "Rafa";
            func.Login = "rafinha987";
            func.Senha = "rF";

            _repositorioFuncionario.Editar(func);

            var funcionarioEncontrado = _repositorioFuncionario.SelecionarPorId(func.Numero);

            Assert.IsNotNull(funcionarioEncontrado);

            Assert.AreEqual(func, funcionarioEncontrado);
        }
        [TestMethod]
        public void DeveExcluirFuncionario()
        {
            _repositorioFuncionario.Inserir(func);

            _repositorioFuncionario.Excluir(func);

            var funcionarioEncontrado = _repositorioFuncionario.SelecionarPorId(func.Numero);

            Assert.IsNull(funcionarioEncontrado);
        }
    }
}
