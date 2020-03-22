using System.Collections.Generic;
using System.Linq;

namespace FTStore.Domain.Entity
{
    public abstract class Entity
    {
        private List<string> _mensagensValidacao;
        private List<string> MensagemValidacao
        {
            get
            {
                return _mensagensValidacao ?? (_mensagensValidacao = new List<string>());
            }
        }

        public abstract void Validate();

        protected void LimparMensagensValidacao()
        {
            MensagemValidacao.Clear();
        }

        protected void AdicionarCritica(string mensagem)
        {
            MensagemValidacao.Add(mensagem);
        }

        public string ObterMensagensValidacao()
        {
            return string.Join(". ", MensagemValidacao);
        }

        public bool EhValido
        {
            get
            {
                return !MensagemValidacao.Any();
            }
        }
    }
}