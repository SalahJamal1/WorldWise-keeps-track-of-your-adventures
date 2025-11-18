using AutoMapper;
using MappyApplication.Contract;
using MappyApplication.Data;
using MappyApplication.Models.workouts;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace MappyApplication.Controllers;

[ApiController]
[Route("api/v1/[controller]")]
public class WorkoutsController : ControllerBase
{
    private readonly IAuthManager _authManager;
    private readonly IMapper _mapper;
    private readonly IWorkoutsRepository _workoutsRepository;

    public WorkoutsController(IWorkoutsRepository workoutsRepository, IMapper mapper,
        IAuthManager authManager)
    {
        _workoutsRepository = workoutsRepository;
        _mapper = mapper;
        _authManager = authManager;
    }


    [HttpGet]
    [Authorize]
    public async Task<IEnumerable<WorkoutsDto>> GetAll()
    {
        var user = await _authManager.GetUser();
        var workouts = await _workoutsRepository.GetWorkoutsByUserId(user.Id);
        return _mapper.Map<IEnumerable<WorkoutsDto>>(workouts);
    }

    [HttpGet]
    [Route("{id}")]
    [Authorize]
    public async Task<WorkoutsDto> GetById(int id)
    {
        var workouts = await _workoutsRepository.GetAsyncById(id);
        return _mapper.Map<WorkoutsDto>(workouts);
    }

    [HttpDelete]
    [Route("{id}")]
    [Authorize]
    public async Task<IActionResult> DeleteById(int id)
    {
        await _workoutsRepository.DeleteAsync(id);
        return Ok(new { message = "Workout deleted successfully" });
    }

    [HttpPatch]
    [Route("{id}")]
    [Authorize]
    public async Task<IActionResult> UpdateById([FromBody] WorkoutsUpdate workoutsUpdate, int id)
    {
        var workout = await _workoutsRepository.GetAsyncById(id);

        _mapper.Map(workoutsUpdate, workout);
        await _workoutsRepository.UpdateAsync(workout);

        return Ok(new { message = "Workout updated successfully" });
    }

    [HttpPost]
    [Authorize]
    public async Task<IActionResult> AddAsynch([FromBody] Workouts workouts)
    {
        var userDto = await _authManager.GetUser();

        workouts.UserId = userDto.Id;
        await _workoutsRepository.AddAsync(workouts);


        return Ok(new { message = "Workout add successfully" });
    }
}