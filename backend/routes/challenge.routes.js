module.exports = app => {
    const challenges = require("../controllers/challenge.controller.js");
    const auth = require("../controllers/auth.js");

    var router = require("express").Router();

    // Create a new Challenges
    router.post("/", auth.isAuthenticated, challenges.create);

    // Retrieve all Challenges
    router.get("/", auth.isAuthenticated, challenges.findAll);

    // Retrieve a single Challenges with id
    router.get("/:id", auth.isAuthenticated, challenges.findOne);

    // Update a Challenges with id
    router.put("/:id", auth.isAuthenticated, challenges.update);

    // Delete a Challenges with id
    router.delete("/:id", auth.isAuthenticated, challenges.delete);

    app.use('/api/challenges', router);
};