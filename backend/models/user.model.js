module.exports = (sequelize, Sequelize) => {
  const User = sequelize.define("User", {
    username: {
      type: Sequelize.STRING
    },
    password: {
      type: Sequelize.STRING
    },
    nickname: {
      type: Sequelize.STRING
    },
    code: {
      type: Sequelize.STRING
    },
    role: {
      type: Sequelize.STRING
    },
    nationality: {
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
  });

  return User;
};