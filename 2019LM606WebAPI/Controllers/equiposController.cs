using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using _2019LM606WebAPI.Models;
using Microsoft.EntityFrameworkCore;

namespace _2019LM606WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class equiposController : ControllerBase
    {
        private readonly prestamosContext _contexto;
        public equiposController(prestamosContext miContexto)
        {
            this._contexto = miContexto;
        }

        [HttpGet]
        [Route("api/equipos")]
        public IActionResult Get()
        {
            IEnumerable<equipos> equiposList = from e in _contexto.equipos select e;
            if(equiposList.Count() > 0)
            {
                return Ok(equiposList);
            }
            return NotFound();
        }

        [HttpGet]
        [Route("api/equipos/{idUsuarios}")]
        public IActionResult Get(int idUsuario)
        {
            equipos equipo = (from e in _contexto.equipos where e.id_equipos == idUsuario select e).FirstOrDefault();
            if(equipo != null)
            {
                return Ok(equipo);
            }
            return NotFound();
        }

        [HttpPost]
        [Route("api/equipos")]
        public IActionResult guardarEquipo([FromBody] equipos equipoNuevo)
        {
            try
            {
                _contexto.equipos.Add(equipoNuevo);
                _contexto.SaveChanges();
                return Ok(equipoNuevo);
            }
            catch (System.Exception)
            {
                return BadRequest();
            }
        }

        [HttpPut]
        [Route("api/equipos")]
        public IActionResult updateEquipo([FromBody] equipos equipoModificar)
        {
            equipos equipoExiste = (from e in _contexto.equipos where e.id_equipos == equipoModificar.id_equipos select e).FirstOrDefault();
            if(equipoExiste is null)
            {
                return NotFound();
            }

            equipoExiste.nombre = equipoModificar.nombre;
            equipoExiste.description = equipoModificar.description;
            _contexto.Entry(equipoExiste).State = EntityState.Modified;
            _contexto.SaveChanges();

            return Ok(equipoExiste);
        }
    }
}
