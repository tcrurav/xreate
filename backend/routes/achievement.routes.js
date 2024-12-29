module.exports = app => {
    const achievements = require("../controllers/achievement.controller.js");
    const auth = require("../controllers/auth.js");

    var router = require("express").Router();

    // Create a new Achievements
    router.post("/", auth.isAuthenticated, achievements.create);

    // Retrieve all Achievements
    router.get("/", auth.isAuthenticated, achievements.findAll);

    // Retrieve all Achievements eagerly
    router.get("/eagerly", auth.isAuthenticated, achievements.findAllEagerly);
    
    // Retrieve a single Achievements with id
    router.get("/:id", auth.isAuthenticated, achievements.findOne);

    // Update a Achievements with id
    router.put("/:id", auth.isAuthenticated, achievements.update);

    // Delete a Achievements with id
    router.delete("/:id", auth.isAuthenticated, achievements.delete);

    app.use('/api/achievements', router);
};