module.exports = app => {
    const inActivityStudentParticipations = require("../controllers/inActivityStudentParticipation.controller.js");
    const auth = require("../controllers/auth.js");

    var router = require("express").Router();

    // Create a new InActivityStudentParticipation
    router.post("/", auth.isAuthenticated, auth.hasRole(["ADMIN"]), inActivityStudentParticipations.create);

    // Retrieve all InActivityStudentParticipation
    router.get("/", auth.isAuthenticated, inActivityStudentParticipations.findAll);

    // Retrieve a single InActivityStudentParticipation with id
    router.get("/:id", auth.isAuthenticated, inActivityStudentParticipations.findOne);

    // Retrieve all InActivityStudentParticipation in an Activity (students participating in an activity)
    router.get("/activities/:activityId", auth.isAuthenticated, inActivityStudentParticipations.findAllByActivityId);

    // Retrieve all InActivityStudentParticipation with points in an Activity (points for each student in an activity)
    router.get("/points/activities/:activityId", auth.isAuthenticated, inActivityStudentParticipations.findAllByActivityIdWithPoints);

    // Retrieve all TopPlayersByTeamAndActivityId with points in an Activity (points for each team in an activity)
    router.get("/points/teams/topPlayers/activities/:activityId", auth.isAuthenticated, inActivityStudentParticipations.findAllTopPlayersByTeamAndActivityIdWithPoints);

    // Retrieve all InActivityStudentParticipation with points in an Activity grouped by team (points for each team in an activity)
    router.get("/points/teams/activities/:activityId", auth.isAuthenticated, inActivityStudentParticipations.findAllPointsByTeamAndActivityId);

    // Retrieve all InActivityStudentParticipation in an Activity for a user (student in an activity)
    router.get("/activities/:activityId/students/:studentId", auth.isAuthenticated, inActivityStudentParticipations.findAllByActivityIdAndStudentId);

    // Retrieve all InActivityStudentParticipation by a user (This is the learning path of a student)
    router.get("/learningPath/students/:studentId", auth.isAuthenticated, inActivityStudentParticipations.findLearningPath);

    // Update a InActivityStudentParticipation with id
    router.put("/:id", auth.isAuthenticated, auth.hasRole(["ADMIN"]), inActivityStudentParticipations.update);

    // Delete a InActivityStudentParticipation with id
    router.delete("/:id", auth.isAuthenticated, auth.hasRole(["ADMIN"]), inActivityStudentParticipations.delete);

    app.use('/api/inActivityStudentParticipations', router);
};