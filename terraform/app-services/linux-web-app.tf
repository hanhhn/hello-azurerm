resource "azurerm_service_plan" "linux" {
  name                = "${var.environment}-${var.product}-${var.service}-asp"
  location            = azurerm_resource_group.main.location
  resource_group_name = azurerm_resource_group.main.name
  os_type             = "Linux"
  sku_name            = "P0v3"
}

resource "azurerm_linux_web_app" "linuxweb" {
  name                = "${var.environment}-${var.product}-${var.service}-web-app"
  location            = azurerm_resource_group.main.location
  resource_group_name = azurerm_resource_group.main.name
  service_plan_id     = azurerm_service_plan.linux.id

  site_config {
    application_stack {
      node_version = "22-lts"
    }

    app_command_line = "node /home/site/wwwroot/main.js"
  }

}

