using System;

namespace el.api.locathales.Infrastructure.DbModels
{
    public class OperadorDbModel
    {
        public Guid Id { get; set; }
        public string Nome { get; set; }
        public string Matricula { get; set; }
        public string Email { get; set; }
    }
}
