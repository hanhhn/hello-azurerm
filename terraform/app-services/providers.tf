terraform {
  required_providers {
    azurerm = {
      source  = "hashicorp/azurerm"
      version = "~> 4.30.0"
    }
  }

  backend "azurerm" {
    access_key           = ""
    resource_group_name  = "tf-state-rg"
    storage_account_name = "7poz3qymt1bg4h7mr4wb"
    container_name       = "tfstate"
    key                  = "az204.terraform.tfstate"
  }
}

# Configure the Microsoft Azure Provider
provider "azurerm" {
  subscription_id = "e61618b6-b3a6-46f6-a674-5ebe5fc6feaf"
  features {}
}
