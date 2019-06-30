FROM mcr.microsoft.com/dotnet/core/aspnet:2.2-stretch-slim AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/core/sdk:2.2-stretch AS build
WORKDIR /src
COPY ["RazorPagesMovie/RazorPagesMovie.csproj", "RazorPagesMovie/"]
RUN dotnet restore "RazorPagesMovie/RazorPagesMovie.csproj"
COPY . .
WORKDIR "/src/RazorPagesMovie"
RUN dotnet build "RazorPagesMovie.csproj" -c Release -o /app

FROM build AS publish
RUN dotnet publish "RazorPagesMovie.csproj" -c Release -o /app

FROM base AS final
WORKDIR /app
COPY --from=publish /app .
ENTRYPOINT ["dotnet", "RazorPagesMovie.dll"]