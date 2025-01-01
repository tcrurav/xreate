const app = require('./app');

require('dotenv').config();

const fs = require('fs');
const https = require("https");
const http = require("http");

const port = process.env.PORT || 80;
let protocol = http;
let options = {};
if (process.env.HTTPS && process.env.HTTPS == "true") {
  protocol = https;
  options = { 
    key: fs.readFileSync(`${process.env.SERVER_KEY}`), 
    cert: fs.readFileSync(`${process.env.SERVER_CERT}`) };
}

protocol.createServer(options, app).listen(port, () => {
  console.log('Server started on: ' + port);
});

module.exports = app; // to be used in the tests