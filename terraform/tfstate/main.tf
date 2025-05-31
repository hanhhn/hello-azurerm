locals {
  tags = {
    owner = "hanhhn"
  }
  location = "southeast asia"
}

resource "azurerm_resource_group" "main" {
  name     = "tf-state-rg"
  location = local.location
  tags     = local.tags
}


resource "random_string" "tfstate_account" {
  length  = 20
  special = false
  upper   = false
}

resource "azurerm_storage_account" "tfstate" {
  name                          = random_string.tfstate_account.result
  resource_group_name           = azurerm_resource_group.main.name
  location                      = azurerm_resource_group.main.location
  account_tier                  = "Standard"
  account_replication_type      = "LRS"
  public_network_access_enabled = true
}

resource "azurerm_storage_container" "tfstate" {
  name                  = "tfstate"
  storage_account_id    = azurerm_storage_account.tfstate.id
  container_access_type = "private"
}

output "storage_account" {
  value = azurerm_storage_account.tfstate.name
}
