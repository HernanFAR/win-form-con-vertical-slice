# Windows Forms con vertical slices y metodolog�a Scrum
En este repositorio se encuentra el c�digo de ejemplo de como emplear vertical slices en una aplicaci�n de Windows Forms, as� como la metodolog�a Scrum para la gesti�n de proyectos.


## Sobre el sistema
Para entender el sistema, hay que tener en cuenta que es un sistema basico, orientado a la gesti�n de preguntas. 

Teniendo eso en consideraci�n, continuemos.

## �C�mo se hizo el levantamiento?
El levantamiento se realizo con la metodolog�a Scrum, por lo que se tiene un documento de historias de usuario, en la ra�z del sistema. All� esta el detalle de las mismas, as� como los escenarios y criterios de aceptaci�n de cada uno. 

Este documento cuenta como el alcance del proyecto, si bien esta hecho en .docx, lo recomendado es hacerlo con herramientas que permitan el seguimiento de las historias de usuario, como Azure DevOps, Jira, Trello, etc.

## Arquitectura del proyecto

El proyecto esta basado en la siguiente arquitectura, de nombre vertical Slices:

![Arquitectura](./Imagenes/Arquitectura.png)

Cada historia de usuario es una funcionalidad, autocontenida y con baja cohesi�n una de otras. 

Unicamente se comparten los elementos de corte com�n, como son la validaci�n, el registro de 
eventos, el manejo de excepciones, acceso a base de datos, etc.

Esto, esta implementado en 4 proyectos:
* WinFormsSample: Es el proyecto principal de la aplicaci�n, presenta la interfaz de 
usuario empleando la tecnologia Windows Forms
* L�gica: Es el proyecto que contiene la l�gica de negocio de la aplicaci�n, dividida en
funcionalidades
* Corte Com�n: Es el proyecto que contiene los elementos de corte com�n de la aplicaci�n:
* Dominio: Es el proyecto que contiene las entidades de dominio de la aplicaci�n
