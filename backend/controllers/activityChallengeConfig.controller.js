const db = require("../models");
const ActivityChallengeConfig = db.activityChallengeConfig;
const Op = db.Sequelize.Op;

// Create and Save a new ActivityChallengeConfig
exports.create = (req, res) => {
    // Validate request
    if (!req.body.activityId || !req.body.challengeId) {
        res.status(400).send({
            message: "Content can not be empty!"
        });
        return;
    }

    // Create a ActivityChallengeConfig
    const activityChallengeConfig = {
        activityId: req.body.activityId,
        challengeId: req.body.challengeId
    };

    // Save ActivityChallengeConfig in the database
    ActivityChallengeConfig.create(activityChallengeConfig)
        .then(data => {
            res.send(data);
        })
        .catch(err => {
            res.status(500).send({
                message: err.message || "Some error occurred while creating the ActivityChallengeConfig."
            });
        });
};

// Retrieve all ActivityChallengeConfigs from the database.
exports.findAll = (req, res) => {
    ActivityChallengeConfig.findAll()
        .then(data => {
            res.send(data);
        })
        .catch(err => {
            res.status(500).send({
                message: err.message || "Some error occurred while retrieving activityChallengeConfigChallengeConfigs."
            });
        });
};

// Retrieve all ActivityChallengeConfigs by activityId from the database.
exports.findAllByActivityId = (req, res) => {
    const activityId = req.params.activityId;

    ActivityChallengeConfig.findAll({
        where: { activityId: activityId }
    })
        .then(data => {
            res.send(data);
        })
        .catch(err => {
            res.status(500).send({
                message: err.message || "Some error occurred while retrieving activityChallengeConfigChallengeConfigs."
            });
        });
};

// Find a single ActivityChallengeConfig with an id
exports.findOne = (req, res) => {
    const id = req.params.id;

    ActivityChallengeConfig.findByPk(id)
        .then(data => {
            res.send(data);
        })
        .catch(err => {
            res.status(500).send({
                message: "Error retrieving ActivityChallengeConfig with id=" + id
            });
        });
};

// Update a ActivityChallengeConfig by the id in the request
exports.update = (req, res) => {
    const id = req.params.id;

    ActivityChallengeConfig.update(req.body, {
        where: { id: id }
    })
        .then(num => {
            if (num == 1) {
                res.send({
                    message: "ActivityChallengeConfig was updated successfully."
                });
            } else {
                res.send({
                    message: `Cannot update ActivityChallengeConfig with id=${id}. Maybe ActivityChallengeConfig was not found or req.body is empty!`
                });
            }
        })
        .catch(err => {
            res.status(500).send({
                message: "Error updating ActivityChallengeConfig with id=" + id
            });
        });
};

// Delete a ActivityChallengeConfig with the specified id in the request
exports.delete = (req, res) => {
    const id = req.params.id;

    ActivityChallengeConfig.destroy({
        where: { id: id }
    })
        .then(num => {
            if (num == 1) {
                res.send({
                    message: "ActivityChallengeConfig was deleted successfully!"
                });
            } else {
                res.send({
                    message: `Cannot delete ActivityChallengeConfig with id=${id}. Maybe ActivityChallengeConfig was not found!`
                });
            }
        })
        .catch(err => {
            res.status(500).send({
                message: "Could not delete ActivityChallengeConfig with id=" + id
            });
        });
};