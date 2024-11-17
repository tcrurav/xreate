const db = require("../models");
const Achievement = db.achievement;
const Op = db.Sequelize.Op;

// Create and Save a new Achievement
exports.create = (req, res) => {
    // Validate request
    if (!req.body.challengeId || !req.body.inActivityStudentParticipationId) {
        res.status(400).send({
            message: "Content can not be empty!"
        });
        return;
    }

    // Create a Achievement
    const achievement = {
        challengeId: req.body.challengeId,
        inActivityStudentParticipationId: req.body.inActivityStudentParticipationId,
    };

    // Save Achievement in the database
    Achievement.create(achievement)
        .then(data => {
            res.send(data);
        })
        .catch(err => {
            res.status(500).send({
                message: err.message || "Some error occurred while creating the Achievement."
            });
        });
};

// Retrieve all Achievements from the database.
exports.findAll = (req, res) => {
    Achievement.findAll()
        .then(data => {
            res.send(data);
        })
        .catch(err => {
            res.status(500).send({
                message: err.message || "Some error occurred while retrieving achievements."
            });
        });
};

// Find a single Achievement with an id
exports.findOne = (req, res) => {
    const id = req.params.id;

    Achievement.findByPk(id)
        .then(data => {
            res.send(data);
        })
        .catch(err => {
            res.status(500).send({
                message: "Error retrieving Achievement with id=" + id
            });
        });
};

// Update a Achievement by the id in the request
exports.update = (req, res) => {
    const id = req.params.id;

    Achievement.update(req.body, {
            where: { id: id }
        })
        .then(num => {
            if (num == 1) {
                res.send({
                    message: "Achievement was updated successfully."
                });
            } else {
                res.send({
                    message: `Cannot update Achievement with id=${id}. Maybe Achievement was not found or req.body is empty!`
                });
            }
        })
        .catch(err => {
            res.status(500).send({
                message: "Error updating Achievement with id=" + id
            });
        });
};

// Delete a Achievement with the specified id in the request
exports.delete = (req, res) => {
    const id = req.params.id;

    Achievement.destroy({
            where: { id: id }
        })
        .then(num => {
            if (num == 1) {
                res.send({
                    message: "Achievement was deleted successfully!"
                });
            } else {
                res.send({
                    message: `Cannot delete Achievement with id=${id}. Maybe Achievement was not found!`
                });
            }
        })
        .catch(err => {
            res.status(500).send({
                message: "Could not delete Achievement with id=" + id
            });
        });
};