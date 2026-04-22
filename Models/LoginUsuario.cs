using Microsoft.AspNetCore.Identity;

//Esta clase nos permite Encapsular el mecanismo de inicio de sesion

internal sealed class LoginUsuario (PasswordHasher passwordHasher, TokenProvider tokenProvider)
{
    public sealed record Request (string Email, string Password);


    // Esta funciion maneja un request del tipo Email password quebusca la contraseña en la base de datos y
    // la hashea, con el request que llega validamos la contraseña actual con la de la base de datps
    //
    public async Task<string> Handle(Request request)
    {
        Usuario usuario = UsuarioDbExtension.ObtenerUsuarioEmail(request.Email);
        usuario.PasswordHash = passwordHasher.Hash(usuario.PasswordHash);

        if(usuario is null || !usuario.EmailVerificado)
        {
            throw new Exception("Usuario no Encontrado");

        }

        bool esValido = passwordHasher.Verificar(request.Password, usuario.PasswordHash);

        if (!esValido)
        {
            throw new Exception("La contraseña es incorrecta...");

        }

        string token = tokenProvider.GenerarToken(usuario);
        return token;

    }
}