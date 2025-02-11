module.exports = app => {
    const achievementItems = require("../controllers/achievementItem.controller.js");
    const auth = require("../controllers/auth.js");

    var router = require("express").Router();

    // Create a new AchievementItems
    router.post("/", auth.isAuthenticated, auth.hasRole(["ADMIN"]), achievementItems.create);

    // Retrieve all AchievementItems
    router.get("/", auth.isAuthenticated, achievementItems.findAll);

    // Retrieve a single AchievementItems with id
    router.get("/:id", auth.isAuthenticated, achievementItems.findOne);

    // get an AchievementItem by challengeName and challenge item and studentId and activityId
    router.get("/challenge/:challengeName/challengeItem/:challengeItemItem/student/:studentId/activity/:activityId",
        auth.isAuthenticated,
        // auth.isUserId(studentId), // TODO - Restrict to just the user involved
        achievementItems.getByChallengeNameAndChallengeItemItemAndStudentIdAndActivityId);

    // Update a AchievementItems with id
    router.put("/:id", auth.isAuthenticated, auth.hasRole(["ADMIN"]), achievementItems.update);

    // update an AchievementItem with challengeName and challenge item and studentId and activityId
    router.put("/challenge/:challengeName/challengeItem/:challengeItemItem/student/:studentId/activity/:activityId",
        auth.isAuthenticated,
        // auth.isUserId(studentId), // TODO - Restrict to just the user involved
        achievementItems.updateByChallengeNameAndChallengeItemItemAndStudentIdAndActivityId);

    // Delete a AchievementItems with id
    router.delete("/:id", auth.isAuthenticated, auth.hasRole(["ADMIN"]), achievementItems.delete);

    app.use('/api/achievementItems', router);
};