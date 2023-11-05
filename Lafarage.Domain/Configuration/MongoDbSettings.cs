using System;
namespace Lafarage.Domain.Configuration;

public partial class MongoDbSettings
{
    public string DatabaseName { get; set; }
    public string ConnectionString { get; set; }
}