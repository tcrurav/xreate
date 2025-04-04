require('dotenv').config();
 
const jwt = require('jsonwebtoken');
const express = require('express');
const cors = require('cors');
const bodyParser = require('body-parser');
 
const app = express();
 
var corsOptions = {
  // TODO - Add concrete URLs
  origin: "http://*"
};
// enable CORS
app.use(cors(corsOptions));

// parse application/json
app.use(bodyParser.json());

// parse application/x-www-form-urlencoded
app.use(bodyParser.urlencoded({ extended: true }));

app.set("view engine", "ejs");
 
// database conection
const db = require("./models");

// TODO - Possibly remove following lines. Sync is now not necessary, because is made with the migrations.
// For explotation. Database is not dropped.
// db.sequelize.sync(); 

// Development only. Drops and re-sync db everytime the server starts.
// db.sequelize.sync({ force: true }).then(() => {
//   console.log("Drop and re-sync db.");
// });

//middleware that checks if JWT token exists and verifies it if it does exist.
//In all future routes, this helps to know if the request is authenticated or not.
app.use(function (req, res, next) {
  // check header or url parameters or post parameters for token
  var token = req.headers['authorization'];
  if (!token) return next(); //if no token, continue

  if(req.headers.authorization.indexOf('Basic ') === 0){
    // verify auth basic credentials
    const base64Credentials =  req.headers.authorization.split(' ')[1];
    const credentials = Buffer.from(base64Credentials, 'base64').toString('ascii');
    const [username, password] = credentials.split(':');

    req.body.username = username;
    req.body.password = password;

    return next();
  }

  token = token.replace('Bearer ', '');
  // .env should contain a line like JWT_SECRET=V3RY#1MP0RT@NT$3CR3T#
  jwt.verify(token, process.env.JWT_SECRET, function (err, user) {
    if (err) {
      return res.status(401).json({
        error: true,
        message: "Invalid user."
      });
    } else {
      req.user = user; //set the user to req so other routes can use it
      req.token = token;
      next();
    }
  });
});

// API routes
require("./routes/achievement.routes")(app);
require("./routes/achievementItem.routes")(app);
require("./routes/activity.routes")(app);
require("./routes/activityChallengeConfig.routes")(app);
require("./routes/activityChallengeConfigItem.routes")(app);
require("./routes/challenge.routes")(app);
require("./routes/challengeItem.routes")(app);
require("./routes/inActivityStudentParticipation.routes")(app);
require("./routes/inActivityTeacherParticipation.routes")(app);
require("./routes/team.routes")(app);
require("./routes/user.routes")(app);

// Views routes
require("./routes/view.routes")(app);

module.exports = app;