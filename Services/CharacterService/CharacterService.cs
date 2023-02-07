global using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using dotnet_rpg.Dtos.Character;

namespace dotnet_rpg.Services.CharacterService
{
    public class CharacterService : ICharacterService
    {
        //  The bellow static is commented b/c it is replaced by the sql server data
        // private static List<Character> characters = new List<Character>(){
        //      new Character(),
        //     new Character{Id=1, Name="Gashaw"}
        // };
        private readonly IMapper _mapper;
        private readonly DataContext _context;
        public CharacterService(IMapper mapper, DataContext context)
        {
            // get the context data injected, the context is available everywhere of our characterService
            // get Database context here
            _context = context;
            _mapper = mapper;
        }
        public async Task<ServiceResponse<List<GetCharacterDto>>> AddCharacter(AddCharacterDto newCharacter)
        {
            var serviceResponse = new ServiceResponse<List<GetCharacterDto>>();
            var character = _mapper.Map<Character>(newCharacter);
            // character.Id = characters.Max(c => c.Id) + 1; ==the sql server do this by itself .. .. no increment manually.
            // characters.Add(character); ====this is done for non db context
            _context.Characters.Add(character);
            //   this method writes changes to the database
            await _context.SaveChangesAsync();
            // serviceResponse.Data = characters.Select(c => _mapper.Map<GetCharacterDto>(c)).ToList(); == do for non db implementation
            serviceResponse.Data =
            await _context.Characters.Select(c => _mapper.Map<GetCharacterDto>(c)).ToListAsync();
            return serviceResponse;
        }
        public async Task<ServiceResponse<List<GetCharacterDto>>> GetAllCharacters()
        {
            var serviceResponse = new ServiceResponse<List<GetCharacterDto>>();
            // get all character from database
            var dbCharacters = await _context.Characters.ToListAsync();
            // serviceResponse.Data = characters.Select(c => _mapper.Map<GetCharacterDto>(c)).ToList();
            serviceResponse.Data = dbCharacters.Select(c => _mapper.Map<GetCharacterDto>(c)).ToList();
            return serviceResponse;
        }
        public async Task<ServiceResponse<GetCharacterDto>> GetCharacterById(int id)
        {
            var serviceResponse = new ServiceResponse<GetCharacterDto>();
            var dbCharacter = await _context.Characters.FirstOrDefaultAsync(c => c.Id == id);
            // var character = characters.FirstOrDefault(c => c.Id == id);
            serviceResponse.Data = _mapper.Map<GetCharacterDto>(dbCharacter);
            return serviceResponse;
        }
        public async Task<ServiceResponse<GetCharacterDto>> UpdateCharacter(UpdateCharacterDto updatedCharacter)
        {
            var serviceResponce = new ServiceResponse<GetCharacterDto>();
            try
            {

                // var character = characters.FirstOrDefault(c => c.Id == updatedCharacter.Id); for non db or static app
                var character = await _context.Characters.FirstOrDefaultAsync(c => c.Id == updatedCharacter.Id);

                if (character is null)
                {
                    throw new Exception($"Character with ID '{updatedCharacter.Id}' not found ");
                }
                _mapper.Map(updatedCharacter, character);

                character.Name = updatedCharacter.Name;
                character.HitPoint = updatedCharacter.HitPoint;
                character.Strength = updatedCharacter.Strength;
                character.Defense = updatedCharacter.Defense;
                character.Inteligence = updatedCharacter.Inteligence;
                character.Class = updatedCharacter.Class;

                await _context.SaveChangesAsync();

                serviceResponce.Data = _mapper.Map<GetCharacterDto>(character);
            }
            catch (Exception ex)
            {
                serviceResponce.Success = false;
                serviceResponce.Message = ex.Message;
            }
            return serviceResponce;
        }
        public async Task<ServiceResponse<List<GetCharacterDto>>> DeleteCharacter(int id)
        {
            var serviceResponce = new ServiceResponse<List<GetCharacterDto>>();
            try
            {

                // The difference between firstOrDefault and First is
                // FirstOrDefault==== will return null if no matching entity is found
                // First === throw an exception directly if ...........................
                // var character = characters.FirstOrDefault(c => c.Id == id);

                var character = await _context.Characters.FirstOrDefaultAsync(c => c.Id == id);
                if (character is null)

                    throw new Exception($"Character with ID '{id}' not found ");
                // characters.Remove(character);
                _context.Characters.Remove(character);

                await _context.SaveChangesAsync();
                // serviceResponce.Data = characters.Select(c => _mapper.Map<GetCharacterDto>(c)).ToList();
                serviceResponce.Data = await _context.Characters.Select(c => _mapper.Map<GetCharacterDto>(c)).ToListAsync();


            }
            catch (Exception ex)
            {
                serviceResponce.Success = false;
                serviceResponce.Message = ex.Message;
            }
            return serviceResponce;
        }
    }
}