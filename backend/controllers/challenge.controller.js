const db = require("../models");
const Challenge = db.challenge;
const Op = db.Sequelize.Op;

// Create and Save a new Challenge
exports.create = (req, res) => {
    // Validate request
    if (!req.body.name || !req.body.type) {
        res.status(400).send({
            message: "Content can not be empty!"
        });
        return;
    }

    // Create a Challenge
    const challenge = {
        name: req.body.name,
        type: req.body.type
    };

    // Save Challenge in the database
    Challenge.create(challenge)
        .then(data => {
            res.send(data);
        })
        .catch(err => {
            res.status(500).send({
                message: err.message || "Some error occurred while creating the Challenge."
            });
        });
};

// Retrieve all Challenges from the database.
exports.findAll = (req, res) => {
    Challenge.findAll()
        .then(data => {
            res.send(data);
        })
        .catch(err => {
            res.status(500).send({
                message: err.message || "Some error occurred while retrieving challenges."
            });
        });
};

// Find a single Challenge with an id
exports.findOne = (req, res) => {
    const id = req.params.id;

    Challenge.findByPk(id)
        .then(data => {
            res.send(data);
        })
        .catch(err => {
            res.status(500).send({
                message: "Error retrieving Challenge with id=" + id
            });
        });
};

// Update a Challenge by the id in the request
exports.update = (req, res) => {
    const id = req.params.id;

    Challenge.update(req.body, {
            where: { id: id }
        })
        .then(num => {
            if (num == 1) {
                res.send({
                    message: "Challenge was updated successfully."
                });
            } else {
                res.send({
                    message: `Cannot update Challenge with id=${id}. Maybe Challenge was not found or req.body is empty!`
                });
            }
        })
        .catch(err => {
            res.status(500).send({
                message: "Error updating Challenge with id=" + id
            });
        });
};

// Delete a Challenge with the specified id in the request
exports.delete = (req, res) => {
    const id = req.params.id;

    Challenge.destroy({
            where: { id: id }
        })
        .then(num => {
            if (num == 1) {
                res.send({
                    message: "Challenge was deleted successfully!"
                });
            } else {
                res.send({
                    message: `Cannot delete Challenge with id=${id}. Maybe Challenge was not found!`
                });
            }
        })
        .catch(err => {
            res.status(500).send({
                message: "Could not delete Challenge with id=" + id
            });
        });
};