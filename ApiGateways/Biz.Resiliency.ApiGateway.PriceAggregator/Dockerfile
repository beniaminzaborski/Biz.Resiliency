FROM microsoft/dotnet:2.2-aspnetcore-runtime AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM microsoft/dotnet:2.2-sdk AS build
WORKDIR /src
COPY ["ApiGateways/Biz.Resiliency.ApiGateway.PriceAggregator/Biz.Resiliency.ApiGateway.PriceAggregator.csproj", "ApiGateways/Biz.Resiliency.ApiGateway.PriceAggregator/"]
RUN dotnet restore "ApiGateways/Biz.Resiliency.ApiGateway.PriceAggregator/Biz.Resiliency.ApiGateway.PriceAggregator.csproj"
COPY . .
WORKDIR "/src/ApiGateways/Biz.Resiliency.ApiGateway.PriceAggregator"
RUN dotnet build "Biz.Resiliency.ApiGateway.PriceAggregator.csproj" -c Release -o /app

FROM build AS publish
RUN dotnet publish "Biz.Resiliency.ApiGateway.PriceAggregator.csproj" -c Release -o /app

FROM base AS final
WORKDIR /app
COPY --from=publish /app .
ENTRYPOINT ["dotnet", "Biz.Resiliency.ApiGateway.PriceAggregator.dll"]