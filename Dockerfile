FROM microsoft/aspnetcore:1.0.1
ARG source
WORKDIR /app
EXPOSE 80
COPY ${source:-obj/Docker/publish} .
ENTRYPOINT ["dotnet", "QDot.API.dll"]