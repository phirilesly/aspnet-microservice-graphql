#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:5.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:5.0 AS build
WORKDIR /src
COPY ["Services/Courses/StudentCourse.API/StudentCourse.API.csproj", "Services/Courses/StudentCourse.API/"]
COPY ["Services/Courses/StudentCourse.Infrastructure/StudentCourse.Infrastructure.csproj", "Services/Courses/StudentCourse.Infrastructure/"]
COPY ["Services/Department/Department.Core/StudentDepartment.Core.csproj", "Services/Department/Department.Core/"]
COPY ["Services/Courses/StudentCourse.Abstraction/StudentCourse.Abstraction.csproj", "Services/Courses/StudentCourse.Abstraction/"]
COPY ["Services/Courses/ZA365Solutions.Platform.Common/ZA365Solutions.Platform.Common.csproj", "Services/Courses/ZA365Solutions.Platform.Common/"]
COPY ["Services/Courses/ZA365Solutions.Platform.Messaging/ZA365Solutions.Platform.Messaging.csproj", "Services/Courses/ZA365Solutions.Platform.Messaging/"]
COPY ["Services/Department/Department.Abstraction/StudentDepartment.Abstraction.csproj", "Services/Department/Department.Abstraction/"]
COPY ["Services/Department/ZA365Solutions.Platform.Common/ZA365Solutions.Platform.Common.csproj", "Services/Department/ZA365Solutions.Platform.Common/"]
COPY ["Services/Department/ZA365Solutions.Platform.Messaging/ZA365Solutions.Platform.Messaging.csproj", "Services/Department/ZA365Solutions.Platform.Messaging/"]
COPY ["Services/Courses/StudentCourse.Core/StudentCourse.Core.csproj", "Services/Courses/StudentCourse.Core/"]
RUN dotnet restore "Services/Courses/StudentCourse.API/StudentCourse.API.csproj"
COPY . .
WORKDIR "/src/Services/Courses/StudentCourse.API"
RUN dotnet build "StudentCourse.API.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "StudentCourse.API.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "StudentCourse.API.dll"]
