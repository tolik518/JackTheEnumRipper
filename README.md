                                                                                
   ▄██▀▀▀▀▀▀▀▀▀▀▀▀▀▀▀▀▀▀▀▀▀▀▀▀▀▀▀▀▀▀▀▀▀▀▀▀▀▀▀▀▀▀▀▀▀▀▀▀▀▀▀▀▀▀▀▀▀▀▀▀▀▀▀▀▀▀▀▀▀██▄  
   █▀                                                                       ▀█  
   █      ▀███   ▄       ▄▄   ██   ▄    T   ▄██▀▀▀▀ █▄   ██ ██ ▐█ ██▄   ▄██  █  
   █       ██▌ ▄█▀█▄  ▄███▀█▄ ██▄██▀    H   ███▄▄▄  ███▄ ██ ██ ▐█ ████▄████  █  
   █   ▄▄  ██▌▄█████▄ ███▄    ██▀█▄     E   ███▀▀   ██▌▀███ ██▄▐█ ███ ██ ██  █  
   █  ███▄███▌█▀  ▀█▀  ▀████▀ ▀█ ▀██        ▀██████ ██▌  ▀█ ▀███▀ ██▀    ██  █  
   █   ▀▀▀▀▀▀      ▄▄▄▄▄▄      ▄▄▄▄▄▄   ▄▄▄▄▄▄   ▄▄▄▄▄▄▄  ▄▄▄▄▄▄             █  
   █              ███▀▀███ ▄▄ ███▀▀███ ███▀▀███ ████▀▀▀  ███▀▀███            █  
   █              ███ ▄█▀   ▌ ███▄▄██▀ ███▄▄██▀ ██████▀  ███ ▄█▀             █  
   █              ███▀▀██▄ ██ ███▀▀    ███▀▀    ███▌     ███▀▀██▄            █  
   █        ▄▄     ▀█  █▀▀▄█▀▄███▄    ▄███▄    ▄████████▄▀██▌  █▀   ▄▄       █  
   █       ████▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄████      █  
   ██▄      ▀▀                                                      ▀▀     ▄██  
    ▀▀▀▀▀▀▀▀▀▀▀▀▀▀▀▀▀▀▀▀▀▀▀▀▀▀▀▀▀▀▀▀▀▀▀▀▀▀▀▀▀▀▀▀▀▀▀▀▀▀▀▀▀▀▀▀▀▀▀▀▀▀▀▀▀▀▀▀▀▀▀▀▀   
                                                                                
																				

Jack the Enum Ripper is a versatile tool designed to extract enumeration definitions from .NET assemblies and output them in various formats. Crafted with precision in C#, this tool supports both .NET Framework 4.8 and .NET 8.0, ensuring broad compatibility across different .NET versions. Whether you're dealing with legacy systems or embracing the latest in .NET technology, Jack the Enum Ripper stands ready to facilitate your development workflow.

## Features

- **Dump Enums**: Load and analyze a .NET assembly (.exe or .dll) to dump all enumeration types.
- **Output Formats**: Supports exporting enums into multiple formats, including C#, JSON, INI, PHP, and Rust, adhering to each language's conventions.

## Getting Started

### Prerequisites

- .NET Framework 4.8 SDK or .NET 8.0 SDK, depending on your target environment.
- Visual Studio 2019 or newer (recommended for development).

### Installation

1. Clone the repository or download the source code.
2. Open the solution in Visual Studio.
3. Build the project for either .NET Framework 4.8 or .NET 8.0, according to your needs
	3.1 Pay attention to the CPU - x86 can only decompile x86 assemblies and x64 can only decompile x64 assemblies
4. The executable `JackTheEnumRipper.exe` will be generated in the respective `bin/Release` or `bin/Debug` directory.

### Usage

Run `JackTheEnumRipper.exe` from the command line with the following syntax:

```
JackTheEnumRipper.exe <path_to_assembly> [format]
```

#### Formats

- `--json`: Output enums in JSON format.
- `--ini`: Output enums in INI format.
- `--php`: Output enums in PHP format (requires PHP 8.1 or newer for enum support).
- `--rust`: Output enums in Rust format.
- `--cs`: Output enums in C# format.

If no options are specified, the tool defaults to exporting enums in C# format.

### Example

To extract enums from a .NET assembly named `MyAssembly.dll` and output them in JSON format, use:

```
JackTheEnumRipper.exe path\to\MyAssembly.dll --json
```

This will create a directory named `Enums.MyAssembly` in the same location as `JackTheEnumRipper.exe`, containing the extracted enums in JSON format.

You can alternatively just drag and drop the assembly to the `JackTheEnumRipper.exe`

## Contributing

Contributions are welcome! If you have suggestions for improvements, please fork the repository and submit a pull request, or open an issue to discuss your ideas.

## License

Jack the Enum Ripper is open-source software licensed under the MIT License. See the `LICENSE` file for more details.