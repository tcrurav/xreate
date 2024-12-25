module.exports = (sequelize, Sequelize) => {
  const InActivityTeacherParticipation = sequelize.define("InActivityTeacherParticipation", {
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

  return InActivityTeacherParticipation;
};