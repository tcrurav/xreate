module.exports = app => {
    const inActivityTeacherParticipations = require("../controllers/inActivityTeacherParticipation.controller.js");
    const auth = require("../controllers/auth.js");

    var router = require("express").Router();

    // Create a new InActivityTeacherParticipations
    router.post("/", auth.isAuthenticated, auth.hasRole(["ADMIN"]), inActivityTeacherParticipations.create);

    // Retrieve all InActivityTeacherParticipations
    router.get("/", auth.isAuthenticated, inActivityTeacherParticipations.findAll);

    // Retrieve a single InActivityTeacherParticipations with id
    router.get("/:id", auth.isAuthenticated, inActivityTeacherParticipations.findOne);

    // Retrieve all InActivityTeacherParticipation by a user (This is the list of activities for a teacher)
    router.get("/teachers/:teacherId", auth.isAuthenticated, inActivityTeacherParticipations.findAllByTeacher);

    // Update a InActivityTeacherParticipations with id
    router.put("/:id", auth.isAuthenticated, auth.hasRole(["ADMIN"]), inActivityTeacherParticipations.update);

    // Delete a InActivityTeacherParticipations with id
    router.delete("/:id", auth.isAuthenticated, auth.hasRole(["ADMIN"]), inActivityTeacherParticipations.delete);

    app.use('/api/inActivityTeacherParticipations', router);
};