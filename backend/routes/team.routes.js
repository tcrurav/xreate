module.exports = app => {
    const teams = require("../controllers/team.controller.js");
    const auth = require("../controllers/auth.js");

    var router = require("express").Router();

    // Create a new Teams
    router.post("/", auth.isAuthenticated, teams.create);

    // Retrieve all Teams
    router.get("/", auth.isAuthenticated, teams.findAll);

    // Retrieve a single Teams with id
    router.get("/:id", auth.isAuthenticated, teams.findOne);

    // Update a Teams with id
    router.put("/:id", auth.isAuthenticated, teams.update);

    // Delete a Teams with id
    router.delete("/:id", auth.isAuthenticated, teams.delete);

    app.use('/api/teams', router);
};