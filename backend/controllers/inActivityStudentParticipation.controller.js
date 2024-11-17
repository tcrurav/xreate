const db = require("../models");
const InActivityStudentParticipation = db.inActivityStudentParticipation;
const Op = db.Sequelize.Op;

// Create and Save a new InActivityStudentParticipation
exports.create = (req, res) => {
    // Validate request
    if (!req.body.teamId || !req.body.activityId || !req.body.studentId) {
        res.status(400).send({
            message: "Content can not be empty!"
        });
        return;
    }

    // Create a InActivityStudentParticipation
    const inActivityStudentParticipation = {
        teamId: req.body.teamId,
        activityId: req.body.activityId,
        studentId: req.body.studentId
    };

    // Save InActivityStudentParticipation in the database
    InActivityStudentParticipation.create(inActivityStudentParticipation)
        .then(data => {
            res.send(data);
        })
        .catch(err => {
            res.status(500).send({
                message: err.message || "Some error occurred while creating the InActivityStudentParticipation."
            });
        });
};

// Retrieve all InActivityStudentParticipations from the database.
exports.findAll = (req, res) => {
    InActivityStudentParticipation.findAll()
        .then(data => {
            res.send(data);
        })
        .catch(err => {
            res.status(500).send({
                message: err.message || "Some error occurred while retrieving inActivityStudentParticipations."
            });
        });
};

// Find a single InActivityStudentParticipation with an id
exports.findOne = (req, res) => {
    const id = req.params.id;

    InActivityStudentParticipation.findByPk(id)
        .then(data => {
            res.send(data);
        })
        .catch(err => {
            res.status(500).send({
                message: "Error retrieving InActivityStudentParticipation with id=" + id
            });
        });
};

// Update a InActivityStudentParticipation by the id in the request
exports.update = (req, res) => {
    const id = req.params.id;

    InActivityStudentParticipation.update(req.body, {
            where: { id: id }
        })
        .then(num => {
            if (num == 1) {
                res.send({
                    message: "InActivityStudentParticipation was updated successfully."
                });
            } else {
                res.send({
                    message: `Cannot update InActivityStudentParticipation with id=${id}. Maybe InActivityStudentParticipation was not found or req.body is empty!`
                });
            }
        })
        .catch(err => {
            res.status(500).send({
                message: "Error updating InActivityStudentParticipation with id=" + id
            });
        });
};

// Delete a InActivityStudentParticipation with the specified id in the request
exports.delete = (req, res) => {
    const id = req.params.id;

    InActivityStudentParticipation.destroy({
            where: { id: id }
        })
        .then(num => {
            if (num == 1) {
                res.send({
                    message: "InActivityStudentParticipation was deleted successfully!"
                });
            } else {
                res.send({
                    message: `Cannot delete InActivityStudentParticipation with id=${id}. Maybe InActivityStudentParticipation was not found!`
                });
            }
        })
        .catch(err => {
            res.status(500).send({
                message: "Could not delete InActivityStudentParticipation with id=" + id
            });
        });
};