FROM microsoft/dotnet:2.1-aspnetcore-runtime
WORKDIR /dotnetapp
COPY ./bin/Docker .
ENV ASPNETCORE_URLS https://*:5001
ENV ASPNETCORE_ENVIRONMENT Docker
ENTRYPOINT dotnet Actio.Api.dll