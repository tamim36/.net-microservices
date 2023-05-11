using AutoMapper;
using CommandService.Data;
using CommandService.Dtos;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace CommandService.Controllers
{
    [Route("api/c/[controller]")]
    [ApiController]
    public class PlatformsController : ControllerBase
    {
        private readonly ICommandRepo _commandRepo;
        private readonly IMapper _mapper;
        public PlatformsController(ICommandRepo commandRepo, IMapper mapper)
        {
            _commandRepo = commandRepo;
            _mapper = mapper;
        }

        [HttpGet]
        public ActionResult<IEnumerable<PlatformreadDto>> GetPlatforms()
        {
            Console.WriteLine("--> Getting Platforms from CommandService");
            var platformLists = _commandRepo.GetAllPlatforms();
            return Ok(_mapper.Map<IEnumerable<PlatformreadDto>>(platformLists));
        }

        [HttpPost]
        public ActionResult TestInboundConnection()
        {
            Console.WriteLine("--> Inbound post command Service");
            return Ok("Command service post");
        }
    }
}
