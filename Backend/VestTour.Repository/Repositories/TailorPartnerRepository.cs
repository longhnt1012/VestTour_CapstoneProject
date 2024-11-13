using AutoMapper;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VestTour.Domain.Entities;
using VestTour.Repository.Data;
using VestTour.Repository.Interfaces;
using VestTour.Repository.Models;

namespace VestTour.Repository.Repositories
{
    public class TailorPartnerRepository : ITailorPartnerRepository
    {
        private readonly VestTourDbContext _context;
        private readonly IMapper _mapper;

        public TailorPartnerRepository(VestTourDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<List<TailorPartnerModel>> GetAllTailorPartnersAsync()
        {
            var tailorPartners = await _context.TailorPartners.ToListAsync();
            return _mapper.Map<List<TailorPartnerModel>>(tailorPartners);
        }

        public async Task<TailorPartnerModel?> GetTailorPartnerByIdAsync(int tailorPartnerId)
        {
            var tailorPartner = await _context.TailorPartners.FindAsync(tailorPartnerId);
            return _mapper.Map<TailorPartnerModel>(tailorPartner);
        }

        public async Task<int> AddTailorPartnerAsync(TailorPartnerModel tailorPartner)
        {
            var newTailorPartner = _mapper.Map<TailorPartner>(tailorPartner);
            _context.TailorPartners.Add(newTailorPartner);
            await _context.SaveChangesAsync();
            return newTailorPartner.TailorPartnerId;
        }

        public async Task UpdateTailorPartnerAsync(int id, TailorPartnerModel tailorPartner)
        {
            var updatedTailorPartner = _mapper.Map<TailorPartner>(tailorPartner);
            _context.TailorPartners.Update(updatedTailorPartner);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteTailorPartnerAsync(int tailorPartnerId)
        {
            var tailorPartner = await _context.TailorPartners.FindAsync(tailorPartnerId);
            if (tailorPartner != null)
            {
                _context.TailorPartners.Remove(tailorPartner);
                await _context.SaveChangesAsync();
            }
        }
    }
    }
