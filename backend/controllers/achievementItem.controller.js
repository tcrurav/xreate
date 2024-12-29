const db = require("../models");
const AchievementItem = db.achievementItem;
const Op = db.Sequelize.Op;

// Create and Save a new AchievementItem
exports.create = (req, res) => {
    // Validate request
    if (!req.body.points || !req.body.challengeItemId || !req.body.achievementId) {
        res.status(400).send({
            message: "Content can not be empty!"
        });
        return;
    }

    // Create a AchievementItem
    const achievementItem = {
        points: req.body.points,
        challengeItemId: req.body.challengeItemId,
        achievementId: req.body.achievementId
    };

    // Save AchievementItem in the database
    AchievementItem.create(achievementItem)
        .then(data => {
            res.send(data);
        })
        .catch(err => {
            res.status(500).send({
                message: err.message || "Some error occurred while creating the AchievementItem."
            });
        });
};

// Retrieve all AchievementItems from the database.
exports.findAll = (req, res) => {
    AchievementItem.findAll()
        .then(data => {
            res.send(data);
        })
        .catch(err => {
            res.status(500).send({
                message: err.message || "Some error occurred while retrieving achievementItems."
            });
        });
};

// Find a single AchievementItem with an id
exports.findOne = (req, res) => {
    const id = req.params.id;

    AchievementItem.findByPk(id)
        .then(data => {
            res.send(data);
        })
        .catch(err => {
            res.status(500).send({
                message: "Error retrieving AchievementItem with id=" + id
            });
        });
};

// Update a AchievementItem by the id in the request
exports.update = (req, res) => {
    const id = req.params.id;

    AchievementItem.update(req.body, {
            where: { id: id }
        })
        .then(num => {
            if (num == 1) {
                res.send({
                    message: "AchievementItem was updated successfully."
                });
            } else {
                res.send({
                    message: `Cannot update AchievementItem with id=${id}. Maybe AchievementItem was not found or req.body is empty!`
                });
            }
        })
        .catch(err => {
            res.status(500).send({
                message: "Error updating AchievementItem with id=" + id
            });
        });
};

// Delete a AchievementItem with the specified id in the request
exports.delete = (req, res) => {
    const id = req.params.id;

    AchievementItem.destroy({
            where: { id: id }
        })
        .then(num => {
            if (num == 1) {
                res.send({
                    message: "AchievementItem was deleted successfully!"
                });
            } else {
                res.send({
                    message: `Cannot delete AchievementItem with id=${id}. Maybe AchievementItem was not found!`
                });
            }
        })
        .catch(err => {
            res.status(500).send({
                message: "Could not delete AchievementItem with id=" + id
            });
        });
};