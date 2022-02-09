# Citadel
A simple command-line utility for encrypting files with the AES-256-GCM algorithm and then converting them to base64 format without . 
# Features
- Encrypt any file using AES-256-GCM algorithm and then encoding it into a base64 format.
- The original filename is included in the encrypted file and may be changed without affecting the original file. 



## How to use
This tool doesn't need any user interaction on the CLI (like console.read). You'll just have to pass the arguments on the exe file.

## Arguments

Citadel.exe [file you wish to encrypt/decrypt]  [path to the directory in which the result will be saved] [-e/-d (encrypt/decrypt)] [password (optional)] [extension file (optional)]

Example:
Citadel.exe "%temp%\\file.exe" "%temp%" -e password7737 exe
The encrypted file will be saved as .exe on %temp% folder

## Default Values
The last two arguments are optional.
If they are left blank, the pre-defined default values will be used. 

Password = "citadel0000Encrypt#!%@#"

Extension file = ".enc"

## Other Info
- File names are randomized and they will be printed on the cli. I might add a feature to use a predetermined file names.

## Troubleshoot
For runtime problems install [.NET 5.0 Runtime](https://dotnet.microsoft.com/en-us/download/dotnet/5.0) 
and if you want to modify the source you must install .NET 5.0 SDK
