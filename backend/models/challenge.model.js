module.exports = (sequelize, Sequelize) => {
  const Challenge = sequelize.define("Challenge", {
    name: {
      type: Sequelize.STRING
    },
    type: {
      type: Sequelize.STRING
    }
  });

  return Challenge;
};