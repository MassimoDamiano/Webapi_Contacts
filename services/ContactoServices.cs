public class ContactoService
{
    public readonly List<Contacto> _contacto = new List<Contacto>();

    private int _id = 1;
    //Primer Servicio
    public List<Contacto> ObtenerTodos() => _contacto;

    public Contacto Crear(Contacto newContacto)
    {
        newContacto.Id = _id++;
        _contacto.Add(newContacto);
        return newContacto;
    }

    public Contacto? ObtenerPorId(int id)
    {
        for (int i = 0; i < _contacto.Count; i++)
        {
            if (_contacto[i].Id == id)
            {
                return _contacto[i];
            }
        }

        return null;
    }

    public bool Editar(int id, Contacto contacto)
{
    for (int i = 0; i < _contacto.Count; i++)
    {
        if (_contacto[i].Id == id)
        {
            _contacto[i].Nombre = contacto.Nombre;
            _contacto[i].Apellido = contacto.Apellido;
            _contacto[i].Telefono = contacto.Telefono;
            _contacto[i].Email = contacto.Email;

            return true;
        }
    }

    return false;
}

 public bool Eliminar(int id)
    {
        var contacto = _contacto.FirstOrDefault(c => c.Id == id);

        if (contacto == null)
            return false;

        _contacto.Remove(contacto);
        return true;
    }

    //El método FirstOrDefault() sirve para buscar el primer elemento que cumpla una condición dentro de una lista.
    // Me parecio lo mas practico
}