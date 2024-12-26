module.exports = (sequelize, Sequelize) => {
  const Activity = sequelize.define("Activity", {
    startDate: {
      type: Sequelize.DATE
    },
    endDate: {
      type: Sequelize.DATE
    },
    state: {   
      type: Sequelize.STRING,  // Possible values are: "NOT_STARTED", "STARTED", "FINISHED" 
      allowNull: false,
      defaultValue: "NOT_STARTED",
      validate: {
        is: /^(NOT_STARTED|STARTED|FINISHED)$/,
      }  
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

  return Activity;
};