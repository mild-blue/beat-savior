run:
	docker-compose up -d

clean:
	docker-compose down -v --remove-orphans

db-re-create:
	docker-compose rm -f -s -v db && docker-compose up -d db

run-be:
	dotnet run --project API/API.csproj

generate-stubs:
    # Local BE needs to be running in order for this to work
	cd ../frontend && yarn run generate-stubs-local
	git add ../frontend/swagger.json ../frontend/src/generated/
