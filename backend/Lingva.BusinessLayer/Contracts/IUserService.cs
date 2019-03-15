﻿using Lingva.DAL.Entities;
using System.Collections.Generic;

namespace Lingva.BC.Contracts
{
    public interface IUserService
    {
        string GetUserToken(User user, string secret);
        User Authenticate(string username, string password);
        IEnumerable<User> GetAll();
        User GetById(int id);
        User Create(User user, string password);
        void Update(User user, string password = null);
        void Delete(int id);
    }
}
