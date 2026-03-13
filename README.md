# hm-staff-service

Independent microservice repository for Hospital Management.

## Local run

`ash
dotnet restore
dotnet build
dotnet run --project src/StaffService.Api/StaffService.Api.csproj
`

## Docker

`ash
docker build -t hm-staff-service:local .
docker run -p 8087:8080 hm-staff-service:local
`

## GitHub setup later

`ash
git branch -M main
git remote add origin <your-github-repo-url>
git add .
git commit -m "Initial scaffold"
git push -u origin main
`
