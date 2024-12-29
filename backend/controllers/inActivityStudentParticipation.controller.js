const db = require("../models");
const InActivityStudentParticipation = db.inActivityStudentParticipation;
const Achievement = db.achievement;
const AchievementItem = db.achievementItem;
const Activity = db.activity;
const Op = db.Sequelize.Op;
const Sequelize = db.Sequelize;

// Create and Save a new InActivityStudentParticipation
exports.create = (req, res) => {
    // Validate request
    if (!req.body.teamId || !req.body.activityId || !req.body.studentId ||
        !req.body.state || !req.body.order) {
        res.status(400).send({
            message: "Content can not be empty!"
        });
        return;
    }

    // Create a InActivityStudentParticipation
    const inActivityStudentParticipation = {
        teamId: req.body.teamId,
        activityId: req.body.activityId,
        studentId: req.body.studentId,
        state: req.body.state,
        order: req.body.order
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

// Retrieve all InActivityStudentParticipations in an Activity from the database. (students participating in an activity)
exports.findAllByActivityId = (req, res) => {
    const activityId = req.params.activityId;

    InActivityStudentParticipation.findAll({ where: { activityId: activityId } })
        .then(data => {
            res.send(data);
        })
        .catch(err => {
            res.status(500).send({
                message: err.message || "Some error occurred while retrieving inActivityStudentParticipations."
            });
        });
};

// Retrieve all InActivityStudentParticipation with points in an Activity (points for each student in an activity)
exports.findAllByActivityIdWithPoints = (req, res) => {
    const activityId = req.params.activityId;

    InActivityStudentParticipation.findAll({
        group: ['studentId', 'teamId', 'activityId'],
        include: [{
            model: Achievement, attributes: [],
            include: [{ model: AchievementItem, attributes: [] }]
        },
            // { //in CASE is necessary. It works too. I don't need it now. Maybe in the future.
            //     model: User,
            //     attributes: [['username', 'username'], ['nickname', 'nickname']]
            // }, {
            //     model: Team,
            //     attributes: [['id', 'teamId'], ['name', 'name']]
            // }
        ],
        attributes: [
            'studentId',
            'teamId',
            'activityId',
            [Sequelize.fn('SUM', Sequelize.col('Achievements.AchievementItems.points')), 'points']],
        where: {
            activityId: activityId
        },
        raw: true
    }).then(data => {
        res.send(data);
    }).catch(err => {
        res.status(500).send({
            message: err.message || "Some error occurred while retrieving inActivityStudentParticipations."
        });
    });
};

// Retrieve all InActivityStudentParticipation in an Activity for a user (student in an activity)
exports.findAllByActivityIdAndStudentId = (req, res) => {
    const activityId = req.params.activityId;
    const studentId = req.params.studentId;

    InActivityStudentParticipation.findAll({ where: { activityId: activityId, studentId: studentId } })
        .then(data => {
            res.send(data);
        })
        .catch(err => {
            res.status(500).send({
                message: err.message || "Some error occurred while retrieving inActivityStudentParticipations."
            });
        });
};

// Retrieve all InActivityStudentParticipation by a user (This is the learning path of a student)
exports.findLearningPath = (req, res) => {
    const studentId = req.params.studentId;

    InActivityStudentParticipation.findAll({
        group: ['studentId', 'teamId', 'activityId', 'state', 'order'],
        order: [['order', 'ASC']],
        include: [{
            model: Achievement, attributes: [],
            include: [{ model: AchievementItem, attributes: [] }]
        },
        {
            model: Activity,
            attributes: []
        }
        ],
        attributes: [
            'teamId',
            'activityId',
            'studentId',
            'state',
            'order',
            [Sequelize.col("Activity.startDate"), "activityStartDate"],
            [Sequelize.col("Activity.endDate"), "activityEndDate"],
            [Sequelize.col("Activity.state"), "activityState"],
            [Sequelize.col("Activity.type"), "activityType"],
            [Sequelize.col("Activity.name"), "activityName"],
            [Sequelize.col("Activity.description"), "activityDescription"],
            [Sequelize.fn('SUM', Sequelize.col('Achievements.AchievementItems.points')), 'points']
        ],
        where: {
            studentId: studentId
        },
        raw: true
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