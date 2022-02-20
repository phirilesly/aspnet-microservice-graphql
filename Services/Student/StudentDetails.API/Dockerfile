#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:5.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:5.0 AS build
WORKDIR /src
COPY ["Services/Student/StudentDetails.API/StudentDetails.API.csproj", "Services/Student/StudentDetails.API/"]
COPY ["Services/Student/StudentDetails.Infrastructure/StudentDetails.Infrastructure.csproj", "Services/Student/StudentDetails.Infrastructure/"]
COPY ["Services/Student/StudentDetails.Core/StudentDetails.Core.csproj", "Services/Student/StudentDetails.Core/"]
COPY ["Services/Student/StudentDetails.Abstractions/StudentDetails.Abstractions.csproj", "Services/Student/StudentDetails.Abstractions/"]
COPY ["Services/Student/ZA365Solutions.Platform.Common/ZA365Solutions.Platform.Common.csproj", "Services/Student/ZA365Solutions.Platform.Common/"]
COPY ["Services/Student/ZA365Solutions.Platform.Messaging/ZA365Solutions.Platform.Messaging.csproj", "Services/Student/ZA365Solutions.Platform.Messaging/"]
RUN dotnet restore "Services/Student/StudentDetails.API/StudentDetails.API.csproj"
COPY . .
WORKDIR "/src/Services/Student/StudentDetails.API"
RUN dotnet build "StudentDetails.API.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "StudentDetails.API.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "StudentDetails.API.dll"]
