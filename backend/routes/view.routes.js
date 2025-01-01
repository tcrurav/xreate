module.exports = app => {
    const view = require("../controllers/view.controller.js");
  
    var router = require("express").Router();
  
    // index view
    router.get("/", view.index);
  
    app.use('/', router);
  };