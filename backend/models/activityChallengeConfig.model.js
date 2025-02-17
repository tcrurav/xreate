module.exports = (sequelize, Sequelize) => {
  const ActivityChallengeConfig = sequelize.define("ActivityChallengeConfig", {
    createdAt: {
      type: Sequelize.DATE,
      allowNull: false,
      defaultValue: new Date()
    }, 
    updatedAt: {
      type: Sequelize.DATE,
      allowNull: false,
      defaultValue: new Date()
    }
  },
  {
    tableName: 'activity_challenge_configs',
  });

  return ActivityChallengeConfig;
};