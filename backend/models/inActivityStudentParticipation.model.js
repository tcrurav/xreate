module.exports = (sequelize, Sequelize) => {
  const InActivityStudentParticipation = sequelize.define("InActivityStudentParticipation", {
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

  return InActivityStudentParticipation;
};