resource "azurerm_service_plan" "linux" {
  name                = "${var.environment}-${var.product}-${var.service}-asp"
  location            = azurerm_resource_group.main.location
  resource_group_name = azurerm_resource_group.main.name
  os_type             = "Linux"
  sku_name            = "P0v3"

  worker_count = 1
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

    app_command_line = "cd /home/site/wwwroot && npm install && node main.js"
  }
}

resource "azurerm_linux_web_app_slot" "staging" {
  name           = "staging"
  app_service_id = azurerm_linux_web_app.linuxweb.id

  site_config {
    application_stack {
      node_version = "22-lts"
    }

    app_command_line = "cd /home/site/wwwroot && npm install && node main.js"
  }
}



