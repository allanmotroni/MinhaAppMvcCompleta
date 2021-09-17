using DevIO.Business.Models;
using FluentValidation;
using FluentValidation.Results;

namespace DevIO.Business.Services
{
    public abstract class BaseService
    {
        protected void Notificar(ValidationResult validationResult)
        {
            foreach (var error in validationResult.Errors)
            {
                Notificar(error.ErrorMessage);
            }
        }
        protected void Notificar(string mensagem)
        {

        }

        protected bool ExecutarValidacao<TV, TE>(TV validacao, TE entity) where TV : AbstractValidator<TE> where TE : Entity
        {
            var validator = validacao.Validate(entity);

            if (validator.IsValid) return true;

            Notificar(validator);

            return false;
        }


    }
}
