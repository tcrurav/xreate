# XREATE digital security training lab
[![en](https://img.shields.io/badge/lang-en-red.svg)](README.md)
<!-- [![es](https://img.shields.io/badge/lang-es-yellow.svg)](README.es.md) -->

This project is a first draft for a training lab in the XREATE project.

At this moment 
  - Backend is working.
  - Unity frontend has just the login and the model of the spaceship in the main scene.

[Read more about XREATE](docs/xreate-project-description.en.md)

## Getting Started

Clone this respository.

```
git clone https://github.com/tcrurav/xreate.git
```

* In the backend:

create a backend/.env file for the backend. This is an example:

```
JWT_SECRET=V3RY#1MP0RT@NT$3CR3T#

MYSQL_DATABASE=db_xreate_dev
MYSQL_USER=root
MYSQL_PASSWORD=sasa
MYSQL_ROOT_PASSWORD=sasa

ADMIN_USER=admin
ADMIN_PASSWORD=sasa

DB_HOST=localhost
PORT=8080

NODE_ENV=development
```

Create the database ```db_xreate_dev``` in your MySQL Server.

Install dependencies and run the backend:

```
cd backend
npm install
node server.js
```

In a new console execute the seeders for the backend:

```
cd backend
npx sequelize-cli db:seed:all
```

* In the frontend (Unity):

Download the 2 folders in this Google drive folder (https://drive.google.com/drive/u/0/folders/1cQXp--uMSGzDGwSNyM7TaGHgkyqevYjI) and insert them into frontend/Assets 


Enjoy!

## Postman
* You can import the following Postman end-points and environment to try the backend: 
  - [postman collection](postman/xreate.postman_collection.json).
  - [postman environment](postman/metaverse.postman_environment.json).
* You can also access de postman end-points online: 
  - [postman collection online](https://documenter.getpostman.com/view/3446841/2sAYBPmZqP).

## Prerequisites

All you need is... some time and...
* Visual Studio Code.
* Git.
* MySQL Server.

## Entity Relationship diagram
![Entity Relationship diagram](docs/screenshots/screenshot-01-EntityRelationship_Diagram.png)

## General Use Case diagram
![General Use Case diagram](docs/screenshots/screenshot-02-General_UseCase_Diagram.png)

## Some definitions

* **Activity**. An activity is a set of students and teachers who work on a scaperoom activity made of a set of challenges. The activity has a start and an end date. 
* **Challenge**. A challenge can be either a room or a corridor task.
* **Team**. A team is a set of students playing together against other student teams.
* **Achievement**. An achievement is the result of the challenge for a student. The achievement can be divided into achievement items. For example in a scene a student X could have a total achievement of 11 points. This achievement could be devided in 5 achievement items: 3 card pairs guessed which means 5 points each, and 2 minutes of thinking time in his turns which means -2 points each. The total achievement for this student X in this challenge would be 3 x 5 – 2 x 2 = 11 points.
* **Team Achievement**. It’s the sum of team members achievements.

## Role descriptions

* **Admin**. Manages accounts. CRUD of accounts. CRUD means Create, Read, Update and Delete.
* **Activity Manager**. Manages activities. An activity is a set of students and teachers who work on a scaperoom activity made of a set of challenges. The activity has a start and end date. The activity manager can manage a CRUD of activities, adding teachers, students and challenges (rooms or corridors). An activity manager says when the activity starts and finishes.
* **Student**. uses a code to join an activity. Once joined can change his nickname and join a team. Students also solve the challenges (rooms or corridor tasks) together with his team.
* **Teacher**. join as a teacher in an activity using his code. Join challenge tasks for teachers.
* **Guest**. joins as guest in an activity using his code. Guests can go anywhere and observe everything.
* **Room manager**. can enter room data like questions, complexity of scene tasks, etc.

## IES El Rincón students working on the project

At the moment 6 students are working on the project. Students will work by pairs developing one scene each team.

## Built With

* [Visual Studio Code](https://code.visualstudio.com/) - The Editor used in this project.
* [Express](https://code.visualstudio.com/) - Fast, unopinionated, minimalist web framework for Node.js.
* [Sequelize](https://sequelize.org/) - Sequelize is a modern TypeScript and Node.js ORM.
* [Unity](https://unity.com/) - Unity is a cross-platform 2D and 3D graphics engine.

## Acknowledgments

* https://gist.github.com/PurpleBooth/109311bb0361f32d87a2. A very complete template for README.md files.
* https://sequelize.org/docs/v6/other-topics/migrations/. Sequelize link to read about seeders.
* https://github.com/tcrurav/UnityWebRequestExpressSequelize. Example project that shows a Unity CRUD consuming an API with Express + Sequelize + MySQL.