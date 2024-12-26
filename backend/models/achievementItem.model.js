module.exports = (sequelize, Sequelize) => {
  const AchievementItem = sequelize.define("AchievementItem", {
    points: {
      type: Sequelize.INTEGER
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
  });

  return AchievementItem;
};