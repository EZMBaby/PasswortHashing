namespace PasswortHashing
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Console.SetWindowSize(Console.WindowWidth*2, Console.WindowHeight*2);

            string savedPassword = PasswortHasher.Hash(SetNewPassword());

            OutputHandler.PrintTitle();
            OutputHandler.TypeWriter("Passwort erfolgreich eingerichtet!");
            Console.ReadKey();

            CheckPassword(savedPassword);
            Console.ReadKey();
        }

        static string SetNewPassword()
        {
            string setPassword = "";
            while (setPassword == "")
            {
                OutputHandler.PrintTitle();
                OutputHandler.TypeWriter("Passwort eingeben: ", false);
                setPassword = Console.ReadLine();
            }
            return setPassword;
        }

        static void CheckPassword(string hash)
        {
            string testPassword = "";
            while (testPassword == "")
            {
                OutputHandler.PrintTitle();
                OutputHandler.TypeWriter("Passwort zum anmelden eingeben: ", false);
                testPassword = Console.ReadLine();
            }

            bool verify = PasswortHasher.Verify(testPassword, hash);
            OutputHandler.PrintEval(testPassword, hash, verify);
        }
    }
}
