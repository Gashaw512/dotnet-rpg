using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using dotnet_rpg.Dtos.Character;

namespace dotnet_rpg.Services.CharacterService
{
    public class CharacterService : ICharacterService
    {
        private static List<Character> characters = new List<Character>(){
             new Character(),
            new Character{Id=1, Name="Gashaw"}

        };
        public async Task<ServiceResponse< List<Character>>>AddCharacter(AddCharacterDto newCharacter)
        {
            var serviceResponse =new ServiceResponse<List<Character>> ();
            characters.Add(newCharacter);
            serviceResponse.Data=characters;
            return serviceResponse;
        }

        public async Task<ServiceResponse< List<GetCharacterDto>>> GetAllCharacters()
        {
             var serviceResponse =new ServiceResponse<List<GetCharacterDto>> ();
             serviceResponse.Data=characters;
            return serviceResponse;
        }

        public async Task <ServiceResponse<GetCharacterDto>> GetCharacterById(int id)
        {
            var serviceResponse =new ServiceResponse<Character> ();
            var character=characters.FirstOrDefault(c => c.Id == id);
             serviceResponse.Data=character;
             return serviceResponse;
        }
           
    }
}