resource "azurerm_service_plan" "linux" {
  name                = "${var.environment}-${var.product}-${var.service}-asp"
  location            = azurerm_resource_group.main.location
  resource_group_name = azurerm_resource_group.main.name
  os_type             = "Linux"
  sku_name            = "P0v3"

  worker_count = 1
}

resource "azurerm_linux_function_app" "dotnet" {
  name                       = "${var.environment}-${var.product}-${var.service}-func"
  resource_group_name        = azurerm_resource_group.main.name
  location                   = azurerm_resource_group.main.location
  storage_account_name       = azurerm_storage_account.asa.name
  storage_account_access_key = azurerm_storage_account.asa.primary_access_key
  service_plan_id            = azurerm_service_plan.linux.id

  site_config {
    application_stack {
      dotnet_version = "8.0"
    }
  }
}
