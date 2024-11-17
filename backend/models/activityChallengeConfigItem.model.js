module.exports = (sequelize, Sequelize) => {
  const ActivityChallengeConfigItem = sequelize.define("ActivityChallengeConfigItem", {
    value: {
      type: Sequelize.STRING
    },
    item: {
      type: Sequelize.STRING
    }
  });

  return ActivityChallengeConfigItem;
};