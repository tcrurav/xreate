module.exports = app => {
    const users = require("../controllers/user.controller.js");
    const auth = require("../controllers/auth.js");

    var router = require("express").Router();

    // Retrieve all User
    router.get("/", auth.isAuthenticated, users.findAll);

    // Retrieve a single User with id
    router.get("/:id", auth.isAuthenticated, users.findOne);

    // Create a new User
    router.post("/", auth.isAuthenticated, auth.hasRole(["ADMIN"]), users.create);

    // Sign in
    router.post("/signin", auth.signin);

    // Sign in with code
    // router.post("/signinWithCode", auth.signinWithCode);

    // Update a User with id
    router.put("/:id", auth.isAuthenticated, auth.hasRole(["ADMIN"]), users.update);

    // Delete a User with id
    router.delete("/:id", auth.isAuthenticated, auth.hasRole(["ADMIN"]), users.delete);

    app.use('/api/users', router);
};