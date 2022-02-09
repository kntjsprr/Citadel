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

            #region Initializing Variables

            string encryptionKey, extension;
            byte[] encryptionKeyBytes;
            CitadelMain.Mode conversionMode;
            string defaultKey = "citadel0000Encrypt#!%@#";

            #endregion
            Console.ForegroundColor = ConsoleColor.Green;

             /*       Other variable used      
             * filePath and fileName used to split fullPath (Currently not used. Initially created to implement predetermined filenames.)
             */


            //Length starts with 1 but array start with 0

            if (args.Length <= 0) //No argument
            {
                Console.WriteLine("No arguments are entered. Use -h for help.");
                return;
            } else if (args[0] == "-h") {
                Console.WriteLine("\nShowing Help:");
                Console.WriteLine("=======================\n");
                Console.WriteLine("Args Guidelines:\n\n 1st: [the path of the file you wish to modify] \n 2nd: [folder where your file will be saved] \n 3rd: [-e to encrypt/-d to decrypt] \n 4th: [encryption key (optional)] \n 5th: [file extension (optional)]\n\n");
                Console.WriteLine("Example args : Citadel.exe \"%temp%\\filetomodify.exe %temp%\\FolderToSave\" -e  secretpasswordencr33 exe");
                Console.WriteLine("This one uses the full args, password set to \"secretpasswordencr33\" and extension to \".exe\"");
                Console.WriteLine("=======================\n\n");
                Console.WriteLine("Example args #2: Citadel.exe \"%temp%\\filetomodify.exe %temp%\\FolderToSave\" -e \"\" exe ");
                Console.WriteLine("This one uses uses the default password by putting empty strings.");
                Console.WriteLine("=======================\n\n");
                Console.WriteLine("Example args #3: Citadel.exe \"%temp%\\filetomodify.exe %temp%\\FolderToSave\" -e ");
                Console.WriteLine("This one uses the uses the default password and extension.");
                Console.WriteLine("=======================\n\n");
                Console.WriteLine("Default Password if 3rd arg left blank: " + defaultKey);
                Console.WriteLine("Use empty quotes to bypass an optional argument.");
                return;
            }

            if (args.Length < 0){ //If equl or more than 0 which means it only contains first argument args[0]
                Console.WriteLine("No path to save. Use -h to help");
                return;
            } else if (args.Length < 1) { 
                Console.WriteLine("No command specified whether to encrypt or decrypt. Use -h for help.");
                return;
            }

            /* Since the args are not empty. Let's assign them and check if they are valid.*/
        

            string fullPath = @args[0];
            string savePath = @args[1]; 
            string action = @args[2];





            if (!File.Exists(fullPath))
            {
                Console.WriteLine("Argument 1 " + fullPath + " doesn't exist. \n" +
                                              "Use -h for help.");
                return;
            }
            else if (!Directory.Exists(savePath))
            {
                Console.Write("Argument 2 " + savePath + "doesn't exist. \nUse -h for help.");
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
                    Console.WriteLine("Invalid third argument. Use -h for help.");
                    return;
            }
           
            if (args.Length < 3)
            {
                encryptionKey = defaultKey; 
            } else if(args[3] == "")
            {
                encryptionKey = defaultKey; 
            } else {
                encryptionKey = args[3];
            }
            
            if (args.Length < 4){
                extension = "enc"; //File extension of the encrypted file
            } else {
                extension = @args[4];
            }

            
            Regex rxPattern = new Regex(@"[^\\\\]*$"); // Regex pattern to use
            Match rx = rxPattern.Match(fullPath); //Match fileName pattern to fullpath and put it into a variable named fileName
            string filePath = Regex.Replace(fullPath, rx.Value, ""); //Remove fileName value and name it to a var named filePath
            string fileName = rx.Value;

         
            using (SHA256Managed SHA256 = new SHA256Managed())
                encryptionKeyBytes = SHA256.ComputeHash(Encoding.UTF8.GetBytes(encryptionKey));
            Task<string> task;
            string newFilePath = null;

            CitadelMain ft = new CitadelMain(encryptionKeyBytes);
            if (conversionMode == CitadelMain.Mode.Transform)
            {
                task = Task.Run(() => ft.TransformFile(fullPath, savePath, extension));
            }
            else
            {  // (conversionMode == FileTransformer.Mode.Restore)
                task = Task.Run(() => ft.RestoreFile(fullPath, savePath));

            }
            newFilePath = await task;
            Console.Write(Path.GetFileName(newFilePath));



        }


        }

    }
