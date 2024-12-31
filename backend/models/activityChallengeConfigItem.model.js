module.exports = (sequelize, Sequelize) => {
  const ActivityChallengeConfigItem = sequelize.define("ActivityChallengeConfigItem", {
    value: {
      type: Sequelize.STRING
    },
    item: {
      type: Sequelize.STRING
    },
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
    tableName: 'in_activity_challenge_config_items',
  });

  return ActivityChallengeConfigItem;
};