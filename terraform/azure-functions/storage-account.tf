resource "random_string" "storage_account_name" {
  length  = 20
  special = false
  upper   = false
}

resource "azurerm_storage_account" "asa" {
  name                     = "${random_string.storage_account_name.result}"
  resource_group_name      = azurerm_resource_group.main.name
  location                 = azurerm_resource_group.main.location
  account_tier             = "Standard"
  account_replication_type = "LRS"
}
