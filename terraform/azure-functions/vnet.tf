locals {
  tags = {
    Owner = "hanhhn"
  }
  location = "southeast asia"
}

resource "azurerm_resource_group" "main" {
  name     = "${var.environment}-${var.product}-${var.service}-rg"
  location = local.location
  tags     = local.tags
}

resource "azurerm_virtual_network" "main" {
  name                = "${var.environment}-${var.product}-${var.service}-vnet"
  location            = azurerm_resource_group.main.location
  resource_group_name = azurerm_resource_group.main.name
  address_space       = ["11.0.0.0/24"]
  tags                = local.tags
}

resource "azurerm_subnet" "public" {
  name                 = "public-subnet"
  address_prefixes     = ["11.0.0.0/25"]
  resource_group_name  = azurerm_resource_group.main.name
  virtual_network_name = azurerm_virtual_network.main.name
}

resource "azurerm_subnet" "private" {
  name                 = "private-subnet"
  address_prefixes     = ["11.0.0.128/26"]
  resource_group_name  = azurerm_resource_group.main.name
  virtual_network_name = azurerm_virtual_network.main.name

}

resource "azurerm_subnet" "privateendpoint" {
  name                 = "private-endpoint-subnet"
  address_prefixes     = ["11.0.0.192/26"]
  resource_group_name  = azurerm_resource_group.main.name
  virtual_network_name = azurerm_virtual_network.main.name
}

resource "azurerm_network_security_group" "public" {
  name                = "${var.environment}-${var.product}-${var.service}-public-nsg"
  location            = azurerm_resource_group.main.location
  resource_group_name = azurerm_resource_group.main.name
  tags                = local.tags

  security_rule {
    name                       = "Allow-80"
    priority                   = 100
    direction                  = "Inbound"
    access                     = "Allow"
    protocol                   = "Tcp"
    source_address_prefix      = "*"
    source_port_range          = "*"
    destination_port_range     = "80"
    destination_address_prefix = "*"
  }

  security_rule {
    name                       = "Allow-3389"
    priority                   = 101
    direction                  = "Inbound"
    access                     = "Allow"
    protocol                   = "Tcp"
    source_address_prefix      = "*"
    source_port_range          = "*"
    destination_port_range     = "3389"
    destination_address_prefix = "*"
  }

  security_rule {
    name                       = "DenyAllInbound"
    priority                   = 200
    direction                  = "Inbound"
    access                     = "Deny"
    protocol                   = "*"
    source_address_prefix      = "*"
    source_port_range          = "*"
    destination_port_range     = "*"
    destination_address_prefix = "*"
  }
}

resource "azurerm_network_security_group" "private" {
  name                = "${var.environment}-${var.product}-${var.service}-private-nsg"
  location            = azurerm_resource_group.main.location
  resource_group_name = azurerm_resource_group.main.name
  tags                = local.tags

  security_rule {
    name                       = "Allow80"
    priority                   = 100
    direction                  = "Inbound"
    access                     = "Allow"
    protocol                   = "Tcp"
    source_address_prefix      = "11.0.0.0/25"
    source_port_range          = "*"
    destination_port_range     = "80"
    destination_address_prefix = "*"
  }

}

resource "azurerm_network_security_group" "privateendpoint" {
  name                = "${var.environment}-${var.product}-${var.service}-privateendpoint-nsg"
  location            = azurerm_resource_group.main.location
  resource_group_name = azurerm_resource_group.main.name
  tags                = local.tags

  security_rule {
    name                       = "Allow80"
    priority                   = 100
    direction                  = "Inbound"
    access                     = "Allow"
    protocol                   = "Tcp"
    source_address_prefix      = "11.0.0.0/25"
    source_port_range          = "*"
    destination_port_range     = "80"
    destination_address_prefix = "*"
  }
}

resource "azurerm_subnet_network_security_group_association" "public" {
  network_security_group_id = azurerm_network_security_group.public.id
  subnet_id                 = azurerm_subnet.public.id
}

resource "azurerm_subnet_network_security_group_association" "private" {
  network_security_group_id = azurerm_network_security_group.private.id
  subnet_id                 = azurerm_subnet.private.id
}

resource "azurerm_subnet_network_security_group_association" "privateendpoint" {
  network_security_group_id = azurerm_network_security_group.privateendpoint.id
  subnet_id                 = azurerm_subnet.privateendpoint.id
}
