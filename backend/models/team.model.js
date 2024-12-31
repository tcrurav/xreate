module.exports = (sequelize, Sequelize) => {
  const Team = sequelize.define("Team", {
    name: {
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
    tableName: 'teams',
  });

  return Team;
};