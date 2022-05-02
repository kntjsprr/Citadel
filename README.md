# Citadel
A simple command-line utility for encrypting files using the AES-256-GCM algorithm and then converting them into a base64 format. 

## Features
- Encrypt any file using the AES-256-GCM algorithm and then encoding it into a base64 format.
- The original filename is included in the encrypted file and may be changed without affecting the original file. 
- Everything can be done using args! You can integrate this using your own app easily using cmd.

## File structure
- Chunk size can be modified on ```CitadelMain.cs```

![File Structure](https://raw.githubusercontent.com/kntjspr/Citadel/main/Github/file-structure.png)


## How to use
This tool doesn't need any user interaction on the CLI. You'll just have to pass the arguments on the exe file.

## Arguments

Citadel.exe [path of the file you wish to encrypt/decrypt]  [path to the directory in which the result will be saved] [-e/-d (encrypt/decrypt)] [password (optional)] [File Extension (Optional)] 

### Encrypting file example:
``Citadel.exe C:\path-to-file-encrypt\unencryptedFile.exe "C:\path-to-encrypted-file-output" -e my-password ext``

- Output sample: C:\path-to-encrypted-file-output\\[dbcsiwpqen.ext](https://github.com/kntjspr/Citadel#other-info)

### Decrypting file example:
``Citadel.exe C:\path-to-encrypted-file\dbcsiwpqen.cit C:\decrypted-output -d my-password``

- Output sample: `C:\decrypted-output\unencryptedFile.exe`

## Default values
The last two arguments are optional.
If they are left blank, the pre-defined default values will be used. 


## Screenshot
![Screenshot](https://raw.githubusercontent.com/kntjspr/Citadel/main/Github/Screenshot%202022-02-10%20073809.png)

## Other info
- File names are randomized when you encrypt the files to avoid file overwrite. I might add a feature to use a predetermined file names.

## Features to add
- Option to choose your own encrypted output filename and file extension
- Ability to select multiple files/folder to encrypt at once. 

(You can choose to encrypt them one by one or to repack them into a one zip file).

## Troubleshoot
For runtime problems install [.NET 5.0 Runtime](https://dotnet.microsoft.com/en-us/download/dotnet/5.0) 
and if you want to modify the source you must install .NET 5.0 SDK
