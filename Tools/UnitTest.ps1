# Remember current location
Push-Location

# Change working directory to script directory
cd (Split-Path $MyInvocation.MyCommand.Path)

# Run tests and collect coverage data
dotnet test ../Tests/UnitTests/ /p:CollectCoverage=true

# Return to previous location
Pop-Location