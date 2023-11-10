using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using CRUDTrabajadores.Data;
using CRUDTrabajadores.Models;

namespace CRUDTrabajadores.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RestController : ControllerBase
    {
        private readonly ContextoBD _context;

        public RestController(ContextoBD context)
        {
            _context = context;
        }

        [HttpGet("provincia/{departamentoId}")]
        public async Task<ActionResult<IEnumerable<Provincia>>> GetProvinciaByDepartamento(int departamentoId)
        {
            return await _context.Provincia.Where(p => p.DepartamentoId == departamentoId).ToListAsync();
        }

        [HttpGet("distrito/{provinciaId}")]
        public async Task<ActionResult<IEnumerable<Distrito>>> GetDistritoByProvincia(int provinciaId)
        {
            return await _context.Distrito.Where(d => d.ProvinciaId == provinciaId).ToListAsync();
        }
    }
}
