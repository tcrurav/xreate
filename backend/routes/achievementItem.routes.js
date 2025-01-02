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

    // Update a AchievementItems with id
    router.put("/:id", auth.isAuthenticated, auth.hasRole(["ADMIN"]), achievementItems.update);

    // Delete a AchievementItems with id
    router.delete("/:id", auth.isAuthenticated, auth.hasRole(["ADMIN"]), achievementItems.delete);

    app.use('/api/achievementItems', router);
};