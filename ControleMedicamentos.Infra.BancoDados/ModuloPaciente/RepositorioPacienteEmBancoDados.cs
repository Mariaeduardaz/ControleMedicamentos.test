using ControleMedicamentos.Dominio.ModuloPaciente;
using FluentValidation.Results;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ControleMedicamentos.Infra.BancoDados.ModuloPaciente
{
    public class RepositorioPacienteEmBancoDados
    {
        private string enderecoBanco =
          @"Data Source=(localdb)\MSSQLLocalDB;Initial Catalog=master;Integrated Security=True;Connect Timeout=30;Encrypt=False;TrustServerCertificate=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False";

        private const string sqlInserir =
          @"INSERT INTO [TBPACIENTE] 
                (
                    [NOME],
                    [TELEFONE]
                    [EMAIL]
                    [CIDADE]
                    [ESTADO]
	            )
	            VALUES
                (
                    @NOME,
                    @TELEFONE
                    @EMAIL
                    @CIDADE
                    @ESTADO
                );SELECT SCOPE_IDENTITY();";

        private const string sqlEditar =
           @"UPDATE [TBPFORNECEDOR]	
		        SET
			        [NOME] = @NOME,
			        [TELEFONE] = @TELEFONE
                    [EMAIL] = @EMAIL
                    [CIDADE] = @CIDADE
                    [ESTADO] = @ESTADO
		        WHERE
			        [ID] = @ID";


        private const string sqlExcluir =
           @"DELETE FROM [TBPFORNECEDOR]			        
		        WHERE
			        [ID] = @ID";

        private const string sqlSelecionarPorId =
          @"SELECT 
		            [ID], 
		           [NOME],
                    [TELEFONE]
                    [EMAIL]
                    [CIDADE]
                    [ESTADO]
	            FROM 
		            [TBPFORNECEDOR]
		        WHERE
                    [ID] = @ID";

        private const string sqlSelecionarTodos =
          @"SELECT 
		            [ID], 
		           [NOME],
                    [TELEFONE]
                    [EMAIL]
                    [CIDADE]
                    [ESTADO]
	            FROM 
		            [TBPFORNECEDOR]";

        public ValidationResult Inserir(Paciente paciente)
        {
            var validador = new ValidadorPaciente();

            var resultadoValidacao = validador.Validate(paciente);

            if (resultadoValidacao.IsValid == false)
                return resultadoValidacao;

            SqlConnection conexaoComBanco = new SqlConnection(enderecoBanco);

            SqlCommand comandoInsercao = new SqlCommand(sqlInserir, conexaoComBanco);

            ConfigurarParametrosPaciente(paciente, comandoInsercao);

            conexaoComBanco.Open();
            var id = comandoInsercao.ExecuteScalar();
            paciente.Id = Convert.ToInt32(id);

            conexaoComBanco.Close();

            return resultadoValidacao;
        }

        public ValidationResult Editar(Paciente paciente)
        {
            var validador = new ValidadorPaciente();

            var resultadoValidacao = validador.Validate(paciente);

            if (resultadoValidacao.IsValid == false)
                return resultadoValidacao;

            SqlConnection conexaoComBanco = new SqlConnection(enderecoBanco);

            SqlCommand comandoEdicao = new SqlCommand(sqlEditar, conexaoComBanco);

            ConfigurarParametrosPaciente(paciente, comandoEdicao);

            conexaoComBanco.Open();
            comandoEdicao.ExecuteNonQuery();
            conexaoComBanco.Close();

            return resultadoValidacao;
        }

        public void Excluir(Paciente paciente)
        {
            SqlConnection conexaoComBanco = new SqlConnection(enderecoBanco);

            SqlCommand comandoExclusao = new SqlCommand(sqlExcluir, conexaoComBanco);

            comandoExclusao.Parameters.AddWithValue("ID", paciente.Id);

            conexaoComBanco.Open();
            comandoExclusao.ExecuteNonQuery();
            conexaoComBanco.Close();
        }

        public Paciente SelecionarPorId(int id)
        {
            SqlConnection conexaoComBanco = new SqlConnection(enderecoBanco);

            SqlCommand comandoSelecao = new SqlCommand(sqlSelecionarPorId, conexaoComBanco);

            comandoSelecao.Parameters.AddWithValue("ID", id);

            conexaoComBanco.Open();
            SqlDataReader leitorPaciente = comandoSelecao.ExecuteReader();

            Paciente paciente = null;
            if (leitorPaciente.Read())
                paciente = ConverterParaPaciente(leitorPaciente);

            conexaoComBanco.Close();

            return paciente;
        }

        public List<Paciente> SelecionarTodos()
        {
            SqlConnection conexaoComBanco = new SqlConnection(enderecoBanco);

            SqlCommand comandoSelecao = new SqlCommand(sqlSelecionarTodos, conexaoComBanco);
            conexaoComBanco.Open();

            SqlDataReader leitorPaciente = comandoSelecao.ExecuteReader();

            List<Paciente> pacientes = new List<Paciente>();

            while (leitorPaciente.Read())
                pacientes.Add(ConverterParaPaciente(leitorPaciente));

            conexaoComBanco.Close();

            return pacientes;
        }

        private void ConfigurarParametrosPaciente(Paciente paciente, SqlCommand comando)
        {
            comando.Parameters.AddWithValue("ID", paciente.Id);
            comando.Parameters.AddWithValue("NOME", paciente.Nome);
            comando.Parameters.AddWithValue("CARTAOSUS", paciente.CartaoSUS);
        }

        private Paciente ConverterParaPaciente(SqlDataReader leitorPaciente)
        {
            int id = Convert.ToInt32(leitorPaciente["ID"]);
            string nome = Convert.ToString(leitorPaciente["NOME"]);
            string cartaoSus = Convert.ToString(leitorPaciente["CARTAOSUS"]);

            return new Paciente()
            {
                Id = id,
                Nome = nome,
                CartaoSUS = cartaoSus
            };
        }
    }
}
