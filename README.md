# ElasticSearchSample

The `ElasticSearchSample` repository demonstrates how to integrate Elasticsearch into a .NET application using the Elasticsearch .NET Client. It provides a foundational structure for performing CRUD operations and implementing search functionality with Elasticsearch.

## Features

- **Generic Repository Pattern**: Implements a base repository to handle common Elasticsearch operations for any document type.
- **CRUD Operations**: Supports Create, Read, Update, and Delete functionalities for Elasticsearch documents.
- **Asynchronous Methods**: Utilizes asynchronous programming to ensure non-blocking operations.
- **Dependency Injection**: Designed for seamless integration with ASP.NET Core's dependency injection system.

## Prerequisites

- **.NET 6.0 SDK or later**: Ensure you have the .NET SDK installed.
- **Elasticsearch Instance**: A running Elasticsearch instance is required. You can set up a local instance using Docker:

  ```bash
  docker run -d -p 9200:9200 -e "discovery.type=single-node" elasticsearch:8.17.0
  ```

## Getting Started

1. **Clone the Repository**:

   ```bash
   git clone https://github.com/carsimoes/ElasticSearchSample.git
   cd ElasticSearchSample
   ```

2. **Restore Dependencies**:

   Navigate to the project directory and restore the necessary packages:

   ```bash
   dotnet restore
   ```

3. **Update Configuration**:

   Modify the `appsettings.json` file to include your Elasticsearch configuration:

   ```json
   {
     "Elasticsearch": {
       "Url": "http://localhost:9200",
       "Username": "your_username",
       "Password": "your_password"
     }
   }
   ```

4. **Run the Application**:

   Start the application using:

   ```bash
   dotnet run
   ```

## Usage

The repository includes a `BaseElasticSearchRepository<T>` class that serves as a generic base for Elasticsearch operations. To create a repository for a specific document type, inherit from this base class:

```csharp
public class ProductRepository : BaseElasticSearchRepository<Product>
{
    public ProductRepository(ElasticsearchClient client) : base(client)
    {
    }

    // Additional methods specific to Product can be added here
}
```

This setup allows you to perform CRUD operations and searches on `Product` documents efficiently.

## Contributing

Contributions are welcome! Feel free to open issues or submit pull requests to enhance the functionality or fix bugs.

## License

This project is licensed under the MIT License. See the [LICENSE](LICENSE) file for details.

## Acknowledgments

- [Elasticsearch .NET Client Documentation](https://www.elastic.co/guide/en/elasticsearch/client/net-api/current/index.html)

This project provides a solid foundation for integrating Elasticsearch with .NET applications, demonstrating best practices and offering a starting point for further development.

