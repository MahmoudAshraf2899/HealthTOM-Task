using Boilerplate.Contracts.Interfaces.Custom;
using Boilerplate.Shared.Consts;
using Microsoft.AspNetCore.Mvc;

namespace Boilerplate.API.Bases
{
    [ApiController]

    public class APIControllerBase : ControllerBase
    {
        protected IHolderOfDTO _holderOfDTO;

        protected APIControllerBase(IHolderOfDTO holderOfDTO)
        {
            _holderOfDTO = holderOfDTO;
        }
        protected IActionResult NotValidModelState()
        {
            _holderOfDTO.Add(Res.state, false);
            _holderOfDTO.Add(Res.modelState, ModelState);
            return BadRequest(_holderOfDTO);
        }
        protected IActionResult ValidModelState()
        {
            _holderOfDTO.Add(Res.state, true);
            return Ok(_holderOfDTO); ;
        }
        protected IActionResult Unauthorized()
        {
            _holderOfDTO.Add(Res.state, false);
            _holderOfDTO.Add(Res.modelState, ModelState);
            return Unauthorized(_holderOfDTO);
        }
        protected IActionResult MigrationState()
        {
            _holderOfDTO.Add(Res.state, true);
            _holderOfDTO.Add(Res.message, "The migration file will be uploaded and add to DB in background service it will take some time");
            return Ok(_holderOfDTO);
        }
        protected IActionResult State(IHolderOfDTO holderOfDTO = null)
        {
            if (holderOfDTO is not null)
            {
                if (!(bool)holderOfDTO[Res.state])
                    return BadRequest(holderOfDTO);

                return Ok(holderOfDTO);
            }
            return BadRequest();
        }
    }
}
