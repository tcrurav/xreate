'use strict';

var Sequelize = require('sequelize');

/**
 * Actions summary:
 *
 * createTable "users", deps: []
 * createTable "activities", deps: []
 * createTable "challenges", deps: []
 * createTable "teams", deps: [challenges]
 * createTable "in_activity_student_participations", deps: [teams, activities, users]
 * createTable "challenge_items", deps: [challenges]
 * createTable "activity_challenge_configs", deps: [activities, challenges]
 * createTable "activity_challenge_config_items", deps: [activity_challenge_configs]
 * createTable "achievements", deps: [challenges, in_activity_student_participations]
 * createTable "in_activity_teacher_participations", deps: [challenges, activities, users]
 * createTable "achievement_items", deps: [achievements, challenge_items]
 *
 **/

var info = {
    "revision": 1,
    "name": "noname",
    "created": "2024-12-31T15:31:14.286Z",
    "comment": ""
};

var migrationCommands = function(transaction) {
    return [{
            fn: "createTable",
            params: [
                "users",
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
                "activities",
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
                "challenges",
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
                "teams",
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
                            "model": "challenges",
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
                "in_activity_student_participations",
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
                            "model": "teams",
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
                            "model": "activities",
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
                            "model": "users",
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
                "challenge_items",
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
                            "model": "challenges",
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
                "activity_challenge_configs",
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
                            "model": "activities",
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
                            "model": "challenges",
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
                "activity_challenge_config_items",
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
                            "model": "activity_challenge_configs",
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
                "achievements",
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
                            "model": "challenges",
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
                            "model": "in_activity_student_participations",
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
                "in_activity_teacher_participations",
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
                            "model": "challenges",
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
                            "model": "activities",
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
                            "model": "users",
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
                "achievement_items",
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
                            "model": "achievements",
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
                            "model": "challenge_items",
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
            params: ["users", {
                transaction: transaction
            }]
        },
        {
            fn: "dropTable",
            params: ["activities", {
                transaction: transaction
            }]
        },
        {
            fn: "dropTable",
            params: ["teams", {
                transaction: transaction
            }]
        },
        {
            fn: "dropTable",
            params: ["challenges", {
                transaction: transaction
            }]
        },
        {
            fn: "dropTable",
            params: ["achievements", {
                transaction: transaction
            }]
        },
        {
            fn: "dropTable",
            params: ["achievement_items", {
                transaction: transaction
            }]
        },
        {
            fn: "dropTable",
            params: ["activity_challenge_config_items", {
                transaction: transaction
            }]
        },
        {
            fn: "dropTable",
            params: ["activity_challenge_configs", {
                transaction: transaction
            }]
        },
        {
            fn: "dropTable",
            params: ["challenge_items", {
                transaction: transaction
            }]
        },
        {
            fn: "dropTable",
            params: ["in_activity_teacher_participations", {
                transaction: transaction
            }]
        },
        {
            fn: "dropTable",
            params: ["in_activity_student_participations", {
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
