module.exports = (sequelize, Sequelize) => {
  const InActivityStudentParticipation = sequelize.define("InActivityStudentParticipation", {
    state: {   
      type: Sequelize.STRING,  // TODO - Possible values are still not clear. Let's see   
    },
    order: {   
      type: Sequelize.INTEGER,    
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
    tableName: 'in_activity_student_participations',
  });

  return InActivityStudentParticipation;
};