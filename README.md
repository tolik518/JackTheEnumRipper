<p align="center">    
	<img src="https://github.com/tolik518/JackTheEnumRipper/assets/3026792/2bd482c4-ef87-40fd-aa16-57c87af617de">    
</p>
<h1 align="center">Jack the Enum Ripper</h1>

<p align="center">    
	Jack the Enum Ripper is a CLI tool designed to extract enums from .NET assemblies and output them in various formats. This tool supports both .NET Framework 4.8 and .NET 8.0.
</p>

## Table of Contents

- [Features](#features)
- [Getting Started](#getting-started)
  - [Usage](#usage)
  - [Example](#example)
- [Building](#building)
  - [Prerequisites](#prerequisites)
  - [Steps](#steps)
- [Contributing](#contributing)
- [License](#license)

## Features

- **Dump Enums**: Load a .NET assembly (.exe or .dll) to dump all enumeration types.
- **Output Formats**: Supports exporting enums into multiple formats, including C#, JSON, INI, PHP, and Rust, adhering to each language's conventions.

## Getting Started

### Usage

Run `JackTheEnumRipper.exe` from the command line with the following syntax:

```
JackTheEnumRipper.exe <path_to_assembly> [format]
```

#### Formats

- `--json`: Output enums in JSON format
- `--ini`: Output enums in INI format
- `--php`: Output enums in PHP format
- `--rust`: Output enums in Rust format
- `--cs`: Output enums in C# format

If no options are specified, the tool defaults to exporting enums in C# format.

### Example

To extract enums from `MyExecutable.exe` in Rust format:

```
JackTheEnumRipper.exe path\to\MyExecutable.exe --rust
```

This will create a directory named `Enums.MyExecutable` in the same location as `MyExecutable.exe`, containing the extracted enums in Rust format.

You can alternatively just drag and drop the assembly to the `JackTheEnumRipper.exe`

## Building

### Prerequisites

- .NET Framework 4.8 SDK or .NET 8.0 SDK, depending on your target environment.
- Recommended: Visual Studio 2019 or newer

### Steps

1. Clone the repository or download the source code
2. Open the solution in Visual Studio
3. Build the project for .NET Framework 4.8 or .NET 8.0 as required
4. The `JackTheEnumRipper.exe` executable is generated in `bin/Release` or `bin/Debug`


## Contributing

Contributions are welcome! If you have suggestions for improvements, please fork the repository and submit a pull request, or open an issue to discuss your ideas.

## License

Jack the Enum Ripper is open-source software licensed under the MIT License. See the `LICENSE` file for more details.

```                                                                                
   ▄██▀▀▀▀▀▀▀▀▀▀▀▀▀▀▀▀▀▀▀▀▀▀▀▀▀▀▀▀▀▀▀▀▀▀▀▀▀▀▀▀▀▀▀▀▀▀▀▀▀▀▀▀▀▀▀▀▀▀▀▀▀▀▀▀▀▀▀▀▀██▄  
   █▀                                                                       ▀█  
   █      ▀███   ▄       ▄▄   ██   ▄    T   ▄██▀▀▀▀ █▄   ██ ██ ██ ██▄   ▄██  █  
   █       ██  ▄█▀█▄  ▄███▀█▄ ██▄██▀    H   ███▄▄▄  ███▄ ██ ██ ██ ████▄████  █  
   █   ▄▄  ██ ▄█████▄ ███▄    ██▀█▄     E   ███▀▀   ██ ▀███ ██▄██ ███ ██ ██  █  
   █  ███▄███ █▀  ▀█▀  ▀████▀ ▀█ ▀██        ▀██████ ▀█   ▀█ ▀███▀ ██▀    ██  █  
   █   ▀▀▀▀▀       ▄▄▄▄▄▄      ▄▄▄▄▄▄   ▄▄▄▄▄▄   ▄▄▄▄▄▄▄  ▄▄▄▄▄▄             █  
   █              ███▀▀███ ▄▄ ███▀▀███ ███▀▀███ ████▀▀▀  ███▀▀███            █  
   █              ███ ▄█▀     ███▄▄██▀ ███▄▄██▀ ██████▀  ███ ▄█▀             █  
   █              ███▀▀██▄ ██ ███▀▀    ███▀▀    ███      ███▀▀██▄            █  
   █        ▄▄     ▀█  █▀▀▄█▀▄███▄    ▄███▄    ▄████████▄▀██   █▀   ▄▄       █  
   █       ████▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄████      █  
   ██▄      ▀▀                                                      ▀▀     ▄██  
    ▀▀▀▀▀▀▀▀▀▀▀▀▀▀▀▀▀▀▀▀▀▀▀▀▀▀▀▀▀▀▀▀▀▀▀▀▀▀▀▀▀▀▀▀▀▀▀▀▀▀▀▀▀▀▀▀▀▀▀▀▀▀▀▀▀▀▀▀▀▀▀▀▀   
 ```
