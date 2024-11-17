'use strict';

const fs = require('fs');
const Sequelize = require('sequelize');
const env = process.env.NODE_ENV || 'development';
const config = require(__dirname + '/../config/config.js')[env];
const db = {};

let sequelize;
if (config.use_env_variable) {
  sequelize = new Sequelize(process.env[config.use_env_variable], config);
} else {
  sequelize = new Sequelize(config.database, config.username, config.password, config);
}

db.user = require("./user.model.js")(sequelize, Sequelize);
db.activity = require("./activity.model.js")(sequelize, Sequelize);
db.team = require("./team.model.js")(sequelize, Sequelize);
db.challenge = require("./challenge.model.js")(sequelize, Sequelize);
db.achievement = require("./achievement.model.js")(sequelize, Sequelize);
db.achievementItem = require("./achievementItem.model.js")(sequelize, Sequelize);
db.activityChallengeConfigItem = require("./activityChallengeConfigItem.model.js")(sequelize, Sequelize);
db.activityChallengeConfig = require("./activityChallengeConfig.model.js")(sequelize, Sequelize);
db.challengeItem = require("./challengeItem.model.js")(sequelize, Sequelize);
db.inActivityTeacherParticipation = require("./inActivityTeacherParticipation.model.js")(sequelize, Sequelize);
db.inActivityStudentParticipation = require("./inActivityStudentParticipation.model.js")(sequelize, Sequelize);

db.challenge.hasMany(db.achievement, { foreignKey: 'challengeId' });
db.achievement.belongsTo(db.challenge, { foreignKey: 'challengeId' });

db.inActivityStudentParticipation.hasMany(db.achievement, { foreignKey: 'inActivityStudentParticipationId' });
db.achievement.belongsTo(db.inActivityStudentParticipation, { foreignKey: 'inActivityStudentParticipationId' });

db.achievement.hasMany(db.achievementItem, { foreignKey: 'achievementId' });
db.achievementItem.belongsTo(db.achievement, { foreignKey: 'achievementId' });

db.activityChallengeConfig.hasMany(db.activityChallengeConfigItem, { foreignKey: 'activityChallengeConfigId' });
db.activityChallengeConfigItem.belongsTo(db.activityChallengeConfig, { foreignKey: 'activityChallengeConfigId' });

db.activity.hasMany(db.activityChallengeConfig, { foreignKey: 'activityId' });
db.activityChallengeConfig.belongsTo(db.activity, { foreignKey: 'activityId' });
db.challenge.hasMany(db.activityChallengeConfig, { foreignKey: 'challengeId' });
db.activityChallengeConfig.belongsTo(db.challenge, { foreignKey: 'challengeId' });

db.challenge.hasMany(db.challengeItem, { foreignKey: 'challengeId' });
db.challengeItem.belongsTo(db.challenge, { foreignKey: 'challengeId' });

db.challenge.hasMany(db.inActivityTeacherParticipation, { foreignKey: 'challengeId' });
db.inActivityTeacherParticipation.belongsTo(db.challenge, { foreignKey: 'challengeId' });
db.activity.hasMany(db.inActivityTeacherParticipation, { foreignKey: 'activityId' });
db.inActivityTeacherParticipation.belongsTo(db.activity, { foreignKey: 'activityId' });
db.user.hasMany(db.inActivityTeacherParticipation, { foreignKey: 'teacherId' });
db.inActivityTeacherParticipation.belongsTo(db.user, { foreignKey: 'teacherId' });

db.team.hasMany(db.inActivityStudentParticipation, { foreignKey: 'teamId' });
db.inActivityStudentParticipation.belongsTo(db.team, { foreignKey: 'teamId' });
db.activity.hasMany(db.inActivityStudentParticipation, { foreignKey: 'activityId' });
db.inActivityStudentParticipation.belongsTo(db.activity, { foreignKey: 'activityId' });
db.user.hasMany(db.inActivityStudentParticipation, { foreignKey: 'studentId' });
db.inActivityStudentParticipation.belongsTo(db.user, { foreignKey: 'studentId' });

db.sequelize = sequelize;
db.Sequelize = Sequelize;

module.exports = db;
