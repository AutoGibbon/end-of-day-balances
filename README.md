# end-of-day-balances

Calculates end-of-day balances for a given json dataset.

# Assumptions made
- the dataset located in `\EndOfDayBalances\EndOfDayBalances\Data\Stores\store.json` is valid and complete
- all requests are authenticated and authorised

# Requirements for local development
- Visual Studio 2022
- Docker Desktop

# Assessing the results
- With the solution open in Visual Studio, ensure the Docker target is selected and click run.
- This will deploy the application image to your local docker and open the default swagger interface.
- For reference, the account Id in the provided dataset was 60241708
- If the account id given was not found, an appropriate response will be generated

# External resources used in creation 
- https://learn.microsoft.com/en-us/dotnet/architecture/containerized-lifecycle/design-develop-containerized-apps/build-aspnet-core-applications-linux-containers-aks-kubernetes
- https://learn.microsoft.com/en-us/dotnet/architecture/containerized-lifecycle/design-develop-containerized-apps/docker-apps-inner-loop-workflow
- https://learn.microsoft.com/en-us/aspnet/core/web-api/handle-errors?view=aspnetcore-7.0#use-exceptions-to-modify-the-response
- https://xunit.net/docs/getting-started/netcore/cmdline
- https://openbanking.atlassian.net/wiki/spaces/DZ/pages/128909480/Balances+v2.0.0
- https://docs.direct.id/#tag/Bank-Data/paths/~1data~1v2~1consents~1{consentId}~1accounts~1{accountId}~1balances/get

# Recommendations
- Add jwt based auth
  - secures the application
  - implements zero trust policy
- Replace json data store with a real storage solution i.e SqlServer and Entity Framework
  - Add data ingress functionality
- Develop a deployment strategy for this that will work with other existing services in the organisation
