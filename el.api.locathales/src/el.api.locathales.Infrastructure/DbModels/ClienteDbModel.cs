using System;

namespace el.api.locathales.Infrastructure.DbModels
{
    public class ClienteDbModel
    {
        public Guid Id { get; set; }
        public string Nome { get; set; }
        public string Cpf { get; set; }
        public string Email { get; set; }
        public DateTime DataNascimento { get; set; }
        public string Senha { get; set; }
        public string Status { get; set; }
    }
}
