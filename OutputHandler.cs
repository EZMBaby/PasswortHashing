using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PasswortHashing
{
    internal class OutputHandler
    {
        private static string[] Logo = new string[]
        {
            "   _ (`-.    ('-.      .-')     .-')     (`\\ .-') /`             _  .-')   .-') _          ('-. .-.   ('-.      .-')    ('-. .-.   ('-.  _  .-')        ",
            "  ( (OO  )  ( OO ).-. ( OO ).  ( OO ).    `.( OO ),'            ( \\( -O ) (  OO) )        ( OO )  /  ( OO ).-. ( OO ). ( OO )  / _(  OO)( \\( -O )       ",
            " _.`     \\  / . --. /(_)---\\_)(_)---\\_),--./  .--.   .-'),-----. ,------. /     '._       ,--. ,--.  / . --. /(_)---\\_),--. ,--.(,------.,------.       ",
            "(__...--''  | \\-.  \\ /    _ | /    _ | |      |  |  ( OO'  .-.  '|   /`. '|'--...__)      |  | |  |  | \\-.  \\ /    _ | |  | |  | |  .---'|   /`. '      ",
            " |  /  | |.-'-'  |  |\\  :` `. \\  :` `. |  |   |  |, /   |  | |  ||  /  | |'--.  .--'      |   .|  |.-'-'  |  |\\  :` `. |   .|  | |  |    |  /  | |      ",
            " |  |_.' | \\| |_.'  | '..`''.) '..`''.)|  |.'.|  |_)\\_) |  |\\|  ||  |_.' |   |  |         |       | \\| |_.'  | '..`''.)|       |(|  '--. |  |_.' |      ",
            " |  .___.'  |  .-.  |.-._)   \\.-._)   \\|         |    \\ |  | |  ||  .  '.'   |  |         |  .-.  |  |  .-.  |.-._)   \\|  .-.  | |  .--' |  .  '.'      ",
            " |  |       |  | |  |\\       /\\       /|   ,'.   |     `'  '-'  '|  |\\  \\    |  |         |  | |  |  |  | |  |\\       /|  | |  | |  `---.|  |\\  \\       ",
            " `--'       `--' `--' `-----'  `-----' '--'   '--'       `-----' `--' '--'   `--'         `--' `--'  `--' `--' `-----' `--' `--' `------'`--' '--'      ",
            "                                                                                                                              by Marvin Dietermann"
        };

        public static void PrintTitle()
        {
            Console.Clear();
            foreach (string line in Logo)
            {
                Console.WriteLine(line);
                Thread.Sleep(10);
            }
            Console.WriteLine();
        }



        public static void PrintEval(string password, string hash, bool valid)
        {
            PrintTitle();

            string passwordText = $"Eingegebenes Passwort: {password}";
            string hashText = $"Hash zum prüfen: {hash}";
            string message = $"Das eingegebene Passwort stimmt ";
            if (!valid)
            {
                message += "nicht ";
            }
            message += "mit dem Hash überein!";

            string[] printAll = { passwordText, hashText, message };

            foreach (string line in printAll)
            {
                TypeWriter(line);
            }
        }

        public static void TypeWriter(string text, bool linebreak = true)
        {
            foreach (char c in text)
            {
                Console.Write(c);
                Thread.Sleep(10);
            }
            if (linebreak) Console.WriteLine();
        }
    }
}
