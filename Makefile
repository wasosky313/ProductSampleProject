revision:
	# to use <makefile revision name="name of new migratio">
	dotnet ef migrations add "${name}" --project AdminStore.Infrastructure --startup-project AdminStore.API

migrate:
	dotnet ef database update --project AdminStore.Infrastructure --startup-project AdminStore.API

