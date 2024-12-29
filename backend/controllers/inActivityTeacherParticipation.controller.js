const db = require("../models");
const InActivityTeacherParticipation = db.inActivityTeacherParticipation;
const Activity = db.activity;
const Challenge = db.challenge;
const Sequelize = db.Sequelize;
const Op = db.Sequelize.Op;

// Create and Save a new InActivityTeacherParticipation
exports.create = (req, res) => {
    // Validate request
    if (!req.body.challengeId || !req.body.activityId || !req.body.teacherId ||
        !req.body.state || !req.body.order) {
        res.status(400).send({
            message: "Content can not be empty!"
        });
        return;
    }

    // Create a InActivityTeacherParticipation
    const inActivityTeacherParticipation = {
        challengeId: req.body.challengeId,
        activityId: req.body.activityId,
        teacherId: req.body.teacherId,
        state: req.body.state,
        order: req.body.order
    };

    // Save InActivityTeacherParticipation in the database
    InActivityTeacherParticipation.create(inActivityTeacherParticipation)
        .then(data => {
            res.send(data);
        })
        .catch(err => {
            res.status(500).send({
                message: err.message || "Some error occurred while creating the InActivityTeacherParticipation."
            });
        });
};

// Retrieve all InActivityTeacherParticipations from the database.
exports.findAll = (req, res) => {
    InActivityTeacherParticipation.findAll()
        .then(data => {
            res.send(data);
        })
        .catch(err => {
            res.status(500).send({
                message: err.message || "Some error occurred while retrieving inActivityTeacherParticipations."
            });
        });
};

// Retrieve all InActivityTeacherParticipation by a user (This is the list of activities for a teacher)
exports.findAllByTeacher = (req, res) => {
    const teacherId = req.params.teacherId;

    InActivityTeacherParticipation.findAll({
        order: [['order', 'ASC']],
        include: [{
            model: Activity,
            attributes: []
        },
        {
            model: Challenge,
            attributes: []
        }
        ],
        attributes: [
            'teacherId',
            'challengeId',
            'activityId',
            'state',
            'order',
            [Sequelize.col("Activity.startDate"), "activityStartDate"],
            [Sequelize.col("Activity.endDate"), "activityEndDate"],
            [Sequelize.col("Activity.state"), "activityState"],
            [Sequelize.col("Activity.type"), "activityType"],
            [Sequelize.col("Activity.name"), "activityName"],
            [Sequelize.col("Activity.description"), "activityDescription"],
            [Sequelize.col("Challenge.type"), "challengeType"],
            [Sequelize.col("Challenge.name"), "challengeName"],
        ],
        where: {
            teacherId: teacherId
        },
        // raw: true
    })
        .then(data => {
            res.send(data);
        })
        .catch(err => {
            res.status(500).send({
                message: err.message || "Some error occurred while retrieving inActivityStudentParticipations."
            });
        });
};

// Find a single InActivityTeacherParticipation with an id
exports.findOne = (req, res) => {
    const id = req.params.id;

    InActivityTeacherParticipation.findByPk(id)
        .then(data => {
            res.send(data);
        })
        .catch(err => {
            res.status(500).send({
                message: "Error retrieving InActivityTeacherParticipation with id=" + id
            });
        });
};

// Update a InActivityTeacherParticipation by the id in the request
exports.update = (req, res) => {
    const id = req.params.id;

    InActivityTeacherParticipation.update(req.body, {
        where: { id: id }
    })
        .then(num => {
            if (num == 1) {
                res.send({
                    message: "InActivityTeacherParticipation was updated successfully."
                });
            } else {
                res.send({
                    message: `Cannot update InActivityTeacherParticipation with id=${id}. Maybe InActivityTeacherParticipation was not found or req.body is empty!`
                });
            }
        })
        .catch(err => {
            res.status(500).send({
                message: "Error updating InActivityTeacherParticipation with id=" + id
            });
        });
};

// Delete a InActivityTeacherParticipation with the specified id in the request
exports.delete = (req, res) => {
    const id = req.params.id;

    InActivityTeacherParticipation.destroy({
        where: { id: id }
    })
        .then(num => {
            if (num == 1) {
                res.send({
                    message: "InActivityTeacherParticipation was deleted successfully!"
                });
            } else {
                res.send({
                    message: `Cannot delete InActivityTeacherParticipation with id=${id}. Maybe InActivityTeacherParticipation was not found!`
                });
            }
        })
        .catch(err => {
            res.status(500).send({
                message: "Could not delete InActivityTeacherParticipation with id=" + id
            });
        });
};