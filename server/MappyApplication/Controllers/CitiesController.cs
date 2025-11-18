using AutoMapper;
using MappyApplication.Contract;
using MappyApplication.Data;
using MappyApplication.Models.cities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace MappyApplication.Controllers;

[ApiController]
[Route("api/v1/[controller]")]
public class CitiesController : ControllerBase
{
    private readonly IAuthManager _authManager;
    private readonly ICitiesRepository _citiesRepository;
    private readonly IMapper _mapper;

    public CitiesController(ICitiesRepository citiesRepository, IMapper mapper,
        IAuthManager authManager)
    {
        _citiesRepository = citiesRepository;
        _mapper = mapper;
        _authManager = authManager;
    }


    [HttpGet]
    [Authorize]
    public async Task<IEnumerable<CitiesDto>> GetAll()
    {
        var user = await _authManager.GetUser();
        var cities = await _citiesRepository.GetCitiesByUserId(user.Id);

        return _mapper.Map<IEnumerable<CitiesDto>>(cities);
    }

    [HttpGet]
    [Route("{id}")]
    [Authorize]
    public async Task<CitiesDto> GetById(int id)
    {
        var city = await _citiesRepository.GetAsyncById(id);
        return _mapper.Map<CitiesDto>(city);
    }

    [HttpDelete]
    [Route("{id}")]
    [Authorize]
    public async Task<IActionResult> DeleteById(int id)
    {
        await _citiesRepository.DeleteAsync(id);
        return Ok(new { message = "Cities deleted successfully" });
    }

    [HttpPatch]
    [Route("{id}")]
    [Authorize]
    public async Task<IActionResult> UpdateById([FromBody] CitiesUpdate citiesUpdate, int id)
    {
        var cities = await _citiesRepository.GetAsyncById(id);

        _mapper.Map(citiesUpdate, cities);
        await _citiesRepository.UpdateAsync(cities);

        return Ok(new { message = "Cities updated successfully" });
    }

    [HttpPost]
    [Authorize]
    public async Task<IActionResult> AddAsynch([FromBody] Cities cities)
    {
        var userDto = await _authManager.GetUser();

        cities.UserId = userDto.Id;
        await _citiesRepository.AddAsync(cities);


        return Ok(new { message = "Cities add successfully" });
    }
}