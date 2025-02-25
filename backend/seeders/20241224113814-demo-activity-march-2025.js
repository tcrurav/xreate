'use strict';

require('dotenv').config();
const bcrypt = require('bcryptjs');

/** @type {import('sequelize-cli').Migration} */
module.exports = {
  async up(queryInterface, Sequelize) {

    // Users - Students - 10 students - 2 students from each country

    await queryInterface.bulkInsert('users', [{
      id: 10,
      username: "spanish1",
      password: bcrypt.hashSync("1234"),
      nickname: "spanish1",
      code: "1234",
      role: "STUDENT",
      nationality: "spanish",
      createdAt: new Date(),
      updatedAt: new Date()
    }], {});

    await queryInterface.bulkInsert('users', [{
      id: 11,
      username: "spanish2",
      password: bcrypt.hashSync("1234"),
      nickname: "spanish2",
      code: "1234",
      role: "STUDENT",
      nationality: "spanish",
      createdAt: new Date(),
      updatedAt: new Date()
    }], {});

    await queryInterface.bulkInsert('users', [{
      id: 20,
      username: "swiss1",
      password: bcrypt.hashSync("1234"),
      nickname: "swiss1",
      code: "1234",
      role: "STUDENT",
      nationality: "swiss",
      createdAt: new Date(),
      updatedAt: new Date()
    }], {});

    await queryInterface.bulkInsert('users', [{
      id: 21,
      username: "swiss2",
      password: bcrypt.hashSync("1234"),
      nickname: "swiss2",
      code: "1234",
      role: "STUDENT",
      nationality: "swiss",
      createdAt: new Date(),
      updatedAt: new Date()
    }], {});

    await queryInterface.bulkInsert('users', [{
      id: 30,
      username: "finish1",
      password: bcrypt.hashSync("1234"),
      nickname: "finish1",
      code: "1234",
      role: "STUDENT",
      nationality: "finish",
      createdAt: new Date(),
      updatedAt: new Date()
    }], {});

    await queryInterface.bulkInsert('users', [{
      id: 31,
      username: "finish2",
      password: bcrypt.hashSync("1234"),
      nickname: "finish2",
      code: "1234",
      role: "STUDENT",
      nationality: "finish",
      createdAt: new Date(),
      updatedAt: new Date()
    }], {});

    await queryInterface.bulkInsert('users', [{
      id: 40,
      username: "english1",
      password: bcrypt.hashSync("1234"),
      nickname: "english1",
      code: "1234",
      role: "STUDENT",
      nationality: "english",
      createdAt: new Date(),
      updatedAt: new Date()
    }], {});

    await queryInterface.bulkInsert('users', [{
      id: 41,
      username: "english2",
      password: bcrypt.hashSync("1234"),
      nickname: "english2",
      code: "1234",
      role: "STUDENT",
      nationality: "english",
      createdAt: new Date(),
      updatedAt: new Date()
    }], {});

    await queryInterface.bulkInsert('users', [{
      id: 50,
      username: "dutch1",
      password: bcrypt.hashSync("1234"),
      nickname: "english1",
      code: "1234",
      role: "STUDENT",
      nationality: "dutch",
      createdAt: new Date(),
      updatedAt: new Date()
    }], {});

    await queryInterface.bulkInsert('users', [{
      id: 51,
      username: "dutch2",
      password: bcrypt.hashSync("1234"),
      nickname: "dutch2",
      code: "1234",
      role: "STUDENT",
      nationality: "dutch",
      createdAt: new Date(),
      updatedAt: new Date()
    }], {});

    // Users - Teachers - 5 teachers - 1 teacher from each country

    await queryInterface.bulkInsert('users', [{
      id: 100,
      username: "tiburcio",
      password: bcrypt.hashSync("4321"),
      nickname: "tiburcio",
      code: "4321",
      role: "TEACHER",
      nationality: "spanish",
      createdAt: new Date(),
      updatedAt: new Date()
    }], {});

    await queryInterface.bulkInsert('users', [{
      id: 101,
      username: "silverio",
      password: bcrypt.hashSync("4321"),
      nickname: "silverio",
      code: "4321",
      role: "TEACHER",
      nationality: "spanish",
      createdAt: new Date(),
      updatedAt: new Date()
    }], {});

    await queryInterface.bulkInsert('users', [{
      id: 102,
      username: "jose",
      password: bcrypt.hashSync("4321"),
      nickname: "jose",
      code: "4321",
      role: "TEACHER",
      nationality: "spanish",
      createdAt: new Date(),
      updatedAt: new Date()
    }], {});

    await queryInterface.bulkInsert('users', [{
      id: 200,
      username: "kehl",
      password: bcrypt.hashSync("4321"),
      nickname: "kehl",
      code: "4321",
      role: "TEACHER",
      nationality: "swiss",
      createdAt: new Date(),
      updatedAt: new Date()
    }], {});

    await queryInterface.bulkInsert('users', [{
      id: 201,
      username: "Karin",
      password: bcrypt.hashSync("4321"),
      nickname: "Karin",
      code: "4321",
      role: "TEACHER",
      nationality: "swiss",
      createdAt: new Date(),
      updatedAt: new Date()
    }], {});

    await queryInterface.bulkInsert('users', [{
      id: 202,
      username: "brulisauer",
      password: bcrypt.hashSync("4321"),
      nickname: "brulisauer",
      code: "4321",
      role: "TEACHER",
      nationality: "swiss",
      createdAt: new Date(),
      updatedAt: new Date()
    }], {});

    await queryInterface.bulkInsert('users', [{
      id: 203,
      username: "ueli",
      password: bcrypt.hashSync("4321"),
      nickname: "ueli",
      code: "4321",
      role: "TEACHER",
      nationality: "swiss",
      createdAt: new Date(),
      updatedAt: new Date()
    }], {});

    await queryInterface.bulkInsert('users', [{
      id: 204,
      username: "bernana",
      password: bcrypt.hashSync("4321"),
      nickname: "bernana",
      code: "4321",
      role: "TEACHER",
      nationality: "swiss",
      createdAt: new Date(),
      updatedAt: new Date()
    }], {});

    await queryInterface.bulkInsert('users', [{
      id: 300,
      username: "hanna",
      password: bcrypt.hashSync("4321"),
      nickname: "hanna",
      code: "4321",
      role: "TEACHER",
      nationality: "finish",
      createdAt: new Date(),
      updatedAt: new Date()
    }], {});

    await queryInterface.bulkInsert('users', [{
      id: 301,
      username: "paula",
      password: bcrypt.hashSync("4321"),
      nickname: "paula",
      code: "4321",
      role: "TEACHER",
      nationality: "finish",
      createdAt: new Date(),
      updatedAt: new Date()
    }], {});

    await queryInterface.bulkInsert('users', [{
      id: 400,
      username: "paul",
      password: bcrypt.hashSync("4321"),
      nickname: "paul",
      code: "4321",
      role: "TEACHER",
      nationality: "english",
      createdAt: new Date(),
      updatedAt: new Date()
    }], {});

    await queryInterface.bulkInsert('users', [{
      id: 401,
      username: "vicky",
      password: bcrypt.hashSync("4321"),
      nickname: "vicky",
      code: "4321",
      role: "TEACHER",
      nationality: "english",
      createdAt: new Date(),
      updatedAt: new Date()
    }], {});

    await queryInterface.bulkInsert('users', [{
      id: 402,
      username: "murray",
      password: bcrypt.hashSync("4321"),
      nickname: "murray",
      code: "4321",
      role: "TEACHER",
      nationality: "english",
      createdAt: new Date(),
      updatedAt: new Date()
    }], {});

    await queryInterface.bulkInsert('users', [{
      id: 500,
      username: "lotte",
      password: bcrypt.hashSync("4321"),
      nickname: "lotte",
      code: "4321",
      role: "TEACHER",
      nationality: "dutch",
      createdAt: new Date(),
      updatedAt: new Date()
    }], {});

    await queryInterface.bulkInsert('users', [{
      id: 501,
      username: "frank",
      password: bcrypt.hashSync("4321"),
      nickname: "frank",
      code: "4321",
      role: "TEACHER",
      nationality: "dutch",
      createdAt: new Date(),
      updatedAt: new Date()
    }], {});

    await queryInterface.bulkInsert('users', [{
      id: 502,
      username: "meike",
      password: bcrypt.hashSync("4321"),
      nickname: "meike",
      code: "4321",
      role: "TEACHER",
      nationality: "dutch",
      createdAt: new Date(),
      updatedAt: new Date()
    }], {});

    await queryInterface.bulkInsert('users', [{
      id: 503,
      username: "arno",
      password: bcrypt.hashSync("4321"),
      nickname: "arno",
      code: "4321",
      role: "TEACHER",
      nationality: "dutch",
      createdAt: new Date(),
      updatedAt: new Date()
    }], {});

    // Activity - In march 2025 the learning programme for a student has 4 following activities

    const today = new Date();
    let tomorrow = today;
    tomorrow.setDate(today.getDate() + 1); //adds 1 day to the current date

    await queryInterface.bulkInsert('activities', [{
      id: 1,
      startDate: today,
      endDate: tomorrow,
      state: "NOT_STARTED",
      type: "VIRTUAL_CLASSROOM",
      name: "Introduction to digital security",
      description: "Digital security essentials to start working as an employee in any company",
      createdAt: new Date(),
      updatedAt: new Date()
    }], {});

    await queryInterface.bulkInsert('activities', [{
      id: 2,
      startDate: today,
      endDate: tomorrow,
      state: "NOT_STARTED",
      type: "TRAINING_LAB",
      name: "Digital security escape room",
      description: "A digital escape room activity to develop your digital security skills",
      createdAt: new Date(),
      updatedAt: new Date()
    }], {});

    await queryInterface.bulkInsert('activities', [{
      id: 3,
      startDate: today,
      endDate: tomorrow,
      state: "NOT_STARTED",
      type: "ASSET_LAB",
      name: "Digital security resources",
      description: "In this laboratory you can enter at any time to learn digital security concepts independently",
      createdAt: new Date(),
      updatedAt: new Date()
    }], {});

    await queryInterface.bulkInsert('activities', [{
      id: 4,
      startDate: today,
      endDate: tomorrow,
      state: "NOT_STARTED",
      type: "VIRTUAL_CLASSROOM",
      name: "Wrap up session",
      description: "Wrap up classroom session to sum up all you have learned during this digital security course",
      createdAt: new Date(),
      updatedAt: new Date()
    }], {});

    // Challenge - 4 challenges because there are 3 escape rooms and 1 corridor with asking alien

    await queryInterface.bulkInsert('challenges', [{
      id: 1,
      name: 'match-the-pairs',
      type: 'ROOM_CHALLENGE',
      createdAt: new Date(),
      updatedAt: new Date()
    }], {});

    await queryInterface.bulkInsert('challenges', [{
      id: 2,
      name: 'supervised',
      type: 'ROOM_CHALLENGE',
      createdAt: new Date(),
      updatedAt: new Date()
    }], {});

    await queryInterface.bulkInsert('challenges', [{
      id: 3,
      name: 'security concepts',
      type: 'ROOM_CHALLENGE',
      createdAt: new Date(),
      updatedAt: new Date()
    }], {});

    await queryInterface.bulkInsert('challenges', [{
      id: 4,
      name: 'security concepts corridor',
      type: 'CORRIDOR_CHALLENGE',
      createdAt: new Date(),
      updatedAt: new Date()
    }], {});

    // Challenge - 4 ActivityChallengeConfig because there are 4 challenges in the activity 2 (digital security escape room)

    await queryInterface.bulkInsert('activity_challenge_configs', [{
      id: 1,
      activityId: 2,
      challengeId: 1,
      createdAt: new Date(),
      updatedAt: new Date()
    }], {});

    await queryInterface.bulkInsert('activity_challenge_configs', [{
      id: 2,
      activityId: 2,
      challengeId: 2,
      createdAt: new Date(),
      updatedAt: new Date()
    }], {});

    await queryInterface.bulkInsert('activity_challenge_configs', [{
      id: 3,
      activityId: 2,
      challengeId: 3,
      createdAt: new Date(),
      updatedAt: new Date()
    }], {});

    await queryInterface.bulkInsert('activity_challenge_configs', [{
      id: 4,
      activityId: 2,
      challengeId: 4,
      createdAt: new Date(),
      updatedAt: new Date()
    }], {});

    // Teams - 2 teams in Activity 2 (digital security training lab) - Each team with 5 students

    await queryInterface.bulkInsert('teams', [{
      name: 'Lions',
      currentChallengeId: 1,
      createdAt: new Date(),
      updatedAt: new Date()
    }], {});

    await queryInterface.bulkInsert('teams', [{
      name: 'Tigers',
      currentChallengeId: 1,
      createdAt: new Date(),
      updatedAt: new Date()
    }], {});


    // InActivityStudentParticipation - 40 participations because there are 10 students which participate each in 4 activities

    await queryInterface.bulkInsert('in_activity_student_participations', [{
      id: 2110,
      // teamId: null, //Not important in VIRTUAL_CLASSROOM activities
      activityId: 1, 
      studentId: 10,
      state: "",
      order: 1,
      createdAt: new Date(),
      updatedAt: new Date()
    }], {});

    await queryInterface.bulkInsert('in_activity_student_participations', [{
      id: 2120,
      // teamId: null, //Not important in VIRTUAL_CLASSROOM activities
      activityId: 1,
      studentId: 20,
      state: "",
      order: 1,
      createdAt: new Date(),
      updatedAt: new Date()
    }], {});

    await queryInterface.bulkInsert('in_activity_student_participations', [{
      id: 2130,
      // teamId: null, //Not important in VIRTUAL_CLASSROOM activities
      activityId: 1,
      studentId: 30,
      state: "",
      order: 1,
      createdAt: new Date(),
      updatedAt: new Date()
    }], {});

    await queryInterface.bulkInsert('in_activity_student_participations', [{
      id: 2140,
      // teamId: null, //Not important in VIRTUAL_CLASSROOM activities
      activityId: 1,
      studentId: 40,
      state: "",
      order: 1,
      createdAt: new Date(),
      updatedAt: new Date()
    }], {});

    await queryInterface.bulkInsert('in_activity_student_participations', [{
      id: 2150,
      // teamId: null, //Not important in VIRTUAL_CLASSROOM activities
      activityId: 1,
      studentId: 50,
      state: "",
      order: 1,
      createdAt: new Date(),
      updatedAt: new Date()
    }], {});

    await queryInterface.bulkInsert('in_activity_student_participations', [{
      id: 2211,
      // teamId: null, //Not important in VIRTUAL_CLASSROOM activities
      activityId: 1,
      studentId: 11,
      state: "",
      order: 1,
      createdAt: new Date(),
      updatedAt: new Date()
    }], {});

    await queryInterface.bulkInsert('in_activity_student_participations', [{
      id: 2221,
      // teamId: null, //Not important in VIRTUAL_CLASSROOM activities
      activityId: 1,
      studentId: 21,
      state: "",
      order: 1,
      createdAt: new Date(),
      updatedAt: new Date()
    }], {});

    await queryInterface.bulkInsert('in_activity_student_participations', [{
      id: 2231,
      // teamId: null, //Not important in VIRTUAL_CLASSROOM activities
      activityId: 1,
      studentId: 31,
      state: "",
      order: 1,
      createdAt: new Date(),
      updatedAt: new Date()
    }], {});

    await queryInterface.bulkInsert('in_activity_student_participations', [{
      id: 2241,
      // teamId: null, //Not important in VIRTUAL_CLASSROOM activities
      activityId: 1,
      studentId: 41,
      state: "",
      order: 1,
      createdAt: new Date(),
      updatedAt: new Date()
    }], {});

    await queryInterface.bulkInsert('in_activity_student_participations', [{
      id: 2251,
      // teamId: null, //Not important in VIRTUAL_CLASSROOM activities
      activityId: 1,
      studentId: 51,
      state: "",
      order: 1,
      createdAt: new Date(),
      updatedAt: new Date()
    }], {});

    await queryInterface.bulkInsert('in_activity_student_participations', [{
      id: 1110,
      teamId: 1,
      activityId: 2, // Training lab - digital security
      studentId: 10,
      state: "",
      order: 2,
      createdAt: new Date(),
      updatedAt: new Date()
    }], {});

    await queryInterface.bulkInsert('in_activity_student_participations', [{
      id: 1120,
      teamId: 1,
      activityId: 2,
      studentId: 20,
      state: "",
      order: 2,
      createdAt: new Date(),
      updatedAt: new Date()
    }], {});

    await queryInterface.bulkInsert('in_activity_student_participations', [{
      id: 1130,
      teamId: 1,
      activityId: 2,
      studentId: 30,
      state: "",
      order: 2,
      createdAt: new Date(),
      updatedAt: new Date()
    }], {});

    await queryInterface.bulkInsert('in_activity_student_participations', [{
      id: 1140,
      teamId: 1,
      activityId: 2,
      studentId: 40,
      state: "",
      order: 2,
      createdAt: new Date(),
      updatedAt: new Date()
    }], {});

    await queryInterface.bulkInsert('in_activity_student_participations', [{
      id: 1150,
      teamId: 1,
      activityId: 2,
      studentId: 50,
      state: "",
      order: 2,
      createdAt: new Date(),
      updatedAt: new Date()
    }], {});

    await queryInterface.bulkInsert('in_activity_student_participations', [{
      id: 1211,
      teamId: 2,
      activityId: 2,
      studentId: 11,
      state: "",
      order: 2,
      createdAt: new Date(),
      updatedAt: new Date()
    }], {});

    await queryInterface.bulkInsert('in_activity_student_participations', [{
      id: 1221,
      teamId: 2,
      activityId: 2,
      studentId: 21,
      state: "",
      order: 2,
      createdAt: new Date(),
      updatedAt: new Date()
    }], {});

    await queryInterface.bulkInsert('in_activity_student_participations', [{
      id: 1231,
      teamId: 2,
      activityId: 2,
      studentId: 31,
      state: "",
      order: 2,
      createdAt: new Date(),
      updatedAt: new Date()
    }], {});

    await queryInterface.bulkInsert('in_activity_student_participations', [{
      id: 1241,
      teamId: 2,
      activityId: 2,
      studentId: 41,
      state: "",
      order: 2,
      createdAt: new Date(),
      updatedAt: new Date()
    }], {});

    await queryInterface.bulkInsert('in_activity_student_participations', [{
      id: 1251,
      teamId: 2,
      activityId: 2,
      studentId: 51,
      state: "",
      order: 2,
      createdAt: new Date(),
      updatedAt: new Date()
    }], {});

    await queryInterface.bulkInsert('in_activity_student_participations', [{
      id: 3110,
      // teamId: null, //Not important in ASSET LAB activities
      activityId: 3,  // Asset lab
      studentId: 10,
      state: "",
      order: 3,
      createdAt: new Date(),
      updatedAt: new Date()
    }], {});

    await queryInterface.bulkInsert('in_activity_student_participations', [{
      id: 3120,
      // teamId: null, //Not important in ASSET LAB activities
      activityId: 3,  // Asset lab
      studentId: 20,
      state: "",
      order: 3,
      createdAt: new Date(),
      updatedAt: new Date()
    }], {});

    await queryInterface.bulkInsert('in_activity_student_participations', [{
      id: 3130,
      // teamId: null, //Not important in ASSET LAB activities
      activityId: 3,  // Asset lab
      studentId: 30,
      state: "",
      order: 3,
      createdAt: new Date(),
      updatedAt: new Date()
    }], {});

    await queryInterface.bulkInsert('in_activity_student_participations', [{
      id: 3140,
      // teamId: null, //Not important in ASSET LAB activities
      activityId: 3,  // Asset lab
      studentId: 40,
      state: "",
      order: 3,
      createdAt: new Date(),
      updatedAt: new Date()
    }], {});

    await queryInterface.bulkInsert('in_activity_student_participations', [{
      id: 3150,
      // teamId: null, //Not important in ASSET LAB activities
      activityId: 3,  // Asset lab
      studentId: 50,
      state: "",
      order: 3,
      createdAt: new Date(),
      updatedAt: new Date()
    }], {});

    await queryInterface.bulkInsert('in_activity_student_participations', [{
      id: 3211,
      // teamId: null, //Not important in ASSET LAB activities
      activityId: 3,  // Asset lab
      studentId: 11,
      state: "",
      order: 3,
      createdAt: new Date(),
      updatedAt: new Date()
    }], {});

    await queryInterface.bulkInsert('in_activity_student_participations', [{
      id: 3221,
      // teamId: null, //Not important in ASSET LAB activities
      activityId: 3,  // Asset lab
      studentId: 21,
      state: "",
      order: 3,
      createdAt: new Date(),
      updatedAt: new Date()
    }], {});

    await queryInterface.bulkInsert('in_activity_student_participations', [{
      id: 3231,
      // teamId: null, //Not important in ASSET LAB activities
      activityId: 3,  // Asset lab
      studentId: 31,
      state: "",
      order: 3,
      createdAt: new Date(),
      updatedAt: new Date()
    }], {});

    await queryInterface.bulkInsert('in_activity_student_participations', [{
      id: 3241,
      // teamId: null, //Not important in ASSET LAB activities
      activityId: 3,  // Asset lab
      studentId: 41,
      state: "",
      order: 3,
      createdAt: new Date(),
      updatedAt: new Date()
    }], {});

    await queryInterface.bulkInsert('in_activity_student_participations', [{
      id: 3251,
      // teamId: null, //Not important in ASSET LAB activities
      activityId: 3,  // Asset lab
      studentId: 51,
      state: "",
      order: 3,
      createdAt: new Date(),
      updatedAt: new Date()
    }], {});

    await queryInterface.bulkInsert('in_activity_student_participations', [{
      id: 4110,
      // teamId: null, //Not important in VIRTUAL_CLASSROOM activities
      activityId: 4,
      studentId: 10,
      state: "",
      order: 4,
      createdAt: new Date(),
      updatedAt: new Date()
    }], {});

    await queryInterface.bulkInsert('in_activity_student_participations', [{
      id: 4120,
      // teamId: null, //Not important in VIRTUAL_CLASSROOM activities
      activityId: 4,
      studentId: 20,
      state: "",
      order: 4,
      createdAt: new Date(),
      updatedAt: new Date()
    }], {});

    await queryInterface.bulkInsert('in_activity_student_participations', [{
      id: 4130,
      // teamId: null, //Not important in VIRTUAL_CLASSROOM activities
      activityId: 4,
      studentId: 30,
      state: "",
      order: 4,
      createdAt: new Date(),
      updatedAt: new Date()
    }], {});

    await queryInterface.bulkInsert('in_activity_student_participations', [{
      id: 4140,
      // teamId: null, //Not important in VIRTUAL_CLASSROOM activities
      activityId: 4,
      studentId: 40,
      state: "",
      order: 4,
      createdAt: new Date(),
      updatedAt: new Date()
    }], {});

    await queryInterface.bulkInsert('in_activity_student_participations', [{
      id: 4150,
      // teamId: null, //Not important in VIRTUAL_CLASSROOM activities
      activityId: 4,
      studentId: 50,
      state: "",
      order: 4,
      createdAt: new Date(),
      updatedAt: new Date()
    }], {});

    await queryInterface.bulkInsert('in_activity_student_participations', [{
      id: 4211,
      // teamId: null, //Not important in VIRTUAL_CLASSROOM activities
      activityId: 4,
      studentId: 11,
      state: "",
      order: 4,
      createdAt: new Date(),
      updatedAt: new Date()
    }], {});

    await queryInterface.bulkInsert('in_activity_student_participations', [{
      id: 4221,
      // teamId: null, //Not important in VIRTUAL_CLASSROOM activities
      activityId: 4,
      studentId: 21,
      state: "",
      order: 4,
      createdAt: new Date(),
      updatedAt: new Date()
    }], {});

    await queryInterface.bulkInsert('in_activity_student_participations', [{
      id: 4231,
      // teamId: null, //Not important in VIRTUAL_CLASSROOM activities
      activityId: 4,
      studentId: 31,
      state: "",
      order: 4,
      createdAt: new Date(),
      updatedAt: new Date()
    }], {});

    await queryInterface.bulkInsert('in_activity_student_participations', [{
      id: 4241,
      // teamId: null, //Not important in VIRTUAL_CLASSROOM activities
      activityId: 4,
      studentId: 41,
      state: "",
      order: 4,
      createdAt: new Date(),
      updatedAt: new Date()
    }], {});

    await queryInterface.bulkInsert('in_activity_student_participations', [{
      id: 4251,
      // teamId: null, //Not important in VIRTUAL_CLASSROOM activities
      activityId: 4,
      studentId: 51,
      state: "",
      order: 4,
      createdAt: new Date(),
      updatedAt: new Date()
    }], {});

    // ChallengeItem - 4 challenge_items because each challenge has 1 item. There 4 challenges: 3 room challenges and 1 corridor challenge.

    await queryInterface.bulkInsert('challenge_items', [{
      id: 11,
      challengeId: '1',
      item: 'total points of a student in this challenge',
      points: 1,
      createdAt: new Date(),
      updatedAt: new Date()
    }], {});

    await queryInterface.bulkInsert('challenge_items', [{
      id: 21,
      challengeId: '2',
      item: 'total points of a student in this challenge',
      points: 1,
      createdAt: new Date(),
      updatedAt: new Date()
    }], {});

    await queryInterface.bulkInsert('challenge_items', [{
      id: 31,
      challengeId: '3',
      item: 'total points of a student in this challenge',
      points: 1,
      createdAt: new Date(),
      updatedAt: new Date()
    }], {});

    await queryInterface.bulkInsert('challenge_items', [{
      id: 41,
      challengeId: '4',
      item: 'total points of a student in this challenge',
      points: 1,
      createdAt: new Date(),
      updatedAt: new Date()
    }], {});

    // Achievement - 40 achievements because each student does 4 challenges (3 rooms and 1 corridor) and there are 10 students.

    await queryInterface.bulkInsert('achievements', [{
      id: 11110,
      challengeId: '1',
      inActivityStudentParticipationId: 1110,
      createdAt: new Date(),
      updatedAt: new Date()
    }], {});

    await queryInterface.bulkInsert('achievements', [{
      id: 21110,
      challengeId: '2',
      inActivityStudentParticipationId: 1110,
      createdAt: new Date(),
      updatedAt: new Date()
    }], {});

    await queryInterface.bulkInsert('achievements', [{
      id: 31110,
      challengeId: '3',
      inActivityStudentParticipationId: 1110,
      createdAt: new Date(),
      updatedAt: new Date()
    }], {});

    await queryInterface.bulkInsert('achievements', [{
      id: 41110,
      challengeId: '4',
      inActivityStudentParticipationId: 1110,
      createdAt: new Date(),
      updatedAt: new Date()
    }], {});

    await queryInterface.bulkInsert('achievements', [{
      id: 11120,
      challengeId: '1',
      inActivityStudentParticipationId: 1120,
      createdAt: new Date(),
      updatedAt: new Date()
    }], {});

    await queryInterface.bulkInsert('achievements', [{
      id: 21120,
      challengeId: '2',
      inActivityStudentParticipationId: 1120,
      createdAt: new Date(),
      updatedAt: new Date()
    }], {});

    await queryInterface.bulkInsert('achievements', [{
      id: 31120,
      challengeId: '3',
      inActivityStudentParticipationId: 1120,
      createdAt: new Date(),
      updatedAt: new Date()
    }], {});

    await queryInterface.bulkInsert('achievements', [{
      id: 41120,
      challengeId: '4',
      inActivityStudentParticipationId: 1120,
      createdAt: new Date(),
      updatedAt: new Date()
    }], {});

    await queryInterface.bulkInsert('achievements', [{
      id: 11130,
      challengeId: '1',
      inActivityStudentParticipationId: 1130,
      createdAt: new Date(),
      updatedAt: new Date()
    }], {});

    await queryInterface.bulkInsert('achievements', [{
      id: 21130,
      challengeId: '2',
      inActivityStudentParticipationId: 1130,
      createdAt: new Date(),
      updatedAt: new Date()
    }], {});

    await queryInterface.bulkInsert('achievements', [{
      id: 31130,
      challengeId: '3',
      inActivityStudentParticipationId: 1130,
      createdAt: new Date(),
      updatedAt: new Date()
    }], {});

    await queryInterface.bulkInsert('achievements', [{
      id: 41130,
      challengeId: '4',
      inActivityStudentParticipationId: 1130,
      createdAt: new Date(),
      updatedAt: new Date()
    }], {});

    await queryInterface.bulkInsert('achievements', [{
      id: 11140,
      challengeId: '1',
      inActivityStudentParticipationId: 1140,
      createdAt: new Date(),
      updatedAt: new Date()
    }], {});

    await queryInterface.bulkInsert('achievements', [{
      id: 21140,
      challengeId: '2',
      inActivityStudentParticipationId: 1140,
      createdAt: new Date(),
      updatedAt: new Date()
    }], {});

    await queryInterface.bulkInsert('achievements', [{
      id: 31140,
      challengeId: '3',
      inActivityStudentParticipationId: 1140,
      createdAt: new Date(),
      updatedAt: new Date()
    }], {});

    await queryInterface.bulkInsert('achievements', [{
      id: 41140,
      challengeId: '4',
      inActivityStudentParticipationId: 1140,
      createdAt: new Date(),
      updatedAt: new Date()
    }], {});

    await queryInterface.bulkInsert('achievements', [{
      id: 11150,
      challengeId: '1',
      inActivityStudentParticipationId: 1150,
      createdAt: new Date(),
      updatedAt: new Date()
    }], {});

    await queryInterface.bulkInsert('achievements', [{
      id: 21150,
      challengeId: '2',
      inActivityStudentParticipationId: 1150,
      createdAt: new Date(),
      updatedAt: new Date()
    }], {});

    await queryInterface.bulkInsert('achievements', [{
      id: 31150,
      challengeId: '3',
      inActivityStudentParticipationId: 1150,
      createdAt: new Date(),
      updatedAt: new Date()
    }], {});

    await queryInterface.bulkInsert('achievements', [{
      id: 41150,
      challengeId: '4',
      inActivityStudentParticipationId: 1150,
      createdAt: new Date(),
      updatedAt: new Date()
    }], {});

    await queryInterface.bulkInsert('achievements', [{
      id: 11211,
      challengeId: '1',
      inActivityStudentParticipationId: 1211,
      createdAt: new Date(),
      updatedAt: new Date()
    }], {});

    await queryInterface.bulkInsert('achievements', [{
      id: 21211,
      challengeId: '2',
      inActivityStudentParticipationId: 1211,
      createdAt: new Date(),
      updatedAt: new Date()
    }], {});

    await queryInterface.bulkInsert('achievements', [{
      id: 31211,
      challengeId: '3',
      inActivityStudentParticipationId: 1211,
      createdAt: new Date(),
      updatedAt: new Date()
    }], {});

    await queryInterface.bulkInsert('achievements', [{
      id: 41211,
      challengeId: '4',
      inActivityStudentParticipationId: 1211,
      createdAt: new Date(),
      updatedAt: new Date()
    }], {});

    await queryInterface.bulkInsert('achievements', [{
      id: 11221,
      challengeId: '1',
      inActivityStudentParticipationId: 1221,
      createdAt: new Date(),
      updatedAt: new Date()
    }], {});

    await queryInterface.bulkInsert('achievements', [{
      id: 21221,
      challengeId: '2',
      inActivityStudentParticipationId: 1221,
      createdAt: new Date(),
      updatedAt: new Date()
    }], {});

    await queryInterface.bulkInsert('achievements', [{
      id: 31221,
      challengeId: '3',
      inActivityStudentParticipationId: 1221,
      createdAt: new Date(),
      updatedAt: new Date()
    }], {});

    await queryInterface.bulkInsert('achievements', [{
      id: 41221,
      challengeId: '4',
      inActivityStudentParticipationId: 1221,
      createdAt: new Date(),
      updatedAt: new Date()
    }], {});

    await queryInterface.bulkInsert('achievements', [{
      id: 11231,
      challengeId: '1',
      inActivityStudentParticipationId: 1231,
      createdAt: new Date(),
      updatedAt: new Date()
    }], {});

    await queryInterface.bulkInsert('achievements', [{
      id: 21231,
      challengeId: '2',
      inActivityStudentParticipationId: 1231,
      createdAt: new Date(),
      updatedAt: new Date()
    }], {});

    await queryInterface.bulkInsert('achievements', [{
      id: 31231,
      challengeId: '3',
      inActivityStudentParticipationId: 1231,
      createdAt: new Date(),
      updatedAt: new Date()
    }], {});

    await queryInterface.bulkInsert('achievements', [{
      id: 41231,
      challengeId: '4',
      inActivityStudentParticipationId: 1231,
      createdAt: new Date(),
      updatedAt: new Date()
    }], {});

    await queryInterface.bulkInsert('achievements', [{
      id: 11241,
      challengeId: '1',
      inActivityStudentParticipationId: 1241,
      createdAt: new Date(),
      updatedAt: new Date()
    }], {});

    await queryInterface.bulkInsert('achievements', [{
      id: 21241,
      challengeId: '2',
      inActivityStudentParticipationId: 1241,
      createdAt: new Date(),
      updatedAt: new Date()
    }], {});

    await queryInterface.bulkInsert('achievements', [{
      id: 31241,
      challengeId: '3',
      inActivityStudentParticipationId: 1241,
      createdAt: new Date(),
      updatedAt: new Date()
    }], {});

    await queryInterface.bulkInsert('achievements', [{
      id: 41241,
      challengeId: '4',
      inActivityStudentParticipationId: 1241,
      createdAt: new Date(),
      updatedAt: new Date()
    }], {});

    await queryInterface.bulkInsert('achievements', [{
      id: 11251,
      challengeId: '1',
      inActivityStudentParticipationId: 1251,
      createdAt: new Date(),
      updatedAt: new Date()
    }], {});

    await queryInterface.bulkInsert('achievements', [{
      id: 21251,
      challengeId: '2',
      inActivityStudentParticipationId: 1251,
      createdAt: new Date(),
      updatedAt: new Date()
    }], {});

    await queryInterface.bulkInsert('achievements', [{
      id: 31251,
      challengeId: '3',
      inActivityStudentParticipationId: 1251,
      createdAt: new Date(),
      updatedAt: new Date()
    }], {});

    await queryInterface.bulkInsert('achievements', [{
      id: 41251,
      challengeId: '4',
      inActivityStudentParticipationId: 1251,
      createdAt: new Date(),
      updatedAt: new Date()
    }], {});

    // AchievementItem - 40 achievementItems because there are 10 students. 
    // Each student does 4 challenges (3 room challenges and 1 corridor challenge).
    // Each room challenge has 1 item, and each corridor challenge has 1 item.

    await queryInterface.bulkInsert('achievement_items', [{
      achievementId: 11110,
      points: 1,
      challengeItemId: 11,
      createdAt: new Date(),
      updatedAt: new Date()
    }], {});

    await queryInterface.bulkInsert('achievement_items', [{
      achievementId: 21110,
      points: 1,
      challengeItemId: 21,
      createdAt: new Date(),
      updatedAt: new Date()
    }], {});

    await queryInterface.bulkInsert('achievement_items', [{
      achievementId: 31110,
      points: 1,
      challengeItemId: 31,
      createdAt: new Date(),
      updatedAt: new Date()
    }], {});

    await queryInterface.bulkInsert('achievement_items', [{
      achievementId: 41110,
      points: 1,
      challengeItemId: 41,
      createdAt: new Date(),
      updatedAt: new Date()
    }], {});

    await queryInterface.bulkInsert('achievement_items', [{
      achievementId: 11120,
      points: 1,
      challengeItemId: 11,
      createdAt: new Date(),
      updatedAt: new Date()
    }], {});

    await queryInterface.bulkInsert('achievement_items', [{
      achievementId: 21120,
      points: 1,
      challengeItemId: 21,
      createdAt: new Date(),
      updatedAt: new Date()
    }], {});

    await queryInterface.bulkInsert('achievement_items', [{
      achievementId: 31120,
      points: 1,
      challengeItemId: 31,
      createdAt: new Date(),
      updatedAt: new Date()
    }], {});

    await queryInterface.bulkInsert('achievement_items', [{
      achievementId: 41120,
      points: 1,
      challengeItemId: 41,
      createdAt: new Date(),
      updatedAt: new Date()
    }], {});

    await queryInterface.bulkInsert('achievement_items', [{
      achievementId: 11130,
      points: 1,
      challengeItemId: 11,
      createdAt: new Date(),
      updatedAt: new Date()
    }], {});

    await queryInterface.bulkInsert('achievement_items', [{
      achievementId: 21130,
      points: 1,
      challengeItemId: 21,
      createdAt: new Date(),
      updatedAt: new Date()
    }], {});

    await queryInterface.bulkInsert('achievement_items', [{
      achievementId: 31130,
      points: 1,
      challengeItemId: 31,
      createdAt: new Date(),
      updatedAt: new Date()
    }], {});

    await queryInterface.bulkInsert('achievement_items', [{
      achievementId: 41130,
      points: 1,
      challengeItemId: 41,
      createdAt: new Date(),
      updatedAt: new Date()
    }], {});

    await queryInterface.bulkInsert('achievement_items', [{
      achievementId: 11140,
      points: 1,
      challengeItemId: 11,
      createdAt: new Date(),
      updatedAt: new Date()
    }], {});

    await queryInterface.bulkInsert('achievement_items', [{
      achievementId: 21140,
      points: 1,
      challengeItemId: 21,
      createdAt: new Date(),
      updatedAt: new Date()
    }], {});

    await queryInterface.bulkInsert('achievement_items', [{
      achievementId: 31140,
      points: 1,
      challengeItemId: 31,
      createdAt: new Date(),
      updatedAt: new Date()
    }], {});

    await queryInterface.bulkInsert('achievement_items', [{
      achievementId: 41140,
      points: 1,
      challengeItemId: 41,
      createdAt: new Date(),
      updatedAt: new Date()
    }], {});

    await queryInterface.bulkInsert('achievement_items', [{
      achievementId: 11150,
      points: 1,
      challengeItemId: 11,
      createdAt: new Date(),
      updatedAt: new Date()
    }], {});

    await queryInterface.bulkInsert('achievement_items', [{
      achievementId: 21150,
      points: 1,
      challengeItemId: 21,
      createdAt: new Date(),
      updatedAt: new Date()
    }], {});

    await queryInterface.bulkInsert('achievement_items', [{
      achievementId: 31150,
      points: 1,
      challengeItemId: 31,
      createdAt: new Date(),
      updatedAt: new Date()
    }], {});

    await queryInterface.bulkInsert('achievement_items', [{
      achievementId: 41150,
      points: 1,
      challengeItemId: 41,
      createdAt: new Date(),
      updatedAt: new Date()
    }], {});

    await queryInterface.bulkInsert('achievement_items', [{
      achievementId: 11211,
      points: 1,
      challengeItemId: 11,
      createdAt: new Date(),
      updatedAt: new Date()
    }], {});

    await queryInterface.bulkInsert('achievement_items', [{
      achievementId: 21211,
      points: 1,
      challengeItemId: 21,
      createdAt: new Date(),
      updatedAt: new Date()
    }], {});

    await queryInterface.bulkInsert('achievement_items', [{
      achievementId: 31211,
      points: 1,
      challengeItemId: 31,
      createdAt: new Date(),
      updatedAt: new Date()
    }], {});

    await queryInterface.bulkInsert('achievement_items', [{
      achievementId: 41211,
      points: 1,
      challengeItemId: 41,
      createdAt: new Date(),
      updatedAt: new Date()
    }], {});

    await queryInterface.bulkInsert('achievement_items', [{
      achievementId: 11221,
      points: 1,
      challengeItemId: 11,
      createdAt: new Date(),
      updatedAt: new Date()
    }], {});

    await queryInterface.bulkInsert('achievement_items', [{
      achievementId: 21221,
      points: 1,
      challengeItemId: 21,
      createdAt: new Date(),
      updatedAt: new Date()
    }], {});

    await queryInterface.bulkInsert('achievement_items', [{
      achievementId: 31221,
      points: 1,
      challengeItemId: 31,
      createdAt: new Date(),
      updatedAt: new Date()
    }], {});

    await queryInterface.bulkInsert('achievement_items', [{
      achievementId: 41221,
      points: 1,
      challengeItemId: 41,
      createdAt: new Date(),
      updatedAt: new Date()
    }], {});

    await queryInterface.bulkInsert('achievement_items', [{
      achievementId: 11231,
      points: 1,
      challengeItemId: 11,
      createdAt: new Date(),
      updatedAt: new Date()
    }], {});

    await queryInterface.bulkInsert('achievement_items', [{
      achievementId: 21231,
      points: 1,
      challengeItemId: 21,
      createdAt: new Date(),
      updatedAt: new Date()
    }], {});

    await queryInterface.bulkInsert('achievement_items', [{
      achievementId: 31231,
      points: 1,
      challengeItemId: 31,
      createdAt: new Date(),
      updatedAt: new Date()
    }], {});

    await queryInterface.bulkInsert('achievement_items', [{
      achievementId: 41231,
      points: 1,
      challengeItemId: 41,
      createdAt: new Date(),
      updatedAt: new Date()
    }], {});

    await queryInterface.bulkInsert('achievement_items', [{
      achievementId: 11241,
      points: 1,
      challengeItemId: 11,
      createdAt: new Date(),
      updatedAt: new Date()
    }], {});

    await queryInterface.bulkInsert('achievement_items', [{
      achievementId: 21241,
      points: 1,
      challengeItemId: 21,
      createdAt: new Date(),
      updatedAt: new Date()
    }], {});

    await queryInterface.bulkInsert('achievement_items', [{
      achievementId: 31241,
      points: 1,
      challengeItemId: 31,
      createdAt: new Date(),
      updatedAt: new Date()
    }], {});

    await queryInterface.bulkInsert('achievement_items', [{
      achievementId: 41241,
      points: 1,
      challengeItemId: 41,
      createdAt: new Date(),
      updatedAt: new Date()
    }], {});

    await queryInterface.bulkInsert('achievement_items', [{
      achievementId: 11251,
      points: 1,
      challengeItemId: 11,
      createdAt: new Date(),
      updatedAt: new Date()
    }], {});

    await queryInterface.bulkInsert('achievement_items', [{
      achievementId: 21251,
      points: 1,
      challengeItemId: 21,
      createdAt: new Date(),
      updatedAt: new Date()
    }], {});

    await queryInterface.bulkInsert('achievement_items', [{
      achievementId: 31251,
      points: 1,
      challengeItemId: 31,
      createdAt: new Date(),
      updatedAt: new Date()
    }], {});

    await queryInterface.bulkInsert('achievement_items', [{
      achievementId: 41251,
      points: 1,
      challengeItemId: 41,
      createdAt: new Date(),
      updatedAt: new Date()
    }], {});

    await queryInterface.bulkInsert('in_activity_teacher_participations', [{
      //challengeId: null, // NOT done yet in CLASSROOM_ACTIVITY
      activityId: 1,
      teacherId: 100,
      state: "",
      order: 1,
      createdAt: new Date(),
      updatedAt: new Date()
    }], {});

    await queryInterface.bulkInsert('in_activity_teacher_participations', [{
      challengeId: 4,
      activityId: 2,
      teacherId: 100,
      state: "",
      order: 2,
      createdAt: new Date(),
      updatedAt: new Date()
    }], {});

    await queryInterface.bulkInsert('in_activity_teacher_participations', [{
      //challengeId: null, // NOT done yet in CLASSROOM_ACTIVITY
      activityId: 1,
      teacherId: 101,
      state: "",
      order: 1,
      createdAt: new Date(),
      updatedAt: new Date()
    }], {});

    await queryInterface.bulkInsert('in_activity_teacher_participations', [{
      challengeId: 4,
      activityId: 2,
      teacherId: 101,
      state: "",
      order: 2,
      createdAt: new Date(),
      updatedAt: new Date()
    }], {});

    await queryInterface.bulkInsert('in_activity_teacher_participations', [{
      //challengeId: null, // NOT done yet in CLASSROOM_ACTIVITY
      activityId: 1,
      teacherId: 102,
      state: "",
      order: 1,
      createdAt: new Date(),
      updatedAt: new Date()
    }], {});

    await queryInterface.bulkInsert('in_activity_teacher_participations', [{
      challengeId: 4,
      activityId: 2,
      teacherId: 102,
      state: "",
      order: 2,
      createdAt: new Date(),
      updatedAt: new Date()
    }], {});

    await queryInterface.bulkInsert('in_activity_teacher_participations', [{
      //challengeId: null, // NOT done yet in CLASSROOM_ACTIVITY
      activityId: 1,
      teacherId: 200,
      state: "",
      order: 1,
      createdAt: new Date(),
      updatedAt: new Date()
    }], {});

    await queryInterface.bulkInsert('in_activity_teacher_participations', [{
      challengeId: 4,
      activityId: 2,
      teacherId: 200,
      state: "",
      order: 2,
      createdAt: new Date(),
      updatedAt: new Date()
    }], {});

    await queryInterface.bulkInsert('in_activity_teacher_participations', [{
      //challengeId: null, // NOT done yet in CLASSROOM_ACTIVITY
      activityId: 1,
      teacherId: 201,
      state: "",
      order: 1,
      createdAt: new Date(),
      updatedAt: new Date()
    }], {});

    await queryInterface.bulkInsert('in_activity_teacher_participations', [{
      challengeId: 4,
      activityId: 2,
      teacherId: 201,
      state: "",
      order: 2,
      createdAt: new Date(),
      updatedAt: new Date()
    }], {});

    await queryInterface.bulkInsert('in_activity_teacher_participations', [{
      //challengeId: null, // NOT done yet in CLASSROOM_ACTIVITY
      activityId: 1,
      teacherId: 202,
      state: "",
      order: 1,
      createdAt: new Date(),
      updatedAt: new Date()
    }], {});

    await queryInterface.bulkInsert('in_activity_teacher_participations', [{
      challengeId: 4,
      activityId: 2,
      teacherId: 202,
      state: "",
      order: 2,
      createdAt: new Date(),
      updatedAt: new Date()
    }], {});

    await queryInterface.bulkInsert('in_activity_teacher_participations', [{
      //challengeId: null, // NOT done yet in CLASSROOM_ACTIVITY
      activityId: 1,
      teacherId: 203,
      state: "",
      order: 1,
      createdAt: new Date(),
      updatedAt: new Date()
    }], {});

    await queryInterface.bulkInsert('in_activity_teacher_participations', [{
      challengeId: 4,
      activityId: 2,
      teacherId: 203,
      state: "",
      order: 2,
      createdAt: new Date(),
      updatedAt: new Date()
    }], {});

    await queryInterface.bulkInsert('in_activity_teacher_participations', [{
      //challengeId: null, // NOT done yet in CLASSROOM_ACTIVITY
      activityId: 1,
      teacherId: 204,
      state: "",
      order: 1,
      createdAt: new Date(),
      updatedAt: new Date()
    }], {});

    await queryInterface.bulkInsert('in_activity_teacher_participations', [{
      challengeId: 4,
      activityId: 2,
      teacherId: 204,
      state: "",
      order: 2,
      createdAt: new Date(),
      updatedAt: new Date()
    }], {});

    await queryInterface.bulkInsert('in_activity_teacher_participations', [{
      //challengeId: null, // NOT done yet in CLASSROOM_ACTIVITY
      activityId: 1,
      teacherId: 300,
      state: "",
      order: 1,
      createdAt: new Date(),
      updatedAt: new Date()
    }], {});

    await queryInterface.bulkInsert('in_activity_teacher_participations', [{
      challengeId: 4,
      activityId: 2,
      teacherId: 300,
      state: "",
      order: 2,
      createdAt: new Date(),
      updatedAt: new Date()
    }], {});

    await queryInterface.bulkInsert('in_activity_teacher_participations', [{
      //challengeId: null, // NOT done yet in CLASSROOM_ACTIVITY
      activityId: 1,
      teacherId: 301,
      state: "",
      order: 1,
      createdAt: new Date(),
      updatedAt: new Date()
    }], {});

    await queryInterface.bulkInsert('in_activity_teacher_participations', [{
      challengeId: 4,
      activityId: 2,
      teacherId: 301,
      state: "",
      order: 2,
      createdAt: new Date(),
      updatedAt: new Date()
    }], {});

    await queryInterface.bulkInsert('in_activity_teacher_participations', [{
      challengeId: 3,
      activityId: 2,
      teacherId: 400,
      state: "",
      order: 1,
      createdAt: new Date(),
      updatedAt: new Date()
    }], {});

    await queryInterface.bulkInsert('in_activity_teacher_participations', [{
      //challengeId: null, // NOT done yet in CLASSROOM_ACTIVITY
      activityId: 4,
      teacherId: 400,
      state: "",
      order: 2,
      createdAt: new Date(),
      updatedAt: new Date()
    }], {});

    await queryInterface.bulkInsert('in_activity_teacher_participations', [{
      //challengeId: null, // NOT done yet in CLASSROOM_ACTIVITY
      activityId: 1,
      teacherId: 401,
      state: "",
      order: 1,
      createdAt: new Date(),
      updatedAt: new Date()
    }], {});

    await queryInterface.bulkInsert('in_activity_teacher_participations', [{
      challengeId: 3,
      activityId: 2,
      teacherId: 401,
      state: "",
      order: 2,
      createdAt: new Date(),
      updatedAt: new Date()
    }], {});

    await queryInterface.bulkInsert('in_activity_teacher_participations', [{
      //challengeId: null, // NOT done yet in CLASSROOM_ACTIVITY
      activityId: 1,
      teacherId: 402,
      state: "",
      order: 1,
      createdAt: new Date(),
      updatedAt: new Date()
    }], {});

    await queryInterface.bulkInsert('in_activity_teacher_participations', [{
      challengeId: 3,
      activityId: 2,
      teacherId: 402,
      state: "",
      order: 2,
      createdAt: new Date(),
      updatedAt: new Date()
    }], {});

    await queryInterface.bulkInsert('in_activity_teacher_participations', [{
      //challengeId: null, // NOT done yet in CLASSROOM_ACTIVITY
      activityId: 1,
      teacherId: 500,
      state: "",
      order: 1,
      createdAt: new Date(),
      updatedAt: new Date()
    }], {});

    await queryInterface.bulkInsert('in_activity_teacher_participations', [{
      challengeId: 3,
      activityId: 2,
      teacherId: 500,
      state: "",
      order: 2,
      createdAt: new Date(),
      updatedAt: new Date()
    }], {});

    await queryInterface.bulkInsert('in_activity_teacher_participations', [{
      //challengeId: null, // NOT done yet in CLASSROOM_ACTIVITY
      activityId: 1,
      teacherId: 501,
      state: "",
      order: 1,
      createdAt: new Date(),
      updatedAt: new Date()
    }], {});

    await queryInterface.bulkInsert('in_activity_teacher_participations', [{
      challengeId: 3,
      activityId: 2,
      teacherId: 501,
      state: "",
      order: 2,
      createdAt: new Date(),
      updatedAt: new Date()
    }], {});

    await queryInterface.bulkInsert('in_activity_teacher_participations', [{
      //challengeId: null, // NOT done yet in CLASSROOM_ACTIVITY
      activityId: 1,
      teacherId: 502,
      state: "",
      order: 1,
      createdAt: new Date(),
      updatedAt: new Date()
    }], {});

    await queryInterface.bulkInsert('in_activity_teacher_participations', [{
      challengeId: 3,
      activityId: 2,
      teacherId: 502,
      state: "",
      order: 2,
      createdAt: new Date(),
      updatedAt: new Date()
    }], {});

    await queryInterface.bulkInsert('in_activity_teacher_participations', [{
      //challengeId: null, // NOT done yet in CLASSROOM_ACTIVITY
      activityId: 1,
      teacherId: 503,
      state: "",
      order: 1,
      createdAt: new Date(),
      updatedAt: new Date()
    }], {});

    await queryInterface.bulkInsert('in_activity_teacher_participations', [{
      challengeId: 3,
      activityId: 2,
      teacherId: 503,
      state: "",
      order: 2,
      createdAt: new Date(),
      updatedAt: new Date()
    }], {});

  },

  async down(queryInterface, Sequelize) {
    await queryInterface.bulkDelete('activities', null, {});
  }
};
