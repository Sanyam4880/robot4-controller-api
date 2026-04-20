FROM mcr.microsoft.com/dotnet/aspnet:9.0
WORKDIR /app
COPY build_artifact/ .
EXPOSE 8080
ENV ASPNETCORE_URLS=http://+:8080
ENTRYPOINT ["dotnet", "robot4-controller-api.dll"]git 