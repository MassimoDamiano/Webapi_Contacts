internal class UsuarioDbExtension
{
    public static Usuario ObtenerUsuarioEmail(string email)
    {
        Usuario usuario = new Usuario();
        usuario.Email = "tudaipem3@";
        usuario.NombreApellido = "tudaipem3";
        usuario.PasswordHash = "tudaipem3";
        usuario.EmailVerificado =true;
        return usuario;
    }
}