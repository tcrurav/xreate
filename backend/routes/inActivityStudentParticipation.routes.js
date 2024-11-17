module.exports = app => {
    const inActivityStudentParticipations = require("../controllers/inActivityStudentParticipation.controller.js");
    const auth = require("../controllers/auth.js");

    var router = require("express").Router();

    // Create a new InActivityStudentParticipation
    router.post("/", auth.isAuthenticated, inActivityStudentParticipations.create);

    // Retrieve all InActivityStudentParticipation
    router.get("/", auth.isAuthenticated, inActivityStudentParticipations.findAll);

    // Retrieve a single InActivityStudentParticipation with id
    router.get("/:id", auth.isAuthenticated, inActivityStudentParticipations.findOne);

    // Update a InActivityStudentParticipation with id
    router.put("/:id", auth.isAuthenticated, inActivityStudentParticipations.update);

    // Delete a InActivityStudentParticipation with id
    router.delete("/:id", auth.isAuthenticated, inActivityStudentParticipations.delete);

    app.use('/api/inActivityStudentParticipations', router);
};