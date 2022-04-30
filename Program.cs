using System;
using System.Text;
using System.Threading.Tasks;
using System.Security.Cryptography;
using System.IO;
using System.Text.RegularExpressions;

namespace Citadel
{
    public partial class Program
    {
        static async Task Main(string[] args)
        {
            Console.ForegroundColor = ConsoleColor.Green;

            #region Initializing Variables
            string encryptionKey, extension;
            byte[] encryptionKeyBytes;
            CitadelMain.Mode conversionMode;
            string defaultKey = "y2J7gj+=fkN-g&h7"; 
            #endregion
            Console.WriteLine("Citadel v1.0.4");
            Console.WriteLine("https://github.com/kntjspr/Citadel");
             /*       Other variable used      
             * filePath and fileName used to split fullPath (Currently not used. Initially created to implement predetermined filenames.)
             * Length starts with 1 but array start with 0  */

            if (args.Length <= 0) {//No argument
                Console.WriteLine("No arguments are entered. Use -h for help.");
                return;
            } else if (args[0] == "-h") {
                Console.WriteLine("\nShowing Help (1/1): ");
                Console.WriteLine("=======================\n");
                Console.WriteLine("Args: [the path of the file you wish to modify] [folder where your file will be saved] [-e to encrypt/-d to decrypt]  [encryption key (optional)]  [file extension (optional)]\n\n");
                Console.WriteLine("Example args : Citadel.exe \"%temp%\\filetomodify.exe %temp%\\FolderToSave\" -e  secretpasswordencr33 exe");
                Console.WriteLine("This one uses the full args, password set to \"secretpasswordencr33\" and extension to \".exe\"");
                Console.WriteLine("https://github.com/kntjspr/Citadel/blob/main/README.md");
                return;
            }

            if (args.Length < 2){ 
                Console.WriteLine("No path to save. Use -h to help");
                return;
            } else if (args.Length < 3) { 
                Console.WriteLine("No command specified whether to encrypt or decrypt. Use -h for help.");
                return;
            }

            string fullPath = @args[0];
            string savePath = @args[1]; 
            string action = @args[2];

            if (!File.Exists(fullPath))
            {
                Console.WriteLine("File " + fullPath + " doesn't exist. \n" +
                                              "Use -h for help.");
                return;
            }
            else if (!Directory.Exists(savePath))
            {
                Console.Write("Directory " + savePath + "doesn't exist. \nUse -h for help.");
                return;
            }
            switch (action)
            {
                case "-e":
              conversionMode = CitadelMain.Mode.Transform;
                    break;
                case "-d":
               conversionMode = CitadelMain.Mode.Restore;
                    break;
                default:
                    Console.WriteLine("Invalid action. Use -h for help.");
                    return;
            }
            if (args.Length < 3)
            {
                encryptionKey = defaultKey; 
            } else if(args[3] == ""){
                encryptionKey = defaultKey; 
            } else {
                encryptionKey = args[3];
            }
            
            if (args.Length < 4){
                extension = "enc"; 
            } else {
                extension = @args[4];
            }

           /* Separate full path into a directory and file name. Not used/referenced at this moment
           *
           * Regex rxPattern = new Regex(@"[^\\\\]*$"); // Regex pattern to use
           * Match rx = rxPattern.Match(fullPath); //Match fileName pattern to fullpath and put it into a variable named fileName
           * string filePath = Regex.Replace(fullPath, rx.Value, ""); //Remove fileName value and name it to a var named filePath
           * string fileName = rx.Value; */

            using (SHA256Managed SHA256 = new SHA256Managed())
                encryptionKeyBytes = SHA256.ComputeHash(Encoding.UTF8.GetBytes(encryptionKey));
            Task<string> task;
            string newFilePath = null;

            CitadelMain ft = new CitadelMain(encryptionKeyBytes);
            if (conversionMode == CitadelMain.Mode.Transform)
            {
                task = Task.Run(() => ft.TransformFile(fullPath, savePath, extension));
            } else {  // (conversionMode == CitadelMain.Mode.Restore)
                task = Task.Run(() => ft.RestoreFile(fullPath, savePath));
            }
            newFilePath = await task;
            Console.Write(Path.GetFileName(newFilePath));
        }
        }
    }
