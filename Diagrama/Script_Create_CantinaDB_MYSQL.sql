-- MySQL Script generated by MySQL Workbench
-- Fri Jun  7 09:09:58 2024
-- Model: New Model    Version: 1.0
-- MySQL Workbench Forward Engineering

SET @OLD_UNIQUE_CHECKS=@@UNIQUE_CHECKS, UNIQUE_CHECKS=0;
SET @OLD_FOREIGN_KEY_CHECKS=@@FOREIGN_KEY_CHECKS, FOREIGN_KEY_CHECKS=0;
SET @OLD_SQL_MODE=@@SQL_MODE, SQL_MODE='ONLY_FULL_GROUP_BY,STRICT_TRANS_TABLES,NO_ZERO_IN_DATE,NO_ZERO_DATE,ERROR_FOR_DIVISION_BY_ZERO,NO_ENGINE_SUBSTITUTION';

-- -----------------------------------------------------
-- Schema cantinaDB
-- -----------------------------------------------------
DROP SCHEMA IF EXISTS `cantinaDB` ;

-- -----------------------------------------------------
-- Schema cantinaDB
-- -----------------------------------------------------
CREATE SCHEMA IF NOT EXISTS `cantinaDB` DEFAULT CHARACTER SET utf8 ;
USE `cantinaDB` ;

-- -----------------------------------------------------
-- Table `cantinaDB`.`Categoria`
-- -----------------------------------------------------
CREATE TABLE IF NOT EXISTS `cantinaDB`.`Categoria` (
  `CategoriaID` INT NOT NULL AUTO_INCREMENT,
  `Nome` VARCHAR(45) NOT NULL,
  `DataCadastro` DATETIME NOT NULL,
  PRIMARY KEY (`CategoriaID`))
ENGINE = InnoDB;


-- -----------------------------------------------------
-- Table `cantinaDB`.`Produto`
-- -----------------------------------------------------
CREATE TABLE IF NOT EXISTS `cantinaDB`.`Produto` (
  `ProdutoID` INT NOT NULL AUTO_INCREMENT,
  `CategoriaID` INT NOT NULL,
  `Nome` VARCHAR(50) NOT NULL,
  `CodigoBarras` VARCHAR(50) NOT NULL,
  `PrecoVenda` DECIMAL(10,2) NOT NULL,
  `Estoque` INT NOT NULL,
  `DataCadastro` DATETIME NOT NULL,
  PRIMARY KEY (`ProdutoID`, `CategoriaID`),
  UNIQUE INDEX `CodigoBarras_UNIQUE` (`CodigoBarras` ASC),
  INDEX `fk_Produto_Categoria_idx` (`CategoriaID` ASC),
  CONSTRAINT `fk_Produto_Categoria`
    FOREIGN KEY (`CategoriaID`)
    REFERENCES `cantinaDB`.`Categoria` (`CategoriaID`)
    ON DELETE NO ACTION
    ON UPDATE NO ACTION)
ENGINE = InnoDB;


-- -----------------------------------------------------
-- Table `cantinaDB`.`Usuario`
-- -----------------------------------------------------
CREATE TABLE IF NOT EXISTS `cantinaDB`.`Usuario` (
  `UsuarioID` INT NOT NULL AUTO_INCREMENT,
  `NomeUsuario` VARCHAR(45) NOT NULL,
  `Senha` VARCHAR(200) NOT NULL,
  `DataCadastro` DATETIME NOT NULL,
  PRIMARY KEY (`UsuarioID`),
  UNIQUE INDEX `NomeUsuario_UNIQUE` (`NomeUsuario` ASC))
ENGINE = InnoDB;


-- -----------------------------------------------------
-- Table `cantinaDB`.`Caixa`
-- -----------------------------------------------------
CREATE TABLE IF NOT EXISTS `cantinaDB`.`Caixa` (
  `CaixaID` INT NOT NULL AUTO_INCREMENT,
  `UsuarioID` INT NOT NULL,
  `CodigoUnico` VARCHAR(45) NOT NULL,
  `DataAbertura` DATETIME NOT NULL,
  PRIMARY KEY (`CaixaID`, `UsuarioID`),
  UNIQUE INDEX `CodigoUnico_UNIQUE` (`CodigoUnico` ASC),
  INDEX `fk_Caixa_Usuario1_idx` (`UsuarioID` ASC),
  CONSTRAINT `fk_Caixa_Usuario1`
    FOREIGN KEY (`UsuarioID`)
    REFERENCES `cantinaDB`.`Usuario` (`UsuarioID`)
    ON DELETE NO ACTION
    ON UPDATE NO ACTION)
ENGINE = InnoDB;


-- -----------------------------------------------------
-- Table `cantinaDB`.`Venda`
-- -----------------------------------------------------
CREATE TABLE IF NOT EXISTS `cantinaDB`.`Venda` (
  `VendaID` INT NOT NULL AUTO_INCREMENT,
  `CaixaID` INT NOT NULL,
  `UsuarioID` INT NOT NULL,
  `ProdutoID` INT NOT NULL,
  `Quantidade` INT NOT NULL,
  `DataVenda` DATETIME NOT NULL,
  PRIMARY KEY (`VendaID`, `CaixaID`, `UsuarioID`, `ProdutoID`),
  INDEX `fk_Venda_Produto_idx` (`ProdutoID` ASC),
  INDEX `fk_Venda_Caixa1_idx` (`CaixaID` ASC, `UsuarioID` ASC),
  CONSTRAINT `fk_Venda_Produto`
    FOREIGN KEY (`ProdutoID`)
    REFERENCES `cantinaDB`.`Produto` (`ProdutoID`)
    ON DELETE NO ACTION
    ON UPDATE NO ACTION,
  CONSTRAINT `fk_Venda_Caixa1`
    FOREIGN KEY (`CaixaID` , `UsuarioID`)
    REFERENCES `cantinaDB`.`Caixa` (`CaixaID` , `UsuarioID`)
    ON DELETE NO ACTION
    ON UPDATE NO ACTION)
ENGINE = InnoDB;


-- -----------------------------------------------------
-- Table `cantinaDB`.`FluxoCaixa`
-- -----------------------------------------------------
CREATE TABLE IF NOT EXISTS `cantinaDB`.`FluxoCaixa` (
  `FluxoCaixaID` INT NOT NULL AUTO_INCREMENT,
  `CaixaID` INT NOT NULL,
  `UsuarioID` INT NOT NULL,
  `ValorAbertura` FLOAT NOT NULL,
  `DataAbertura` DATETIME NOT NULL,
  `ValorFechamento` FLOAT NOT NULL,
  `DataFechamento` DATETIME NOT NULL,
  `CaixaFechado` BIT NOT NULL,
  PRIMARY KEY (`FluxoCaixaID`, `CaixaID`, `UsuarioID`),
  INDEX `fk_FluxoCaixa_Caixa1_idx` (`CaixaID` ASC, `UsuarioID` ASC),
  CONSTRAINT `fk_FluxoCaixa_Caixa1`
    FOREIGN KEY (`CaixaID` , `UsuarioID`)
    REFERENCES `cantinaDB`.`Caixa` (`CaixaID` , `UsuarioID`)
    ON DELETE NO ACTION
    ON UPDATE NO ACTION)
ENGINE = InnoDB;


SET SQL_MODE=@OLD_SQL_MODE;
SET FOREIGN_KEY_CHECKS=@OLD_FOREIGN_KEY_CHECKS;
SET UNIQUE_CHECKS=@OLD_UNIQUE_CHECKS;
