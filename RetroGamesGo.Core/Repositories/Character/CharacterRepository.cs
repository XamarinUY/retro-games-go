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
                MainImage = "marioBros.png",
                Silhouette = "marioBrosSilhouette.png",
                Url = "https://es.wikipedia.org/wiki/Mario_Bros.",
                Captured = false,
                AssetSticker = "Mario/MarioSticker.png",
                AssetModel = "Mario/Mario.obj",
                AssetTexture = "Mario/Mario.png",
                AssetSound = "mario_coin.mp3"
            });
            await this.AddCharacter(new Character()
            {
                Id = Guid.NewGuid(),
                Number = 2,
                Name = "Donkey Kong",
                Description = "Es un juego de máquina recreativa creado por Nintendo en el año 1981. Es un primitivo juego del género plataformas que se centra en controlar al personaje sobre una serie de plataformas mientras evita obstáculos",
                Year = 1981,
                FunFact = "fun",
                Animation = "Circus_F.json",
                MainImage = "donkeyKong.png",
                Silhouette = "donkeyKongSilhouette.png", //TODO: change this
                Url = "https://es.wikipedia.org/wiki/Donkey_Kong_(videojuego)",
                Captured = false,
                AssetSticker = "DonkeyKong/DonkeyKongSticker.png",
                AssetModel = "DonkeyKong/DonkeyKong.obj", //TODO: change this
                AssetTexture = "DonkeyKong/DonkeyKong.png", //TODO: change this
                AssetSound = "DonkeyKong.wav"
            });
            await this.AddCharacter(new Character()
            {
                Id = Guid.NewGuid(),
                Number = 3,
                Name = "Space Invader",
                Description = "Es un videojuego de arcade diseñado por Toshihiro Nishikado y lanzado al mercado en 1978. Su objetivo es eliminar oleadas de alienígenas con un cañón láser y obtener la mayor cantidad de puntos posible",
                Year = 1978,
                FunFact = "fun",
                Animation = "Circus_F.json",
                MainImage = "spaceInvader.png",
                Silhouette = "spaceInvaderSilhouette.png", //TODO: change this
                Url = "https://es.wikipedia.org/wiki/Space_Invaders",
                Captured = false,
                AssetSticker = "SpaceInvader/SpaceInvaderSticker.png",
                AssetModel = "SpaceInvader/Space_Invader.obj", //TODO: change this
                AssetTexture = "Mario/Mario.png", //TODO: change this
                AssetSound = "space.wav"
            });
            await this.AddCharacter(new Character()
            {
                Id = Guid.NewGuid(),
                Number = 4,
                Name = "Sonic",
                Description = " Este videojuego de plataformas fue, durante mucho tiempo, considerado el buque insignia de Sega, el ejemplo a seguir para sus futuros juegos. Incluso llegó a dar nombre a uno de sus equipos de desarrollo",
                Year = 1991,
                FunFact = "fun",
                Animation = "Circus_F.json",
                MainImage = "sonic.png",
                Silhouette = "sonicSilhouette.png", //TODO: change this
                Url = "https://es.wikipedia.org/wiki/Sonic_the_Hedgehog_(videojuego_de_1991)",
                Captured = false,
                AssetSticker = "Sonic/SonicSticker.png",
                AssetModel = "Sonic/Sonic.obj", //TODO: change this
                AssetTexture = "Sonic/Sonic.png", //TODO: change this
                AssetSound = "sonic.wav"
            });
        }
    }
}
