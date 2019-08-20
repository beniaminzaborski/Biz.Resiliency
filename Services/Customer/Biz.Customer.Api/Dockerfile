FROM microsoft/dotnet:2.2-aspnetcore-runtime AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM microsoft/dotnet:2.2-sdk AS build
WORKDIR /src
COPY ["Services/Customer/Biz.Customer.Api/Biz.Customer.Api.csproj", "Services/Customer/Biz.Customer.Api/"]
RUN dotnet restore "Services/Customer/Biz.Customer.Api/Biz.Customer.Api.csproj"
COPY . .
WORKDIR "/src/Services/Customer/Biz.Customer.Api"
RUN dotnet build "Biz.Customer.Api.csproj" -c Release -o /app

FROM build AS publish
RUN dotnet publish "Biz.Customer.Api.csproj" -c Release -o /app

FROM base AS final
WORKDIR /app
COPY --from=publish /app .
ENTRYPOINT ["dotnet", "Biz.Customer.Api.dll"]