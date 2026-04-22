using System.Security.Cryptography;

/*
sealed: no se puede heredar (la lógica de hashing queda cerrada y protegida).

internal: solo se usa dentro del mismo proyecto/ensamblado.
*/
internal sealed class PasswordHasher
{
    private const int SaltSize = 16;
    private const int HashSize = 32;
    private const int Iterations = 100000;

    private static readonly HashAlgorithmName Algorithm = HashAlgorithmName.SHA512;

/*
enera un arreglo de bytes aleatorios de tamaño SaltSize (que es 16).
Este es el "salt", un valor único y aleatorio que se mezcla con la contraseña antes de generar el hash.

Usa el algoritmo PBKDF2 (Password-Based Key Derivation Function 2).
Este algoritmo toma:
 La contraseña del usuario.
 El salt aleatorio.
 Una cantidad de iteraciones (100.000, en este caso).
 El algoritmo de hash (SHA512).
 El tamaño del hash de salida (32 bytes).

⚙️ Resultado:
 Devuelve un hash seguro que es muy difícil de romper.

Convierte ambos arreglos de bytes (hash y salt) a texto hexadecimal.
Los concatena con un - en el medio.

Para poder guardar en texto plano en la base de datos.
El - permite separarlos fácilmente cuando queramos verificar la contraseña después.
*/    

public string Hash(string password)
    {
        byte[] salt = RandomNumberGenerator.GetBytes(SaltSize);
        byte[] hash = Rfc2898DeriveBytes.Pbkdf2(password, salt, Iterations, Algorithm, HashSize);

        return $"{Convert.ToHexString(hash)}-{Convert.ToHexString(salt)}";
    }

/*
Usa la contraseña ingresada por el usuario y el salt que estaba guardado.
Genera un nuevo hash (inputHash) para comparar contra el original.
Es exactamente el mismo proceso que cuando se guardó la contraseña originalmente.

🧠 Si la contraseña ingresada es correcta, este hash debería ser igual al original.

Compara hash (el guardado en la base) con inputHash (el generado desde la contraseña ingresada).

Usa FixedTimeEquals, que evita ataques por tiempo de respuesta (side-channel attacks).
*/
    public bool Verificar(string password, string passwordHash)
    {
        string[] parts = passwordHash.Split('-');
        byte[] hash = Convert.FromHexString(parts[0]);
        byte[] salt = Convert.FromHexString(parts[1]);

        byte[] inputHash = Rfc2898DeriveBytes.Pbkdf2(password, salt, Iterations, Algorithm, HashSize);

        return CryptographicOperations.FixedTimeEquals(hash, inputHash);
    }
}
