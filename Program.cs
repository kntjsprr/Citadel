using System;
using System.Text;
using System.Threading.Tasks;
using System.Security.Cryptography;
using System.IO;

namespace Citadel
{
    public partial class Program
    {
        static async Task Main(string[] args)
        {
            #region Initializing Variables

            string encryptionKey, extension, fullPath, savePath, action;
            byte[] encryptionKeyBytes;
            CitadelMain.Mode conversionMode;
            string defaultKey = @"LLp53sQt%BDPmKn?";
            // filePath and fileName used to split fullPath (Currently not used. Initially created to implement predetermined filenames.)
            #endregion


            int pointer = args.Length - 1;
            if (pointer < 0) //No argument
            {
                Console.WriteLine("No arguments are entered. Use -h for help.");
                return;
            } else if (args[0] == "-h") {
                Console.WriteLine("\nShowing Help:");
                Console.WriteLine("=======================\n");
                Console.WriteLine("Args : [the path of the file you wish to modify]  [folder where your file will be saved] [-e to encrypt/-d to decrypt] 4th: [encryption key (optional)]  5th: [file extension (optional)]\n\n");

                return;
            }

            if (pointer < 1){
                Console.WriteLine("No path to save. Use -h to help");
                return;
            } else if (pointer < 2) {
                Console.WriteLine("No command specified whether to encrypt or decrypt. Use -h for help.");
                return;
            }
           fullPath = @args[0]; savePath = @args[1]; action = @args[2];

            if (!File.Exists(fullPath))
            {
                Console.WriteLine("Argument " + fullPath + " doesn't exist. \n" +
                                              "Use -h for help.");
                return;
            }
            else if (!Directory.Exists(savePath))
            {
                Console.Write("Argument " + savePath + "doesn't exist. \nUse -h for help.");
                return;
            }
            switch (action)
            {
                case @"-e":
              conversionMode = CitadelMain.Mode.Transform;
                    break;
                case @"-d":
               conversionMode = CitadelMain.Mode.Restore;
                    break;
                default:
                    Console.WriteLine("Invalid argument. Use -h for help.");
                    return;
            }
                if (pointer < 3)
            {
                encryptionKey = defaultKey;
            }
                 else if(args[3] == "")
            {
                encryptionKey = defaultKey;
            } else {
                encryptionKey = args[3];
            } if (pointer < 4)
            {
                extension = "enc"; //File extension of the encrypted file
            }  else {
                extension = @args[4];
            }

            /*
            Regex rxPattern = new Regex(@"[^\\\\]*$"); // Regex pattern to use
            Match rx = rxPattern.Match(fullPath); //Match fileName pattern to fullpath and put it into a variable named fileName
            string filePath = Regex.Replace(fullPath, rx.Value, ""); //Remove fileName value and name it to a var named filePath
            string fileName = rx.Value;
            */

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
            {  // (conversionMode == CitadelMain.Mode.Restore)
                task = Task.Run(() => ft.RestoreFile(fullPath, savePath));

            }
            newFilePath = await task;
            Console.Write("Output: " + savePath + @"\" + Path.GetFileName(newFilePath));



        }


        }

    }
