module.exports = app => {
    const teams = require("../controllers/team.controller.js");
    const auth = require("../controllers/auth.js");

    var router = require("express").Router();

    // Create a new Teams
    router.post("/", auth.isAuthenticated, auth.hasRole(["ADMIN"]), teams.create);

    // Retrieve all Teams
    router.get("/", auth.isAuthenticated, teams.findAll);

    // Retrieve all Teams with points
    router.get("/points", auth.isAuthenticated, teams.findAllWithPoints);

    // Retrieve a single Teams with id
    router.get("/:id", auth.isAuthenticated, teams.findOne);

    // Update a Teams with id
    router.put("/:id", auth.isAuthenticated, auth.hasRole(["ADMIN"]), teams.update);

    // Delete a Teams with id
    router.delete("/:id", auth.isAuthenticated, auth.hasRole(["ADMIN"]), teams.delete);

    app.use('/api/teams', router);
};