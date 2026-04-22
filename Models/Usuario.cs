public class Usuario
{   
    public Guid Id { get; set; }
    public string Email { get; set; }
    public string NombreApellido { get; set; }
    public string PasswordHash { get; set; }
    public bool EmailVerificado { get; set; }
    
}