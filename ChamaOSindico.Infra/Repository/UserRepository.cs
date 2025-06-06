﻿using ChamaOSindico.Domain.Entities;
using ChamaOSindico.Domain.Interfaces;
using ChamaOSindico.Infra.Context;
using MongoDB.Driver;

namespace ChamaOSindico.Infra.Repository
{
    public class UserRepository : IUserRepository
    {
        private readonly IMongoCollection<User> _users;

        public UserRepository(MongoAppDbContext context)
        {
            _users = context.GetCollection<User>();
        }

        public async Task<User?> GetUserByEmailAsync(string email)
        {
            var filter = Builders<User>.Filter.Eq(u => u.Email, email);
            return await _users.Find(filter).FirstOrDefaultAsync();
        }

        public async Task<User?> GetUserByIdAsync(string id)
        {
            var filter = Builders<User>.Filter.Eq(u => u.Id, id);
            return await _users.Find(filter).FirstOrDefaultAsync();
        }

        public async Task CreateUserAsync(User user)
        {
            await _users.InsertOneAsync(user);
        }

        public Task<string> GetUserHashedPassword(string id)
        {
            var filter = Builders<User>.Filter.Eq(u => u.Id, id);
            var projection = Builders<User>.Projection.Expression(u => u.PasswordHash);

            return _users
                .Find(filter)
                .Project(projection)
                .FirstOrDefaultAsync();
        }

        public async Task DeleteUserAsync(string userId)
        {
            await _users.DeleteOneAsync(u => u.Id == userId);
        }
    }
}
