module.exports = app => {
    const activityChallengeConfigs = require("../controllers/activityChallengeConfig.controller.js");
    const auth = require("../controllers/auth.js");

    var router = require("express").Router();

    // Create a new ActivityChallengeConfigs
    router.post("/", auth.isAuthenticated, auth.hasRole(["ADMIN"]), activityChallengeConfigs.create);

    // Retrieve all ActivityChallengeConfigs
    router.get("/", auth.isAuthenticated, activityChallengeConfigs.findAll);

    // Retrieve all ActivityChallengeConfig by activityId
    router.get("/activity/:activityId", auth.isAuthenticated, activityChallengeConfigs.findAllByActivityId);

    // Retrieve a single ActivityChallengeConfigs with id
    router.get("/:id", auth.isAuthenticated, activityChallengeConfigs.findOne);

    // Update a ActivityChallengeConfigs with id
    router.put("/:id", auth.isAuthenticated, auth.hasRole(["ADMIN"]), activityChallengeConfigs.update);

    // Delete a ActivityChallengeConfigs with id
    router.delete("/:id", auth.isAuthenticated, auth.hasRole(["ADMIN"]), activityChallengeConfigs.delete);

    app.use('/api/activityChallengeConfigs', router);
};