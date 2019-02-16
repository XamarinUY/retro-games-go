using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using MvvmCross;
using RetroGamesGo.Core.Models;

namespace RetroGamesGo.Core.Repositories
{
    public class CharacterRepository : ICharacterRepository
    {
        private IDatabase<Character> database;

        public CharacterRepository(IDatabase<Character> db = null)
        {
            database = db ?? Mvx.IoCProvider.Resolve<IDatabase<Character>>();
        }

        public async Task<int> AddCharacter(Character character)
        {
            if (character != null)
            {
                try
                {
                    return await database.Insert(character);
                }
                catch (Exception ex)
                {
                    throw new RepositoryException("The character could not be added to the database");
                }
            }
            return 0;
        }

        public async Task<int> UpdateCharacter(Character character)
        {
            if (character != null)
            {
                try
                {
                    return await database.Update(character);
                }
                catch (Exception ex)
                {
                    throw new RepositoryException("The character could not be updated to the database");
                }
            }
            return 0;
        }

        public async Task<Character> GetCharacter(Guid id)
        {
            if (id != null)
            {
                try
                {
                    return await database.Select(id);
                }
                catch (Exception ex)
                {
                    throw new RepositoryException("The character could not be selected");
                }
            }
            return null;
        }

        public async Task<List<Character>> GetAll()
        {
            try
            {
                return await database.Select();
            }
            catch (Exception ex)
            {
                throw new RepositoryException("The characters list could not be got");
            }
        }


        public async Task<int> DeleteCharacter(Character character)
        {
            throw new NotImplementedException();
        }


    }
}
