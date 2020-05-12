FROM mcr.microsoft.com/dotnet/core/aspnet:3.1

LABEL version="1.0.1" description="FTStore"

COPY dist /app
WORKDIR /app
EXPOSE 80/tcp
ENTRYPOINT ["dotnet","FTStore.Web.dll"]