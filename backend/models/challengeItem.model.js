module.exports = (sequelize, Sequelize) => {
  const ChallengeItem = sequelize.define("ChallengeItem", {
    points: {
      type: Sequelize.INTEGER
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
    tableName: 'challenge_items',
  });

  return ChallengeItem;
};