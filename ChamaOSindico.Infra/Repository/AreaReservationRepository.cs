﻿using ChamaOSindico.Domain.Entities;
using ChamaOSindico.Domain.Interfaces;
using ChamaOSindico.Infra.Context;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChamaOSindico.Infra.Repository
{
    public class AreaReservationRepository : IAreaReservationRepository
    {
        private readonly IMongoCollection<AreaReservation> _context;

        public AreaReservationRepository(MongoAppDbContext context)
        {
            _context = context.GetCollection<AreaReservation>();
        }

        public async Task DeleteAreaReservationAsync(string id)
        {
            await _context.DeleteOneAsync(ar => ar.Id == id);
        }

        public async Task<IEnumerable<AreaReservation>> GetAllAreaReservationsAsync()
        => await _context.Find(_ => true).ToListAsync();

        public async Task<IEnumerable<AreaReservation>> GetAllAreaReservationsByUserAsync(string residentId)
        => await _context.Find(ar => ar.ResidentId == residentId).ToListAsync();

        public async Task<AreaReservation> GetAreaReservationByIdAsync(string id)
        => await _context.Find(ar => ar.Id == id).FirstOrDefaultAsync();

        public async Task SaveAreaReservationAsync(AreaReservation areaReservation)
        {
            if (string.IsNullOrEmpty(areaReservation.Id))
            {
                await _context.InsertOneAsync(areaReservation);
            }
            else
            {
                await _context.ReplaceOneAsync(ar => ar.Id == areaReservation.Id, areaReservation);
            }
        }
    }
}
