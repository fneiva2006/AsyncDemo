#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

ARG REPO=mcr.microsoft.com/dotnet/aspnet

FROM $REPO:5.0.10-alpine3.14-amd64 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM $REPO:5.0.10-alpine3.14-amd64 AS build
WORKDIR /src
COPY ["AsyncDemo/AsyncDemo.csproj", "AsyncDemo/"]
RUN dotnet restore "AsyncDemo/AsyncDemo.csproj"
COPY . .
WORKDIR "/src/AsyncDemo"
RUN dotnet build "AsyncDemo.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "AsyncDemo.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "AsyncDemo.dll"]