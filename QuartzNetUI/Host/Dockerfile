#��������������������
FROM microsoft/dotnet:2.1-aspnetcore-runtime AS base
WORKDIR /app
EXPOSE 80
ENV TZ=Asia/Shanghai
RUN cp /usr/share/zoneinfo/Asia/Shanghai /usr/share/zoneinfo/Asia/Beijing
RUN ln -snf /usr/share/zoneinfo/$TZ /etc/localtime && echo $TZ > /etc/timezone

#���루��ʱ������Ҫ�������뷢����Ŀ��
FROM microsoft/dotnet:2.1-sdk AS publish
WORKDIR /src
COPY . .
WORKDIR /src/Host
RUN dotnet publish -c Release -o /app

#��������
FROM base AS final
WORKDIR /app
COPY --from=publish /app .
CMD ["dotnet", "Host.dll"]