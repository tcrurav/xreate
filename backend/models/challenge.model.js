module.exports = (sequelize, Sequelize) => {
  const Challenge = sequelize.define("Challenge", {
    name: {
      type: Sequelize.STRING
    },
    type: {
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
    tableName: 'challenges',
  });

  return Challenge;
};