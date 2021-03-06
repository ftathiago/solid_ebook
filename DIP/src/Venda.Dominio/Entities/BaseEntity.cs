using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;

namespace Venda.Dominio.Entities
{
    public abstract class BaseEntity
    {
        public virtual bool Validar()
        {
            return !Validate().Any();
        }

        public abstract IEnumerable<ValidationResult> Validate(ValidationContext validationContext);

        public abstract IEnumerable<ValidationResult> Validate();
    }
}