const db = require("../models");
const ActivityChallengeConfigItem = db.activityChallengeConfigItem;
const Op = db.Sequelize.Op;

// Create and Save a new ActivityChallengeConfigItem
exports.create = (req, res) => {
    // Validate request
    if (!req.body.activityChallengeConfigId || !req.body.value || !req.body.item) {
        res.status(400).send({
            message: "Content can not be empty!"
        });
        return;
    }

    // Create a ActivityChallengeConfigItem
    const activityChallengeConfigItem = {
        activityChallengeConfigId: req.body.activityChallengeConfigId,
        value: req.body.value,
        item: req.body.item
    };

    // Save ActivityChallengeConfigItem in the database
    ActivityChallengeConfigItem.create(activityChallengeConfigItem)
        .then(data => {
            res.send(data);
        })
        .catch(err => {
            res.status(500).send({
                message: err.message || "Some error occurred while creating the ActivityChallengeConfigItem."
            });
        });
};

// Retrieve all ActivityChallengeConfigItems from the database.
exports.findAll = (req, res) => {
    ActivityChallengeConfigItem.findAll()
        .then(data => {
            res.send(data);
        })
        .catch(err => {
            res.status(500).send({
                message: err.message || "Some error occurred while retrieving activityChallengeConfigItems."
            });
        });
};

// Find a single ActivityChallengeConfigItem with an id
exports.findOne = (req, res) => {
    const id = req.params.id;

    ActivityChallengeConfigItem.findByPk(id)
        .then(data => {
            res.send(data);
        })
        .catch(err => {
            res.status(500).send({
                message: "Error retrieving ActivityChallengeConfigItem with id=" + id
            });
        });
};

// Update a ActivityChallengeConfigItem by the id in the request
exports.update = (req, res) => {
    const id = req.params.id;

    ActivityChallengeConfigItem.update(req.body, {
            where: { id: id }
        })
        .then(num => {
            if (num == 1) {
                res.send({
                    message: "ActivityChallengeConfigItem was updated successfully."
                });
            } else {
                res.send({
                    message: `Cannot update ActivityChallengeConfigItem with id=${id}. Maybe ActivityChallengeConfigItem was not found or req.body is empty!`
                });
            }
        })
        .catch(err => {
            res.status(500).send({
                message: "Error updating ActivityChallengeConfigItem with id=" + id
            });
        });
};

// Delete a ActivityChallengeConfigItem with the specified id in the request
exports.delete = (req, res) => {
    const id = req.params.id;

    ActivityChallengeConfigItem.destroy({
            where: { id: id }
        })
        .then(num => {
            if (num == 1) {
                res.send({
                    message: "ActivityChallengeConfigItem was deleted successfully!"
                });
            } else {
                res.send({
                    message: `Cannot delete ActivityChallengeConfigItem with id=${id}. Maybe ActivityChallengeConfigItem was not found!`
                });
            }
        })
        .catch(err => {
            res.status(500).send({
                message: "Could not delete ActivityChallengeConfigItem with id=" + id
            });
        });
};

// Find all items by activityChallengeConfigId
exports.findAllByConfigId = async (req, res) => {
    try {
        const configId = req.params.configId;
        const items = await ActivityChallengeConfigItem.findAll({
            where: { activityChallengeConfigId: configId }
        });
        res.status(200).json(items);
    } catch (error) {
        res.status(500).json({ message: error.message });
    }
};
