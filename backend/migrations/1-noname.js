'use strict';

var Sequelize = require('sequelize');

/**
 * Actions summary:
 *
 * createTable "Users", deps: []
 * createTable "Activities", deps: []
 * createTable "Challenges", deps: []
 * createTable "Teams", deps: [Challenges]
 * createTable "InActivityStudentParticipations", deps: [Teams, Activities, Users]
 * createTable "ChallengeItems", deps: [Challenges]
 * createTable "ActivityChallengeConfigs", deps: [Activities, Challenges]
 * createTable "ActivityChallengeConfigItems", deps: [ActivityChallengeConfigs]
 * createTable "Achievements", deps: [Challenges, InActivityStudentParticipations]
 * createTable "InActivityTeacherParticipations", deps: [Challenges, Activities, Users]
 * createTable "AchievementItems", deps: [Achievements, ChallengeItems]
 *
 **/

var info = {
    "revision": 1,
    "name": "noname",
    "created": "2024-12-30T14:22:55.189Z",
    "comment": ""
};

var migrationCommands = function(transaction) {
    return [{
            fn: "createTable",
            params: [
                "Users",
                {
                    "id": {
                        "type": Sequelize.INTEGER,
                        "field": "id",
                        "autoIncrement": true,
                        "primaryKey": true,
                        "allowNull": false
                    },
                    "username": {
                        "type": Sequelize.STRING,
                        "field": "username"
                    },
                    "password": {
                        "type": Sequelize.STRING,
                        "field": "password"
                    },
                    "nickname": {
                        "type": Sequelize.STRING,
                        "field": "nickname"
                    },
                    "code": {
                        "type": Sequelize.STRING,
                        "field": "code"
                    },
                    "role": {
                        "type": Sequelize.STRING,
                        "field": "role"
                    },
                    "nationality": {
                        "type": Sequelize.STRING,
                        "field": "nationality"
                    },
                    "createdAt": {
                        "type": Sequelize.DATE,
                        "field": "createdAt",
                        "defaultValue": Sequelize.Date,
                        "allowNull": false
                    },
                    "updatedAt": {
                        "type": Sequelize.DATE,
                        "field": "updatedAt",
                        "defaultValue": Sequelize.Date,
                        "allowNull": false
                    }
                },
                {
                    "transaction": transaction
                }
            ]
        },
        {
            fn: "createTable",
            params: [
                "Activities",
                {
                    "id": {
                        "type": Sequelize.INTEGER,
                        "field": "id",
                        "autoIncrement": true,
                        "primaryKey": true,
                        "allowNull": false
                    },
                    "startDate": {
                        "type": Sequelize.DATE,
                        "field": "startDate"
                    },
                    "endDate": {
                        "type": Sequelize.DATE,
                        "field": "endDate"
                    },
                    "state": {
                        "type": Sequelize.STRING,
                        "field": "state",
                        "defaultValue": "NOT_STARTED",
                        "allowNull": false
                    },
                    "type": {
                        "type": Sequelize.STRING,
                        "field": "type",
                        "defaultValue": "TRAINNING_LAB",
                        "allowNull": false
                    },
                    "name": {
                        "type": Sequelize.STRING,
                        "field": "name"
                    },
                    "description": {
                        "type": Sequelize.STRING,
                        "field": "description"
                    },
                    "createdAt": {
                        "type": Sequelize.DATE,
                        "field": "createdAt",
                        "defaultValue": Sequelize.Date,
                        "allowNull": false
                    },
                    "updatedAt": {
                        "type": Sequelize.DATE,
                        "field": "updatedAt",
                        "defaultValue": Sequelize.Date,
                        "allowNull": false
                    }
                },
                {
                    "transaction": transaction
                }
            ]
        },
        {
            fn: "createTable",
            params: [
                "Challenges",
                {
                    "id": {
                        "type": Sequelize.INTEGER,
                        "field": "id",
                        "autoIncrement": true,
                        "primaryKey": true,
                        "allowNull": false
                    },
                    "name": {
                        "type": Sequelize.STRING,
                        "field": "name"
                    },
                    "type": {
                        "type": Sequelize.STRING,
                        "field": "type"
                    },
                    "createdAt": {
                        "type": Sequelize.DATE,
                        "field": "createdAt",
                        "defaultValue": Sequelize.Date,
                        "allowNull": false
                    },
                    "updatedAt": {
                        "type": Sequelize.DATE,
                        "field": "updatedAt",
                        "defaultValue": Sequelize.Date,
                        "allowNull": false
                    }
                },
                {
                    "transaction": transaction
                }
            ]
        },
        {
            fn: "createTable",
            params: [
                "Teams",
                {
                    "id": {
                        "type": Sequelize.INTEGER,
                        "field": "id",
                        "autoIncrement": true,
                        "primaryKey": true,
                        "allowNull": false
                    },
                    "name": {
                        "type": Sequelize.STRING,
                        "field": "name"
                    },
                    "createdAt": {
                        "type": Sequelize.DATE,
                        "field": "createdAt",
                        "defaultValue": Sequelize.Date,
                        "allowNull": false
                    },
                    "updatedAt": {
                        "type": Sequelize.DATE,
                        "field": "updatedAt",
                        "defaultValue": Sequelize.Date,
                        "allowNull": false
                    },
                    "currentChallengeId": {
                        "type": Sequelize.INTEGER,
                        "field": "currentChallengeId",
                        "onUpdate": "CASCADE",
                        "onDelete": "SET NULL",
                        "references": {
                            "model": "Challenges",
                            "key": "id"
                        },
                        "allowNull": true
                    }
                },
                {
                    "transaction": transaction
                }
            ]
        },
        {
            fn: "createTable",
            params: [
                "InActivityStudentParticipations",
                {
                    "id": {
                        "type": Sequelize.INTEGER,
                        "field": "id",
                        "autoIncrement": true,
                        "primaryKey": true,
                        "allowNull": false
                    },
                    "state": {
                        "type": Sequelize.STRING,
                        "field": "state"
                    },
                    "order": {
                        "type": Sequelize.INTEGER,
                        "field": "order"
                    },
                    "createdAt": {
                        "type": Sequelize.DATE,
                        "field": "createdAt",
                        "defaultValue": Sequelize.Date,
                        "allowNull": false
                    },
                    "updatedAt": {
                        "type": Sequelize.DATE,
                        "field": "updatedAt",
                        "defaultValue": Sequelize.Date,
                        "allowNull": false
                    },
                    "teamId": {
                        "type": Sequelize.INTEGER,
                        "field": "teamId",
                        "onUpdate": "CASCADE",
                        "onDelete": "SET NULL",
                        "references": {
                            "model": "Teams",
                            "key": "id"
                        },
                        "allowNull": true
                    },
                    "activityId": {
                        "type": Sequelize.INTEGER,
                        "field": "activityId",
                        "onUpdate": "CASCADE",
                        "onDelete": "SET NULL",
                        "references": {
                            "model": "Activities",
                            "key": "id"
                        },
                        "allowNull": true
                    },
                    "studentId": {
                        "type": Sequelize.INTEGER,
                        "field": "studentId",
                        "onUpdate": "CASCADE",
                        "onDelete": "SET NULL",
                        "references": {
                            "model": "Users",
                            "key": "id"
                        },
                        "allowNull": true
                    }
                },
                {
                    "transaction": transaction
                }
            ]
        },
        {
            fn: "createTable",
            params: [
                "ChallengeItems",
                {
                    "id": {
                        "type": Sequelize.INTEGER,
                        "field": "id",
                        "autoIncrement": true,
                        "primaryKey": true,
                        "allowNull": false
                    },
                    "points": {
                        "type": Sequelize.INTEGER,
                        "field": "points"
                    },
                    "item": {
                        "type": Sequelize.STRING,
                        "field": "item"
                    },
                    "createdAt": {
                        "type": Sequelize.DATE,
                        "field": "createdAt",
                        "defaultValue": Sequelize.Date,
                        "allowNull": false
                    },
                    "updatedAt": {
                        "type": Sequelize.DATE,
                        "field": "updatedAt",
                        "defaultValue": Sequelize.Date,
                        "allowNull": false
                    },
                    "challengeId": {
                        "type": Sequelize.INTEGER,
                        "field": "challengeId",
                        "onUpdate": "CASCADE",
                        "onDelete": "SET NULL",
                        "references": {
                            "model": "Challenges",
                            "key": "id"
                        },
                        "allowNull": true
                    }
                },
                {
                    "transaction": transaction
                }
            ]
        },
        {
            fn: "createTable",
            params: [
                "ActivityChallengeConfigs",
                {
                    "id": {
                        "type": Sequelize.INTEGER,
                        "field": "id",
                        "autoIncrement": true,
                        "primaryKey": true,
                        "allowNull": false
                    },
                    "createdAt": {
                        "type": Sequelize.DATE,
                        "field": "createdAt",
                        "defaultValue": Sequelize.Date,
                        "allowNull": false
                    },
                    "updatedAt": {
                        "type": Sequelize.DATE,
                        "field": "updatedAt",
                        "defaultValue": Sequelize.Date,
                        "allowNull": false
                    },
                    "activityId": {
                        "type": Sequelize.INTEGER,
                        "field": "activityId",
                        "onUpdate": "CASCADE",
                        "onDelete": "SET NULL",
                        "references": {
                            "model": "Activities",
                            "key": "id"
                        },
                        "allowNull": true
                    },
                    "challengeId": {
                        "type": Sequelize.INTEGER,
                        "field": "challengeId",
                        "onUpdate": "CASCADE",
                        "onDelete": "SET NULL",
                        "references": {
                            "model": "Challenges",
                            "key": "id"
                        },
                        "allowNull": true
                    }
                },
                {
                    "transaction": transaction
                }
            ]
        },
        {
            fn: "createTable",
            params: [
                "ActivityChallengeConfigItems",
                {
                    "id": {
                        "type": Sequelize.INTEGER,
                        "field": "id",
                        "autoIncrement": true,
                        "primaryKey": true,
                        "allowNull": false
                    },
                    "value": {
                        "type": Sequelize.STRING,
                        "field": "value"
                    },
                    "item": {
                        "type": Sequelize.STRING,
                        "field": "item"
                    },
                    "createdAt": {
                        "type": Sequelize.DATE,
                        "field": "createdAt",
                        "defaultValue": Sequelize.Date,
                        "allowNull": false
                    },
                    "updatedAt": {
                        "type": Sequelize.DATE,
                        "field": "updatedAt",
                        "defaultValue": Sequelize.Date,
                        "allowNull": false
                    },
                    "activityChallengeConfigId": {
                        "type": Sequelize.INTEGER,
                        "field": "activityChallengeConfigId",
                        "onUpdate": "CASCADE",
                        "onDelete": "SET NULL",
                        "references": {
                            "model": "ActivityChallengeConfigs",
                            "key": "id"
                        },
                        "allowNull": true
                    }
                },
                {
                    "transaction": transaction
                }
            ]
        },
        {
            fn: "createTable",
            params: [
                "Achievements",
                {
                    "id": {
                        "type": Sequelize.INTEGER,
                        "field": "id",
                        "autoIncrement": true,
                        "primaryKey": true,
                        "allowNull": false
                    },
                    "createdAt": {
                        "type": Sequelize.DATE,
                        "field": "createdAt",
                        "defaultValue": Sequelize.Date,
                        "allowNull": false
                    },
                    "updatedAt": {
                        "type": Sequelize.DATE,
                        "field": "updatedAt",
                        "defaultValue": Sequelize.Date,
                        "allowNull": false
                    },
                    "challengeId": {
                        "type": Sequelize.INTEGER,
                        "field": "challengeId",
                        "onUpdate": "CASCADE",
                        "onDelete": "SET NULL",
                        "references": {
                            "model": "Challenges",
                            "key": "id"
                        },
                        "allowNull": true
                    },
                    "inActivityStudentParticipationId": {
                        "type": Sequelize.INTEGER,
                        "field": "inActivityStudentParticipationId",
                        "onUpdate": "CASCADE",
                        "onDelete": "SET NULL",
                        "references": {
                            "model": "InActivityStudentParticipations",
                            "key": "id"
                        },
                        "allowNull": true
                    }
                },
                {
                    "transaction": transaction
                }
            ]
        },
        {
            fn: "createTable",
            params: [
                "InActivityTeacherParticipations",
                {
                    "id": {
                        "type": Sequelize.INTEGER,
                        "field": "id",
                        "autoIncrement": true,
                        "primaryKey": true,
                        "allowNull": false
                    },
                    "state": {
                        "type": Sequelize.STRING,
                        "field": "state"
                    },
                    "order": {
                        "type": Sequelize.INTEGER,
                        "field": "order"
                    },
                    "createdAt": {
                        "type": Sequelize.DATE,
                        "field": "createdAt",
                        "defaultValue": Sequelize.Date,
                        "allowNull": false
                    },
                    "updatedAt": {
                        "type": Sequelize.DATE,
                        "field": "updatedAt",
                        "defaultValue": Sequelize.Date,
                        "allowNull": false
                    },
                    "challengeId": {
                        "type": Sequelize.INTEGER,
                        "field": "challengeId",
                        "onUpdate": "CASCADE",
                        "onDelete": "SET NULL",
                        "references": {
                            "model": "Challenges",
                            "key": "id"
                        },
                        "allowNull": true
                    },
                    "activityId": {
                        "type": Sequelize.INTEGER,
                        "field": "activityId",
                        "onUpdate": "CASCADE",
                        "onDelete": "SET NULL",
                        "references": {
                            "model": "Activities",
                            "key": "id"
                        },
                        "allowNull": true
                    },
                    "teacherId": {
                        "type": Sequelize.INTEGER,
                        "field": "teacherId",
                        "onUpdate": "CASCADE",
                        "onDelete": "SET NULL",
                        "references": {
                            "model": "Users",
                            "key": "id"
                        },
                        "allowNull": true
                    }
                },
                {
                    "transaction": transaction
                }
            ]
        },
        {
            fn: "createTable",
            params: [
                "AchievementItems",
                {
                    "id": {
                        "type": Sequelize.INTEGER,
                        "field": "id",
                        "autoIncrement": true,
                        "primaryKey": true,
                        "allowNull": false
                    },
                    "points": {
                        "type": Sequelize.INTEGER,
                        "field": "points"
                    },
                    "createdAt": {
                        "type": Sequelize.DATE,
                        "field": "createdAt",
                        "defaultValue": Sequelize.Date,
                        "allowNull": false
                    },
                    "updatedAt": {
                        "type": Sequelize.DATE,
                        "field": "updatedAt",
                        "defaultValue": Sequelize.Date,
                        "allowNull": false
                    },
                    "achievementId": {
                        "type": Sequelize.INTEGER,
                        "field": "achievementId",
                        "onUpdate": "CASCADE",
                        "onDelete": "SET NULL",
                        "references": {
                            "model": "Achievements",
                            "key": "id"
                        },
                        "allowNull": true
                    },
                    "challengeItemId": {
                        "type": Sequelize.INTEGER,
                        "field": "challengeItemId",
                        "onUpdate": "CASCADE",
                        "onDelete": "SET NULL",
                        "references": {
                            "model": "ChallengeItems",
                            "key": "id"
                        },
                        "allowNull": true
                    }
                },
                {
                    "transaction": transaction
                }
            ]
        }
    ];
};
var rollbackCommands = function(transaction) {
    return [{
            fn: "dropTable",
            params: ["Users", {
                transaction: transaction
            }]
        },
        {
            fn: "dropTable",
            params: ["Activities", {
                transaction: transaction
            }]
        },
        {
            fn: "dropTable",
            params: ["Teams", {
                transaction: transaction
            }]
        },
        {
            fn: "dropTable",
            params: ["Challenges", {
                transaction: transaction
            }]
        },
        {
            fn: "dropTable",
            params: ["Achievements", {
                transaction: transaction
            }]
        },
        {
            fn: "dropTable",
            params: ["AchievementItems", {
                transaction: transaction
            }]
        },
        {
            fn: "dropTable",
            params: ["ActivityChallengeConfigItems", {
                transaction: transaction
            }]
        },
        {
            fn: "dropTable",
            params: ["ActivityChallengeConfigs", {
                transaction: transaction
            }]
        },
        {
            fn: "dropTable",
            params: ["ChallengeItems", {
                transaction: transaction
            }]
        },
        {
            fn: "dropTable",
            params: ["InActivityTeacherParticipations", {
                transaction: transaction
            }]
        },
        {
            fn: "dropTable",
            params: ["InActivityStudentParticipations", {
                transaction: transaction
            }]
        }
    ];
};

module.exports = {
    pos: 0,
    useTransaction: true,
    execute: function(queryInterface, Sequelize, _commands)
    {
        var index = this.pos;
        function run(transaction) {
            const commands = _commands(transaction);
            return new Promise(function(resolve, reject) {
                function next() {
                    if (index < commands.length)
                    {
                        let command = commands[index];
                        console.log("[#"+index+"] execute: " + command.fn);
                        index++;
                        queryInterface[command.fn].apply(queryInterface, command.params).then(next, reject);
                    }
                    else
                        resolve();
                }
                next();
            });
        }
        if (this.useTransaction) {
            return queryInterface.sequelize.transaction(run);
        } else {
            return run(null);
        }
    },
    up: function(queryInterface, Sequelize)
    {
        return this.execute(queryInterface, Sequelize, migrationCommands);
    },
    down: function(queryInterface, Sequelize)
    {
        return this.execute(queryInterface, Sequelize, rollbackCommands);
    },
    info: info
};
