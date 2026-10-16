# SynonymCheck — transitive synonym checker

> **Scope:** This guide describes the code actually committed to this repository. It does not assert successful runtime execution; see the linked CI checks. **No geographic map or quantitative performance chart is presented** because the repository contains no verified location or benchmark data.

## Project overview
A .NET Framework 4.8 console exercise that reads word-pair test cases, expands synonym relationships and writes 'synonyms' or 'different' results to an output file.

**Tech stack:** C# console application, .NET Framework 4.8; no external service or database.

## Feature and component inventory
| Source path | Actual responsibility |
|---|---|
| `SynonymCheck/Program.cs` | Input parser, pair processing, transitive relationship closure and output writer |
| `SynonymCheck/Input/test.in` | Example word-pair test data |
| `SynonymCheck/Output/test.out` | Sample output |
| `SynonymCheck.sln` | Visual Studio solution |

## Architecture diagram
~~~mermaid
flowchart LR
  User[Developer or HTTP client] --> Entry[C# .NET Framework application]
  Entry --> Logic[Application logic]
  Logic --> Data[Local input and output files]
~~~

## Processing sequence
~~~mermaid
sequenceDiagram
  participant U as User or client
  participant E as Application
  participant D as Local files
  U->>E: Start application or request operation
  E->>D: Read required data
  D-->>E: Return data
  E-->>U: Processed result
~~~

## Installation and setup (source-grounded)
1. Install Visual Studio with the .NET Framework 4.8 developer pack on Windows.
2. Open SynonymCheck.sln; build the executable and inspect input/test.in.
3. Run from a Visual Studio build working directory consistent with the program's relative Input/Output calculation; examine Output/test.out after execution.

~~~mermaid
flowchart TD
  A[Clone repository] --> B[Install development prerequisites]
  B --> C[Open solution and restore dependencies]
  C --> D[Build and run locally]
  D --> E{Results as expected?}
  E -->|Yes| F[Exercise core behavior]
  E -->|No| G[Review environment and known limits]
  G --> B
~~~

## Usage and functionality
Program reads a case count, dictionary pair count and query pairs; synonym links are made bidirectional and Process() repeatedly expands related pairs. Output files contain one comparison result per line.

## Testing and verification
- The added GitHub Actions workflow validates local Markdown links and fenced blocks and attempts a Windows .NET Framework solution build.
- Building the solution is **not equivalent** to running the application or checking external services.
- Check the PR's **Checks** tab; do not consider this validated until CI passes.
- Manual verification: follow the setup above and compare outcomes with the source and sample files.

## Known limitations and review recommendations
- Program.cs currently reads an absolute user-specific path before computing the relative input path; this can fail on other machines and must be fixed in a separate code PR.
- Input and output paths are coupled to the current working directory; no command-line path arguments are implemented.
- No dedicated unit test project; sample input and output require manual comparison.
- Do not publish credentials, tokens or developer-specific data from configuration into documentation.
- This documentation-only PR does not change core application behavior.

## Next steps
Run the application in the documented environment, add dedicated automated tests around core behavior, and separate any necessary code fixes into subsequent PRs.
