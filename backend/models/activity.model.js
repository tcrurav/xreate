module.exports = (sequelize, Sequelize) => {
  const Activity = sequelize.define("Activity", {
    startDate: {
      type: Sequelize.DATE
    },
    endDate: {
      type: Sequelize.DATE
    },
    state: {   
      type: Sequelize.STRING,
      allowNull: false,
      defaultValue: "NOT_STARTED",
      validate: {
        is: /^(NOT_STARTED|IN_PROGRESS|FINISHED)$/,
      }  
    },
    type: {
      type: Sequelize.STRING,
      allowNull: false,
      defaultValue: "TRAINNING_LAB",
      validate: {
        is: /^(TRAINING_LAB|VIRTUAL_CLASSROOM|ASSET_LAB)$/,
      }
    },
    name: {
      type: Sequelize.STRING
    },
    description: {
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

  return Activity;
};