const db = require("../models");
const Team = db.team;
const Op = db.Sequelize.Op;

// Create and Save a new Team
exports.create = (req, res) => {
    // Validate request
    if (!req.body.name) {
        res.status(400).send({
            message: "Content can not be empty!"
        });
        return;
    }

    // Create a Team
    const team = {
        name: req.body.name,
    };

    // Save Team in the database
    Team.create(team)
        .then(data => {
            res.send(data);
        })
        .catch(err => {
            res.status(500).send({
                message: err.message || "Some error occurred while creating the Team."
            });
        });
};

// Retrieve all Teams from the database.
exports.findAll = (req, res) => {
    Team.findAll()
        .then(data => {
            res.send(data);
        })
        .catch(err => {
            res.status(500).send({
                message: err.message || "Some error occurred while retrieving teams."
            });
        });
};

// Find a single Team with an id
exports.findOne = (req, res) => {
    const id = req.params.id;

    Team.findByPk(id)
        .then(data => {
            res.send(data);
        })
        .catch(err => {
            res.status(500).send({
                message: "Error retrieving Team with id=" + id
            });
        });
};

// Update a Team by the id in the request
exports.update = (req, res) => {
    const id = req.params.id;

    Team.update(req.body, {
            where: { id: id }
        })
        .then(num => {
            if (num == 1) {
                res.send({
                    message: "Team was updated successfully."
                });
            } else {
                res.send({
                    message: `Cannot update Team with id=${id}. Maybe Team was not found or req.body is empty!`
                });
            }
        })
        .catch(err => {
            res.status(500).send({
                message: "Error updating Team with id=" + id
            });
        });
};

// Delete a Team with the specified id in the request
exports.delete = (req, res) => {
    const id = req.params.id;

    Team.destroy({
            where: { id: id }
        })
        .then(num => {
            if (num == 1) {
                res.send({
                    message: "Team was deleted successfully!"
                });
            } else {
                res.send({
                    message: `Cannot delete Team with id=${id}. Maybe Team was not found!`
                });
            }
        })
        .catch(err => {
            res.status(500).send({
                message: "Could not delete Team with id=" + id
            });
        });
};