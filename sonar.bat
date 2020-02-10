dotnet tool install --global dotnet-sonarscanner --version 4.8.0
dotnet sonarscanner begin /k:"Solid_ebook" /d:sonar.host.url=http://191.234.179.215:9000/ /d:sonar.login=admin /d:sonar.password=admin
dotnet build
dotnet test /p:CollectCoverage=true /p:CoverletOutputFormat=opencover -l trx
dotnet sonarscanner end  /d:sonar.login=admin /d:sonar.password=admin