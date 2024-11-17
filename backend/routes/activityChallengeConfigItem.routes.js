module.exports = app => {
    const activityChallengeConfigItems = require("../controllers/activityChallengeConfigItem.controller.js");
    const auth = require("../controllers/auth.js");

    var router = require("express").Router();

    // Create a new ActivityChallengeConfigItems
    router.post("/", auth.isAuthenticated, activityChallengeConfigItems.create);

    // Retrieve all ActivityChallengeConfigItems
    router.get("/", auth.isAuthenticated, activityChallengeConfigItems.findAll);

    // Retrieve a single ActivityChallengeConfigItems with id
    router.get("/:id", auth.isAuthenticated, activityChallengeConfigItems.findOne);

    // Update a ActivityChallengeConfigItems with id
    router.put("/:id", auth.isAuthenticated, activityChallengeConfigItems.update);

    // Delete a ActivityChallengeConfigItems with id
    router.delete("/:id", auth.isAuthenticated, activityChallengeConfigItems.delete);

    app.use('/api/activityChallengeConfigItems', router);
};