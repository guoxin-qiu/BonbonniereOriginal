FROM microsoft/aspnetcore:latest
WORKDIR /publish
COPY /publish /publish
EXPOSE 80
ENTRYPOINT ["dotnet", "Bonbonniere.Website.dll"]
