const { where } = require("sequelize");
const db = require("../models");
const AchievementItem = db.achievementItem;
const Achievement = db.achievement;
const InActivityStudentParticipation = db.inActivityStudentParticipation;
const Challenge = db.challenge;
const ChallengeItem = db.challengeItem;
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

// get an AchievementItem with challengeName and challenge item and studentId and activityId
exports.getByChallengeNameAndChallengeItemItemAndStudentIdAndActivityId = (req, res) => {
    const challengeName = req.params.challengeName;
    const challengeItemItem = req.params.challengeItemItem;
    const studentId = req.params.studentId;
    const activityId = req.params.activityId;

    AchievementItem.findAll({
        include: [{
            model: Achievement, attributes: [],
            required: true,
            include: [{
                model: InActivityStudentParticipation, attributes: [],
                where: {
                    activityId: activityId,
                    studentId: studentId
                }
            }, {
                model: Challenge, attributes: [],
                where: {
                    name: challengeName
                }
            }]
        }, {
            model: ChallengeItem, attributes: [],
            required: true,
            where: {
                item: challengeItemItem
            }
        }],
        attributes: [
            'id',
            'achievementId',
            'challengeItemId',
            'points',
            'ChallengeItem.item',
            'Achievement.Challenge.name',
            'Achievement.InActivityStudentParticipation.activityId',
            'Achievement.InActivityStudentParticipation.studentId',
        ],
        raw: true
    }).then(data => {
        res.send(data);
    }).catch(err => {
        res.status(500).send({
            message: err.message
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

// Reset all points in all activities
exports.resetPoints = (req, res) => {

    AchievementItem.update({ points: 0 })
        .then(num => {
            if (num > 0) {
                res.send({
                    message: `{num} AchievementItems have been successfully reset.`
                });
            } else {
                res.send({
                    message: "No AchievementItem found"
                });
            }
        })
        .catch(err => {
            res.status(500).send({
                message: "Error resetting points"
            });
        });
};

// Reset all points by activityId
exports.resetPointsByActivityId = async (req, res) => {
    const activityId = req.params.activityId;

    try {
        // find first AchievementItem of ActivityId
        const achivementItemsToReset = await AchievementItem.findAll({
            include: [{
                model: Achievement, attributes: [],
                required: true,
                include: [{
                    model: InActivityStudentParticipation, attributes: [],
                    where: {
                        activityId: activityId
                    }
                }]
            }],
            attributes: [
                'id',
                'achievementId',
                'challengeItemId',
                'points',
                'Achievement.InActivityStudentParticipation.activityId',
            ],
            raw: true
        });

        achivementItemsToReset.map(async (a) => {
            await AchievementItem.update({ points: 0 }, {
                where: { id: a.id }
            });
        });

        res.send({
            message: "AchievementItem updating finished with no errors."
        });

    } catch (e) {
        res.status(500).send({
            message: err.message
        });
    }
};

// update an AchievementItem with challengeName and challenge item and studentId and activityId
exports.updateByChallengeNameAndChallengeItemItemAndStudentIdAndActivityId = (req, res) => {
    const challengeName = req.params.challengeName;
    const challengeItemItem = req.params.challengeItemItem;
    const studentId = req.params.studentId;
    const activityId = req.params.activityId;

    // First look for the AchievementItem whose points we want to update
    AchievementItem.findAll({
        include: [{
            model: Achievement, attributes: [],
            required: true,
            include: [{
                model: InActivityStudentParticipation, attributes: [],
                where: {
                    activityId: activityId,
                    studentId: studentId
                }
            }, {
                model: Challenge, attributes: [],
                where: {
                    name: challengeName
                }
            }]
        }, {
            model: ChallengeItem, attributes: [],
            required: true,
            where: {
                item: challengeItemItem
            }
        }],
        attributes: [
            'id',
            'achievementId',
            'challengeItemId',
            'points',
            'ChallengeItem.item',
            'Achievement.Challenge.name',
            'Achievement.InActivityStudentParticipation.activityId',
            'Achievement.InActivityStudentParticipation.studentId',
        ],
        raw: true
    }).then(data => {
        // It will return an array but by the logic of the business (game) is just one (the first one)

        const updatedChallengeItem = {
            id: req.body.id ? req.body.id : data.id,
            achievementId: req.body.achievementId ? req.body.achievementId : data.achievementId,
            challengeItemId: req.body.challengeItemId ? req.body.challengeItemId : data.challengeItemId,
            points: req.body.points ? req.body.points : data.points,
        }

        AchievementItem.update(updatedChallengeItem, {
            where: { id: data[0].id } // as mention above it's just one AchievementItem
        }).then(num => {
            if (num == 1) {
                res.send({
                    message: "AchievementItem points was updated successfully."
                });
            } else {
                res.send({
                    message: `Cannot update AchievementItem with id=${data[0].id}. Maybe AchievementItem was not found or req.body is empty!`
                });
            }
        }).catch(err => {
            res.status(500).send({
                message: "Error updating AchievementItem with id=" + data[0].id
            });
        });
    }).catch(err => {
        res.status(500).send({
            message: err.message
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