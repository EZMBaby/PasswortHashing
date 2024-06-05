using System.Security.Cryptography;

namespace PasswortHashing
{
    internal class PasswortHasher
    {
        /// <summary>
        /// Größe des Salt.
        /// </summary>
        private const int SaltSize = 16;

        /// <summary>
        /// Größe des Hash.
        /// </summary>
        private const int HashSize = 20;

        private const string HashPrefix = "$FAMDTL$V1$";

        public static int SaltSize1 => SaltSize;

        public static int HashSize1 => HashSize;

        /// <summary>
        /// Erstellt ein Hash anhand des gegebenen Passworts.
        /// </summary>
        /// <param name="password">Das Password.</param>
        /// <param name="iterations">Anzahl der Iterationen.</param>
        /// <returns>Den Hash</returns>
        public static string Hash(string password, int iterations)
        {
            // Erstelle den Salt
            byte[] salt;
            new RNGCryptoServiceProvider().GetBytes(salt = new byte[SaltSize1]);

            // Erstelle den Hash
            Rfc2898DeriveBytes passwordBasedKeyDerivationFunc2 = new Rfc2898DeriveBytes(password, salt, iterations);
            byte[] hash = passwordBasedKeyDerivationFunc2.GetBytes(HashSize1);

            // Salt und Hash kombinieren
            byte[] hashBytes = new byte[SaltSize1 + HashSize1];
            Array.Copy(salt, 0, hashBytes, 0, SaltSize1);
            Array.Copy(hash, 0, hashBytes, SaltSize1, HashSize1);

            // Zu base64 konvertieren
            string base64Hash = Convert.ToBase64String(hashBytes);

            // Hash mit extra Informationen Formatieren
            return string.Format($"{HashPrefix}{iterations}${base64Hash}");
        }

        /// <summary>
        /// Erstellt ein Hash mit 10000 Iterationen
        /// </summary>
        /// <param name="password">Das Passwort.</param>
        /// <returns>Den Hash.</returns>
        public static string Hash(string password)
        {
            return Hash(password, 10000);
        }

        /// <summary>
        /// Prüft, ob der gegebene Hash unterstützt wird
        /// </summary>
        /// <param name="hashString">Der Hash.</param>
        /// <returns>Bool ob unterstützt wird oder nicht</returns>
        public static bool IsHashSupported(string hashString)
        {
            return hashString.Contains(HashPrefix);
        }

        /// <summary>
        /// Prüft und verifiziert das Passwort mit dem Hash.
        /// </summary>
        /// <param name="password">Das zu verifizierende Passwort.</param>
        /// <param name="hashedPassword">Der Hash.</param>
        /// <returns>Bool ob verifiziert werden kann</returns>
        public static bool Verify(string password, string hashedPassword)
        {
            // Hash prüfen
            if (!IsHashSupported(hashedPassword))
            {
                throw new NotSupportedException("The hashtype is not supported");
            }

            // Iterationen und Base64-String extrahieren
            string[] splittedHashString = hashedPassword.Replace(HashPrefix, "").Split('$');
            int iterations = int.Parse(splittedHashString[0]);
            string base64Hash = splittedHashString[1];

            // Hash Bytes erhalten
            byte[] hashBytes = Convert.FromBase64String(base64Hash);

            // Salt erhalten
            byte[] salt = new byte[SaltSize1];
            Array.Copy(hashBytes, 0, salt, 0, SaltSize1);

            // Hach mit dem gegebenen Salt erstellen
            Rfc2898DeriveBytes passwordBasedKeyDerivationFunc2 = new Rfc2898DeriveBytes(password, salt, iterations);
            byte[] hash = passwordBasedKeyDerivationFunc2.GetBytes(HashSize1);

            // Get result
            for (int i = 0; i < HashSize1; i++)
            {
                if (hashBytes[i + SaltSize1] != hash[i])
                {
                    return false;
                }
            }
            return true;
        }
    }
}

