const db = require("../models");
const ChallengeItem = db.challengeItem;
const Op = db.Sequelize.Op;

// Create and Save a new ChallengeItem
exports.create = (req, res) => {
    // Validate request
    if (!req.body.points || !req.body.item || !req.body.challengeId) {
        res.status(400).send({
            message: "Content can not be empty!"
        });
        return;
    }

    // Create a ChallengeItem
    const challengeItem = {
        points: req.body.points,
        item: req.body.item,
        challengeId: req.body.challengeId
    };

    // Save ChallengeItem in the database
    ChallengeItem.create(challengeItem)
        .then(data => {
            res.send(data);
        })
        .catch(err => {
            res.status(500).send({
                message: err.message || "Some error occurred while creating the ChallengeItem."
            });
        });
};

// Retrieve all ChallengeItems from the database.
exports.findAll = (req, res) => {
    ChallengeItem.findAll()
        .then(data => {
            res.send(data);
        })
        .catch(err => {
            res.status(500).send({
                message: err.message || "Some error occurred while retrieving challengeItems."
            });
        });
};

// Find a single ChallengeItem with an id
exports.findOne = (req, res) => {
    const id = req.params.id;

    ChallengeItem.findByPk(id)
        .then(data => {
            res.send(data);
        })
        .catch(err => {
            res.status(500).send({
                message: "Error retrieving ChallengeItem with id=" + id
            });
        });
};

// Update a ChallengeItem by the id in the request
exports.update = (req, res) => {
    const id = req.params.id;

    ChallengeItem.update(req.body, {
            where: { id: id }
        })
        .then(num => {
            if (num == 1) {
                res.send({
                    message: "ChallengeItem was updated successfully."
                });
            } else {
                res.send({
                    message: `Cannot update ChallengeItem with id=${id}. Maybe ChallengeItem was not found or req.body is empty!`
                });
            }
        })
        .catch(err => {
            res.status(500).send({
                message: "Error updating ChallengeItem with id=" + id
            });
        });
};

// Delete a ChallengeItem with the specified id in the request
exports.delete = (req, res) => {
    const id = req.params.id;

    ChallengeItem.destroy({
            where: { id: id }
        })
        .then(num => {
            if (num == 1) {
                res.send({
                    message: "ChallengeItem was deleted successfully!"
                });
            } else {
                res.send({
                    message: `Cannot delete ChallengeItem with id=${id}. Maybe ChallengeItem was not found!`
                });
            }
        })
        .catch(err => {
            res.status(500).send({
                message: "Could not delete ChallengeItem with id=" + id
            });
        });
};