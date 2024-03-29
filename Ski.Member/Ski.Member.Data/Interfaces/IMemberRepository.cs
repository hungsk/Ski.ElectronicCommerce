﻿using Ski.Member.Domain.Entities;

namespace Ski.Member.Data.Interfaces
{
    public interface IMemberRepository
    {
        void CreateAsync(MemberModel member);

        void UpdateAsync(MemberModel member);

        void DeleteAsync(MemberModel member);

        Task<MemberModel> GetAsync(int id);

        Task<IEnumerable<MemberModel>> GetAllAsync();

        Task SaveChangesAsync();

        bool MemberExists(string id);
    }
}