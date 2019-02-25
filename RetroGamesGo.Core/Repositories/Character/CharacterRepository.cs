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
            await this.AddCharacter(new Character()
            {
                Id = Guid.NewGuid(),
                Number = 1,
                Name = "Mario Bros",
                Description = "Es un videojuego de arcade desarrollado por Nintendo en el año 1983. Fue creado por Shigeru Miyamoto. En el juego, Mario es retratado como un fontanero italo-estadounidense que, junto con su hermano menor Luigi, tiene que derrotar a las criaturas que han venido de las alcantarillas debajo de Nueva York.",
                Year = 1983,
                FunFact = "fun",
                Animation = "Mario_F.json",
                Silhouette = "marioBrosSilhouette.png",
                Url = "https://es.wikipedia.org/wiki/Mario_Bros.",
                Captured = false,
                AssetSticker = "Mario/MarioSticker.png",
                AssetModel = "Mario/Mario.obj",
                AssetTexture = "Mario/Mario.png",

            });
            await this.AddCharacter(new Character()
            {
                Id = Guid.NewGuid(),
                Number = 2,
                Name = "Pac-Man",
                Description = "Desde que Pac-Man fue lanzado el 21 de mayo de 1980, fue un éxito. Se convirtió en un fenómeno mundial en la industria de los videojuegos, llegó a tener el récord Guiness del videojuego de arcade más exitoso de todos los tiempos con un total de 293 822 máquinas vendidas desde 1981 hasta 1987.",
                Year = 1980,
                FunFact = "fun",
                Animation = "Pacman_F.json",
                Silhouette = "marioBrosSilhouette.png",
                Url = "https://es.wikipedia.org/wiki/Pac-Man",
                Captured = true,
                AssetSticker = "PacMan/PacmanSticker.png",
                AssetModel = "PacMan/Mario.obj",
                AssetTexture = "PacMan/Mario.png",
            });
            await this.AddCharacter(new Character()
            {
                Id = Guid.NewGuid(),
                Number = 3,
                Name = "Circus Charlie",
                Description = "es un videojuego arcade publicado por Konami en 1984. Apareció en Nintendo DS dentro del compilado Konami Classics Series: Arcade Hits.",
                Year = 1984,
                FunFact = "fun",
                Animation = "Circus_F.json",
                Silhouette = "marioBrosSilhouette.png",
                Url = "https://es.wikipedia.org/wiki/Circus_Charlie",
                Captured = true,
                AssetSticker = "CircusCharlie/CircusCharlieSticker.jpg",
                AssetModel = "CircusCharlie/Mario.obj",
                AssetTexture = "CircusCharlie/Mario.png",
            });
        }
    }
}
