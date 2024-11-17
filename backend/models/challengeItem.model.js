module.exports = (sequelize, Sequelize) => {
  const ChallengeItem = sequelize.define("ChallengeItem", {
    value: {
      type: Sequelize.STRING
    },
    item: {
      type: Sequelize.STRING
    },
  });

  return ChallengeItem;
};