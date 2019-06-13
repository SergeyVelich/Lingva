FROM microsoft/dotnet:aspnetcore-runtime

WORKDIR /app

COPY ./UI/Lingva.MVC/Lingva.MVC/publish .

ENTRYPOINT ["dotnet", "Lingva.MVC.dll"]