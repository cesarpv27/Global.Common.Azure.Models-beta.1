# nuget Global.Common.Azure.Models

# namespace Global.Common.Azure.Models

# Description

This project contains several features that provide functionalities to support and enhance development on Azure platform. All methods are documented.
One important feature is that it contains a generic Azure responses hierarchy, which allows you to manage your development in the most common scenarios.
This is a beta version and has not been thoroughly tested or recommended for use in production environments.
All methods are documented.


# Dependencies
- https://www.nuget.org/packages/Global.Common.Features.Models
- https://www.nuget.org/packages/Azure.Data.Tables
- https://www.nuget.org/packages/Azure.Storage.Blobs

# Features

## Generic responses hierarchy

- GlobalResponse<TEx> (from nuget Global.Common.Features.Models)
	- AzGlobalResponse<TAzureResponse, TEx>
		- AzTableResponse<TEx>
			- AzTableResponse
		- AzBlobResponse<TEx>
			- AzBlobResponse
		- AzGlobalValueResponse<T, TAzureResponse, TEx>
			- AzTableValueResponse<T, TEx>
				- AzTableValueResponse<T>
			- AzBlobValueResponse<T, TEx>
				- AzBlobValueResponse<T>

# Examples
For additional examples, see the repository: https://github.com/cesarpv27/Global.Logging-beta.1