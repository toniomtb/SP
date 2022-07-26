using System;
using System.Linq;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using SP.Logic.Interfaces;
using SP.Model.Models;
using SP.Model.RequestModels;
using SP.Model.ResponseModels;

namespace SP.Controllers
{
    [ApiController]
    [Route("/api/[controller]")]
    public class PraticheController : ControllerBase
    {
        private readonly ILogger<PraticheController> _logger;
        private readonly IPraticheLogic _praticheLogic;
        private readonly IMapper _mapper;

        public PraticheController(ILogger<PraticheController> logger, IPraticheLogic praticheLogic, IMapper mapper)
        {
            _logger = logger;
            _praticheLogic = praticheLogic;
            _mapper = mapper;
        }

        [Authorize]
        [HttpPost]
        public IActionResult Post([FromBody] RequestPratica nuovaPratica)
        {
            _logger.LogDebug($"/api/controller POST invoked at {DateTime.Now}");

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState.Values.SelectMany(x => x.Errors).Select(x => x.ErrorMessage).ToList());
            }

            try
            {
                var response = _praticheLogic.CreateNewPratica(_mapper.Map<Pratica>(nuovaPratica));
                return Ok(response);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        [Authorize]
        [HttpPut]
        [Route("{idPratica:int}")]
        public IActionResult Put([FromRoute] int idPratica, [FromBody] RequestPratica updatedPratica)
        {
            _logger.LogDebug($"/api/controller PUT invoked at {DateTime.Now}");

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState.Values.SelectMany(x => x.Errors).Select(x => x.ErrorMessage).ToList());
            }

            try
            {
                _praticheLogic.UpdatePratica(idPratica, _mapper.Map<Pratica>(updatedPratica));
                return Ok("Pratica successfully updated");
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        [Authorize]
        [HttpGet]
        [Route("{idPratica:int}")]
        public ResponsePratica Get([FromRoute] int idPratica)
        {
            _logger.LogDebug(@$"/api/controller/{{idPratica}} GET invoked at {DateTime.Now}");

            var pratica = _praticheLogic.GetPratica(idPratica);
            var responsePratica = _mapper.Map<ResponsePratica>(pratica);
            return responsePratica;
        }

        [Authorize]
        [HttpGet]
        [Route("getpdf/{idPratica:int}")]
        public ResponsePdf GetPdf([FromRoute] int idPratica)
        {
            _logger.LogDebug(@$"/api/controller/getpdf/{{idPratica}} GET invoked at {DateTime.Now}");

            var pdfBytes = _praticheLogic.GetPdf(idPratica);

            ResponsePdf response = new ResponsePdf();
            response.AllegatoPratica = pdfBytes;
            response.IdPratica = idPratica;

            return response;
        }

        [Authorize]
        [HttpPost]
        [Route("progress/{idPratica:int}")]
        public IActionResult ProgressPratica([FromRoute] int idPratica)
        {
            _logger.LogDebug(@$"/api/controller/progress/{{idPratica}} POST invoked at {DateTime.Now}");

            try
            {
                _praticheLogic.ProgressPratica(idPratica);
                return Ok("Pratica progressed");
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }
    }
}
