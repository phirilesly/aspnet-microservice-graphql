#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:5.0 AS base
WORKDIR /app
EXPOSE 80

FROM mcr.microsoft.com/dotnet/sdk:5.0 AS build
WORKDIR /src
COPY ["Services/Department/Department.API/StudentDepartment.API.csproj", "Services/Department/Department.API/"]
COPY ["Services/Department/Department.Abstraction/StudentDepartment.Abstraction.csproj", "Services/Department/Department.Abstraction/"]
COPY ["Services/Department/ZA365Solutions.Platform.Common/ZA365Solutions.Platform.Common.csproj", "Services/Department/ZA365Solutions.Platform.Common/"]
COPY ["Services/Department/ZA365Solutions.Platform.Messaging/ZA365Solutions.Platform.Messaging.csproj", "Services/Department/ZA365Solutions.Platform.Messaging/"]
COPY ["Services/Department/Department.Infrastructure/StudentDepartment.Infrastructure.csproj", "Services/Department/Department.Infrastructure/"]
COPY ["Services/Department/Department.Core/StudentDepartment.Core.csproj", "Services/Department/Department.Core/"]
COPY ["Services/Courses/StudentCourse.Abstraction/StudentCourse.Abstraction.csproj", "Services/Courses/StudentCourse.Abstraction/"]
COPY ["Services/Courses/ZA365Solutions.Platform.Common/ZA365Solutions.Platform.Common.csproj", "Services/Courses/ZA365Solutions.Platform.Common/"]
COPY ["Services/Courses/ZA365Solutions.Platform.Messaging/ZA365Solutions.Platform.Messaging.csproj", "Services/Courses/ZA365Solutions.Platform.Messaging/"]
RUN dotnet restore "Services/Department/Department.API/StudentDepartment.API.csproj"
COPY . .
WORKDIR "/src/Services/Department/Department.API"
RUN dotnet build "StudentDepartment.API.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "StudentDepartment.API.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "StudentDepartment.API.dll"]
