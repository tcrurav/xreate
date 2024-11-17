module.exports = app => {
    const activities = require("../controllers/activity.controller.js");
    const auth = require("../controllers/auth.js");

    var router = require("express").Router();

    // Create a new Activities
    router.post("/", auth.isAuthenticated, activities.create);

    // Retrieve all Activities
    router.get("/", auth.isAuthenticated, activities.findAll);

    // Retrieve a single Activities with id
    router.get("/:id", auth.isAuthenticated, activities.findOne);

    // Update a Activities with id
    router.put("/:id", auth.isAuthenticated, activities.update);

    // Delete a Activities with id
    router.delete("/:id", auth.isAuthenticated, activities.delete);

    app.use('/api/activities', router);
};