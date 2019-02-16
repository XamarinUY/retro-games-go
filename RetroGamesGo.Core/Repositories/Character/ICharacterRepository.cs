using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using RetroGamesGo.Core.Models;

namespace RetroGamesGo.Core.Repositories
{
    public interface ICharacterRepository
    {
        Task<int> AddCharacter(Character character);
        Task<int> UpdateCharacter(Character character);
        Task<int> DeleteCharacter(Character character);
        Task<bool> DeleteAll();
        Task<Character> GetCharacter(Guid id);
        Task<List<Character>> GetAll();
    }
}
