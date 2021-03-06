using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ControleMedicamentos.Dominio.ModuloMedicamento
{
    public class ValidadorMedicamento : AbstractValidator<Medicamento>
    {
        public ValidadorMedicamento()
        {
            RuleFor(x => x.Nome)
                  .NotNull().NotEmpty().MinimumLength(6);
            RuleFor(x => x.Descricao)
                .NotNull().NotEmpty().MinimumLength(10);
            RuleFor(x => x.Lote)
                .NotNull().NotEmpty().MinimumLength(5);
            RuleFor(x => x.Validade)
                .NotNull().NotEmpty();
            RuleFor(x => x.Fornecedor)
                .NotNull().NotEmpty();
        }
    }
    
}
