# resource "random_string" "cosmosdb_account_name" {
#   length  = 20
#   special = false
#   upper   = false
# }

# resource "azurerm_cosmosdb_account" "cosmos" {
#   name                = random_string.cosmosdb_account_name.result
#   location            = azurerm_resource_group.main.location
#   resource_group_name = azurerm_resource_group.main.name
#   offer_type          = "Standard"
#   kind                = "MongoDB"

#   automatic_failover_enabled = true

#   capabilities {
#     name = "EnableAggregationPipeline"
#   }

#   capabilities {
#     name = "mongoEnableDocLevelTTL"
#   }

#   capabilities {
#     name = "MongoDBv3.4"
#   }

#   capabilities {
#     name = "EnableMongo"
#   }

#   consistency_policy {
#     consistency_level       = "Session"
#     max_interval_in_seconds = 300
#     max_staleness_prefix    = 100000
#   }

#   geo_location {
#     location          = "eastus"
#     failover_priority = 1
#   }

#   geo_location {
#     location          = "westus"
#     failover_priority = 0
#   }
# }

# resource "azurerm_cosmosdb_mongo_database" "mongo" {
#   name                = "${var.environment}-${var.product}-${var.service}-mongo-db"
#   resource_group_name = azurerm_cosmosdb_account.cosmos.resource_group_name
#   account_name        = azurerm_cosmosdb_account.cosmos.name
#   throughput          = 400
# }


# resource "azurerm_cosmosdb_mongo_collection" "test" {
#   name                = "test1"
#   resource_group_name = azurerm_cosmosdb_account.cosmos.resource_group_name
#   account_name        = azurerm_cosmosdb_account.cosmos.name
#   database_name       = azurerm_cosmosdb_mongo_database.mongo.name

#   default_ttl_seconds = "777"
#   shard_key           = "uniqueKey"
#   throughput          = 400

#   index {
#     keys   = ["_id"]
#     unique = true
#   }
# }

# resource "azurerm_cosmosdb_mongo_collection" "test2" {
#   name                = "test2"
#   resource_group_name = azurerm_cosmosdb_account.cosmos.resource_group_name
#   account_name        = azurerm_cosmosdb_account.cosmos.name
#   database_name       = azurerm_cosmosdb_mongo_database.mongo.name

#   default_ttl_seconds = "777"
#   shard_key           = "uniqueKey"
#   throughput          = 400

#   index {
#     keys   = ["_id"]
#     unique = true
#   }
# }
