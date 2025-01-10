# Web Security Forum - Démonstration des Pratiques de Codage Sécurisé

[![en](https://img.shields.io/badge/lang-en-red.svg)](https://github.com/ocharron/WebSecurityForum/blob/master/README.md)
[![fr](https://img.shields.io/badge/lang-fr-blue.svg)](https://github.com/ocharron/WebSecurityForum/blob/master/README_fr.md)  

Le projet **Web Security Forum** est une application de forum conçue pour démontrer des pratiques de codage sécurisées et des protections contre les vulnérabilités Web courantes. Ce projet présente des techniques avancées de sécurité Web utilisant ASP.NET Core MVC.

---

## Technologies utilisées

- **Framework** : ASP.NET Core (.NET 8.0)  
- **Base de données** : Microsoft SQL Server 2022  
- **ORM** : Entity Framework Core  
- **Authentification** : ASP.NET Core Identity  

---

## Installation

Pour installer et exécuter **Forum de Sécurité Web** sur votre machine locale, suivez ces étapes :

### Prérequis

- [.NET 8.0 SDK](https://dotnet.microsoft.com/download/dotnet/8.0)  
- [Visual Studio](https://visualstudio.microsoft.com/downloads/) ou l'éditeur de texte de votre choix  
- [Microsoft SQL Server 2022](https://www.microsoft.com/en-ca/sql-server/sql-server-downloads) (ou version ultérieure)  

### Instructions

1. **Cloner le dépôt**
   ```bash
   git clone https://github.com/ocharron/WebSecurityForum.git
   ```

2. **Configuration de la base de données**
   - Créez une nouvelle base de données dans SQL Server.
   - Mettez à jour la chaîne de connexion dans le fichier `appsettings.json` avec les détails de votre base de données.
   - Exécutez ensuite une migration pour mettre en place le schéma de base de données:
     ```bash
     dotnet ef migrations add <MigrationName>
     dotnet ef database update
     ```

3. **Compilation et exécution**
   - Ouvrez le projet dans Visual Studio ou utilisez la ligne de commande.
   - Exécutez la commande suivante pour restaurer les dépendances :
     ```bash
     dotnet restore
     ```
   - Ensuite, exécutez l'application :
     ```bash
     dotnet run
     ```

---

## Fonctionnalités Principales

1. **Authentification sécurisée** : Implémentation d'ASP.NET Identity pour une authentification utilisateur sécurisée.
2. **Autorisation basée sur les rôles** : Gestion des rôles et permissions utilisateur pour protéger les fonctionnalités sensibles.
3. **Protection contre les vulnérabilités** : Défense contre l'injection SQL, les attaques XSS et CSRF.
4. **Journalisation et surveillance** : Enregistrement des activités de connexion et des comportements suspects.
5. **Gestion des erreurs** : Gestion personnalisée des exceptions pour éviter l'exposition d'informations sensibles.

---

## Auteur

Ce projet a été développé par [Olivier Charron](https://github.com/ocharron).