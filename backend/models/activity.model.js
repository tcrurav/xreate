module.exports = (sequelize, Sequelize) => {
  const Activity = sequelize.define("Activity", {
    startDate: {
      type: Sequelize.DATE
    },
    endDate: {
      type: Sequelize.DATE
    },
    started: {
      type: Sequelize.BOOLEAN
    },
  });

  return Activity;
};