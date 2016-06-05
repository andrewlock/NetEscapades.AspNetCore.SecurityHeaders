#!/usr/bin/env bash

#exit if any command fails
set -e

artifactsFolder="./artifacts"

if [ -d $artifactsFolder ]; then
  rm -R $artifactsFolder
fi

dotnet restore

# Ideally we would use theis command to test netcoreapp and net451 so restrict for now 
# but currently donsn't work due to https://github.com/dotnet/cli/issues/3073 
dotnet test ./test/NetEscapades.AspNetCore.SecurityHeaders.Test -c Release -f netcoreapp1.0

# Instead, run directly with mono for the full .net version 
dotnet build ./test/NetEscapades.AspNetCore.SecurityHeaders.Test -c Release -f net451

mono \
./test/NetEscapades.AspNetCore.SecurityHeaders.Test/bin/Release/net451/*/dotnet-test-xunit.exe \
./test/NetEscapades.AspNetCore.SecurityHeaders.Test/bin/Release/net451/*/NetEscapades.AspNetCore.SecurityHeaders.Test.dll

revision=${TRAVIS_JOB_ID:=1}
revision=$(printf "%04d" $revision) 

dotnet pack ./src/NetEscapades.AspNetCore.SecurityHeaders -c Release -o ./artifacts --version-suffix=$revision