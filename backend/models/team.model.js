module.exports = (sequelize, Sequelize) => {
  const Team = sequelize.define("Team", {
    name: {
      type: Sequelize.STRING
    }
  });

  return Team;
};