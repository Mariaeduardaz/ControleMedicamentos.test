using ControleMedicamentos.Dominio.ModuloRequisicao;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ControleMedicamentos.Infra.BancoDados.ModuloRequisicao
{
    public class ValidadorRequisicao : AbstractValidator<Requisicao>
    {
        public ValidadorRequisicao()
        {
            RuleFor(x => x.Paciente)
                .NotNull();
            RuleFor(x => x.Funcionario)
                .NotNull();
            RuleFor(x => x.Medicamento)
                .NotNull();
            RuleFor(x => x.QtdMedicamento)
                .NotNull();
            RuleFor(x => x.Data)
                .NotNull().NotEmpty();
        }
    }
}
