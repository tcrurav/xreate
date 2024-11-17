module.exports = (sequelize, Sequelize) => {
  const AchievementItem = sequelize.define("AchievementItem", {
    points: {
      type: Sequelize.INTEGER
    },
    item: {
      type: Sequelize.STRING
    }
  });

  return AchievementItem;
};