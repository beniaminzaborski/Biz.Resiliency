FROM microsoft/dotnet:2.2-aspnetcore-runtime AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM microsoft/dotnet:2.2-sdk AS build
WORKDIR /src
COPY ["Services/Product/Biz.Product.Api/Biz.Product.Api.csproj", "Services/Product/Biz.Product.Api/"]
RUN dotnet restore "Services/Product/Biz.Product.Api/Biz.Product.Api.csproj"
COPY . .
WORKDIR "/src/Services/Product/Biz.Product.Api"
RUN dotnet build "Biz.Product.Api.csproj" -c Release -o /app

FROM build AS publish
RUN dotnet publish "Biz.Product.Api.csproj" -c Release -o /app

FROM base AS final
WORKDIR /app
COPY --from=publish /app .
ENTRYPOINT ["dotnet", "Biz.Product.Api.dll"]