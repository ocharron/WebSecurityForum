# Web Security Forum - D�monstration des Pratiques de Codage S�curis�

[![en](https://img.shields.io/badge/lang-en-red.svg)](https://github.com/ocharron/WebSecurityForum/blob/master/README.md)
[![fr](https://img.shields.io/badge/lang-fr-blue.svg)](https://github.com/ocharron/WebSecurityForum/blob/master/README_fr.md)  

Le projet **Web Security Forum** est une application de forum con�ue pour d�montrer des pratiques de codage s�curis�es et des protections contre les vuln�rabilit�s Web courantes. Ce projet pr�sente des techniques avanc�es de s�curit� Web utilisant ASP.NET Core MVC.

---

## Technologies utilis�es

- **Framework** : ASP.NET Core (.NET 8.0)  
- **Base de donn�es** : Microsoft SQL Server 2022  
- **ORM** : Entity Framework Core  
- **Authentification** : ASP.NET Core Identity  

---

## Installation

Pour installer et ex�cuter **Forum de S�curit� Web** sur votre machine locale, suivez ces �tapes :

### Pr�requis

- [.NET 8.0 SDK](https://dotnet.microsoft.com/download/dotnet/8.0)  
- [Visual Studio](https://visualstudio.microsoft.com/downloads/) ou l'�diteur de texte de votre choix  
- [Microsoft SQL Server 2022](https://www.microsoft.com/en-ca/sql-server/sql-server-downloads) (ou version ult�rieure)  

### Instructions

1. **Cloner le d�p�t**
   ```bash
   git clone https://github.com/ocharron/WebSecurityForum.git
   ```

2. **Configuration de la base de donn�es**
   - Cr�ez une nouvelle base de donn�es dans SQL Server.
   - Mettez � jour la cha�ne de connexion dans le fichier `appsettings.json` avec les d�tails de votre base de donn�es.
   - Ex�cutez ensuite une migration pour mettre en place le sch�ma de base de donn�es:
     ```bash
     dotnet ef migrations add <MigrationName>
     dotnet ef database update
     ```

3. **Compilation et ex�cution**
   - Ouvrez le projet dans Visual Studio ou utilisez la ligne de commande.
   - Ex�cutez la commande suivante pour restaurer les d�pendances :
     ```bash
     dotnet restore
     ```
   - Ensuite, ex�cutez l'application :
     ```bash
     dotnet run
     ```

---

## Fonctionnalit�s Principales

1. **Authentification s�curis�e** : Impl�mentation d'ASP.NET Identity pour une authentification utilisateur s�curis�e.
2. **Autorisation bas�e sur les r�les** : Gestion des r�les et permissions utilisateur pour prot�ger les fonctionnalit�s sensibles.
3. **Protection contre les vuln�rabilit�s** : D�fense contre l'injection SQL, les attaques XSS et CSRF.
4. **Journalisation et surveillance** : Enregistrement des activit�s de connexion et des comportements suspects.
5. **Gestion des erreurs** : Gestion personnalis�e des exceptions pour �viter l'exposition d'informations sensibles.

---

## Auteur

Ce projet a �t� d�velopp� par [Olivier Charron](https://github.com/ocharron).