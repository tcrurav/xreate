const db = require("../models");
const Activity = db.activity;
const Op = db.Sequelize.Op;

// Create and Save a new Activity
exports.create = (req, res) => {
    // Validate request
    if (!req.body.startDate || !req.body.endDate || 
        !req.body.state || !req.body.type ||
        !req.body.name || !req.body.description) {
        res.status(400).send({
            message: "Content can not be empty!"
        });
        return;
    }

    // Create a Activity
    const activity = {
        startDate: req.body.startDate,
        endDate: req.body.endDate,
        state: req.body.state,
        type: req.body.type,
        name: req.body.name,
        description: req.body.description,
    };

    // Save Activity in the database
    Activity.create(activity)
        .then(data => {
            res.send(data);
        })
        .catch(err => {
            res.status(500).send({
                message: err.message || "Some error occurred while creating the Activity."
            });
        });
};

// Retrieve all Activities from the database.
exports.findAll = (req, res) => {
    Activity.findAll()
        .then(data => {
            res.send(data);
        })
        .catch(err => {
            res.status(500).send({
                message: err.message || "Some error occurred while retrieving activities."
            });
        });
};

// Find a single Activity with an id
exports.findOne = (req, res) => {
    const id = req.params.id;

    Activity.findByPk(id)
        .then(data => {
            res.send(data);
        })
        .catch(err => {
            res.status(500).send({
                message: "Error retrieving Activity with id=" + id
            });
        });
};

// Update a Activity by the id in the request
exports.update = (req, res) => {
    const id = req.params.id;

    Activity.update(req.body, {
            where: { id: id }
        })
        .then(num => {
            if (num == 1) {
                res.send({
                    message: "Activity was updated successfully."
                });
            } else {
                res.send({
                    message: `Cannot update Activity with id=${id}. Maybe Activity was not found or req.body is empty!`
                });
            }
        })
        .catch(err => {
            res.status(500).send({
                message: "Error updating Activity with id=" + id
            });
        });
};

// Delete a Activity with the specified id in the request
exports.delete = (req, res) => {
    const id = req.params.id;

    Activity.destroy({
            where: { id: id }
        })
        .then(num => {
            if (num == 1) {
                res.send({
                    message: "Activity was deleted successfully!"
                });
            } else {
                res.send({
                    message: `Cannot delete Activity with id=${id}. Maybe Activity was not found!`
                });
            }
        })
        .catch(err => {
            res.status(500).send({
                message: "Could not delete Activity with id=" + id
            });
        });
};