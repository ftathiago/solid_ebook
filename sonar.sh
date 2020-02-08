#!/usr/bin/env bash
dotnet tool install --global dotnet-sonarscanner --version 4.8.0
dotnet sonarscanner begin /k:"DDDMentoria" /d:sonar.host.url=http://localhost:9000 /d:sonar.login=admin /d:sonar.password=admin
dotnet build
dotnet test /p:CollectCoverage=true /p:CoverletOutputFormat=opencover -l trx
dotnet sonarscanner end  /d:sonar.login=admin /d:sonar.password=admin