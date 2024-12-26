module.exports = (sequelize, Sequelize) => {
  const InActivityStudentParticipation = sequelize.define("InActivityStudentParticipation", {
    state: {   
      type: Sequelize.STRING,  // TODO - Possible values are still not clear. Let's see   
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

  return InActivityStudentParticipation;
};