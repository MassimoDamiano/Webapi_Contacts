using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
[Authorize]


[ApiController]
//Mapea una ruta del tipo contacto
[Route("api/[controller]")]

public class ContactoController : ControllerBase
{
    public readonly ContactoService _service;

    public ContactoController(ContactoService service)
    {
        _service = service;
    }


    
    // Obtener todos
[HttpGet]
public ActionResult<List<Contacto>> ObtenerTodos()
{
    var contactos = _service.ObtenerTodos();
    return Ok(contactos);
}

// Obtener por ID
[HttpGet("{id}")]
public ActionResult<Contacto> ObtenerPorId(int id)
{
    var contacto = _service.ObtenerPorId(id);

    if (contacto == null)
    {
        return NotFound();
    }

    return Ok(contacto);
}


    // POST: api/contacto/add
    [HttpPost("add")]
    public ActionResult<Contacto> Crear(Contacto contacto)
    {
        var nuevo = _service.Crear(contacto);

        return CreatedAtAction(nameof(ObtenerPorId), new { id = nuevo.Id }, nuevo);
    }



    // PUT: api/contacto/edit/{id}
    [HttpPut("edit/{id}")]
    public ActionResult Editar(int id, [FromBody] Contacto contacto)
    {
        var editado = _service.Editar(id, contacto);

        if (!editado)
        {
            return NotFound();
        }

        return NoContent();
    }

    // Eliminar contacto 
    [HttpDelete("{id}")]
    public ActionResult Eliminar(int id)
    {
        var eliminado = _service.Eliminar(id);

        if (!eliminado)
        {
            return NotFound();
        }

        return NoContent();
    }
}