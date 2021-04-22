OpenApi Specification Generator
---

Cli para exportação da OAS para arquivo json em aplicações *"webapi"* que utilizam a lib NSwag.
Compatível com dotnet 5, dotnet 3.1, dotnet 2.2, dotnet 2.1.

### Sugestão de uso

Gere o executável, adicione ao seu projeto e configure para executar após o build da webapi.
Como parametro informe o caminho completo de onde encontrar a dll da sua *"webapi"*.

Você pode fazer isso adicionando uma instrução no seu *.csproj*.

```xml
  <Target Name="PostBuild" AfterTargets="PostBuildEvent">
    <Exec Command="&quot;C:\source\repos\nswagg-export-sample\netcore 5\OpenApiSpecGenerator\bin\Debug\net5.0\OpenApiSpecGenerator.exe&quot; &quot;C:\source\repos\nswagg-export-sample\netcore 5\Users\bin\Debug\netcore5\Users.dll&quot;" />
  </Target>
```

Com isso toda vez que sua *"webapi"* for compilada, a Cli será executada e o arquivo json será gerado.
