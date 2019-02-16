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

            Task.Run(async () => 
            {
                var character = await database.Select();
                if (character.Count == 0)
                {
                    await this.CreateCharacters();
                }
            });
        }

        public async Task<int> AddCharacter(Character character)
        {
            if (character != null)
            {
                try
                {
                    return await database.Insert(character);
                }
                catch (Exception)
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
                catch (Exception)
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
                catch (Exception)
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
            catch (Exception)
            {
                throw new RepositoryException("The characters list could not be got");
            }
        }

        public Task<int> DeleteCharacter(Character character)
        {
            throw new NotImplementedException();
        }

        public async Task<bool> DeleteAll()
        {
            try
            {
                return await database.DeleteAll();
            }
            catch (Exception)
            {
                throw new RepositoryException("The characters registers could not be deleted");
            }
        }

        private async Task CreateCharacters()
        {
            await this.AddCharacter(new Models.Character()
            {
                Id = Guid.NewGuid(),
                Number = 1,
                Name = "Super Mario Bros.",
                Description = "Description 1",
                FunFact = "fun",
                Picture = "marioBros.png",
                Silhouette = "marioBrosSilhouette.png",
                Url = "http:lala.mario.com"
            });
            await this.AddCharacter(new Models.Character()
            {
                Id = Guid.NewGuid(),
                Number = 2,
                Name = "Super Mario Bros.",
                Description = "Description 1",
                FunFact = "fun",
                Picture = "marioBros.png",
                Silhouette = "marioBrosSilhouette.png",
                Url = "http:lala.mario.com"
            });
            await this.AddCharacter(new Models.Character()
            {
                Id = Guid.NewGuid(),
                Number = 3,
                Name = "Super Mario Bros.",
                Description = "Description 1",
                FunFact = "fun",
                Picture = "marioBros.png",
                Silhouette = "marioBrosSilhouette.png",
                Url = "http:lala.mario.com"
            });
        }
    }
}
