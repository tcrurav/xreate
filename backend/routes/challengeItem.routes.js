module.exports = app => {
    const challengeItems = require("../controllers/challengeItem.controller.js");
    const auth = require("../controllers/auth.js");

    var router = require("express").Router();

    // Create a new AhallengeItems
    router.post("/", auth.isAuthenticated, auth.hasRole(["ADMIN"]), challengeItems.create);

    // Retrieve all AhallengeItems
    router.get("/", auth.isAuthenticated, challengeItems.findAll);

    // Retrieve a single AhallengeItems with id
    router.get("/:id", auth.isAuthenticated, challengeItems.findOne);

    // Update a AhallengeItems with id
    router.put("/:id", auth.isAuthenticated, auth.hasRole(["ADMIN"]), challengeItems.update);

    // Delete a AhallengeItems with id
    router.delete("/:id", auth.isAuthenticated, auth.hasRole(["ADMIN"]), challengeItems.delete);

    app.use('/api/challengeItems', router);
};