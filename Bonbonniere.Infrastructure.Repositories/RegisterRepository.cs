using Bonbonniere.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Bonbonniere.Infrastructure.Repositories
{
    public class RegisterRepository : IRegisterRepository
    {
        private static List<Register> _registers = new List<Register>();

        public Register Get(int id)
        {
            return _registers.SingleOrDefault(r => r.Id == id);
        }

        public void Save(Register model)
        {
            model.Id = new Random().Next(1, 25000);
            _registers.Add(model);
        }
    }
}
