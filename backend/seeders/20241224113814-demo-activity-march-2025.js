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
      role: "student",
      nationality: "spanish",
    }], {});

    await queryInterface.bulkInsert('users', [{
      id: 11,
      username: "spanish2",
      password: bcrypt.hashSync("1234"),
      nickname: "spanish2",
      code: "1234",
      role: "student",
      nationality: "spanish",
    }], {});

    await queryInterface.bulkInsert('users', [{
      id: 20,
      username: "swiss1",
      password: bcrypt.hashSync("1234"),
      nickname: "swiss1",
      code: "1234",
      role: "student",
      nationality: "swiss",
    }], {});

    await queryInterface.bulkInsert('users', [{
      id: 21,
      username: "swiss2",
      password: bcrypt.hashSync("1234"),
      nickname: "swiss2",
      code: "1234",
      role: "student",
      nationality: "swiss",
    }], {});

    await queryInterface.bulkInsert('users', [{
      id: 30,
      username: "finish1",
      password: bcrypt.hashSync("1234"),
      nickname: "finish1",
      code: "1234",
      role: "student",
      nationality: "finish",
    }], {});

    await queryInterface.bulkInsert('users', [{
      id: 31,
      username: "finish2",
      password: bcrypt.hashSync("1234"),
      nickname: "finish2",
      code: "1234",
      role: "student",
      nationality: "finish",
    }], {});

    await queryInterface.bulkInsert('users', [{
      id: 40,
      username: "english1",
      password: bcrypt.hashSync("1234"),
      nickname: "english1",
      code: "1234",
      role: "student",
      nationality: "english",
    }], {});

    await queryInterface.bulkInsert('users', [{
      id: 41,
      username: "english2",
      password: bcrypt.hashSync("1234"),
      nickname: "english2",
      code: "1234",
      role: "student",
      nationality: "english",
    }], {});

    await queryInterface.bulkInsert('users', [{
      id: 50,
      username: "dutch1",
      password: bcrypt.hashSync("1234"),
      nickname: "english1",
      code: "1234",
      role: "student",
      nationality: "dutch",
    }], {});

    await queryInterface.bulkInsert('users', [{
      id: 51,
      username: "dutch2",
      password: bcrypt.hashSync("1234"),
      nickname: "dutch2",
      code: "1234",
      role: "student",
      nationality: "dutch",
    }], {});

    // Users - Teachers - 5 teachers - 1 teacher from each country

    await queryInterface.bulkInsert('users', [{
      id: 100,
      username: "tiburcio",
      password: bcrypt.hashSync("4321"),
      nickname: "tiburcio",
      code: "4321",
      role: "teacher",
      nationality: "spanish",
    }], {});

    await queryInterface.bulkInsert('users', [{
      id: 200,
      username: "daniel",
      password: bcrypt.hashSync("4321"),
      nickname: "daniel",
      code: "4321",
      role: "teacher",
      nationality: "swiss",
    }], {});

    await queryInterface.bulkInsert('users', [{
      id: 300,
      username: "hanna",
      password: bcrypt.hashSync("4321"),
      nickname: "hanna",
      code: "4321",
      role: "teacher",
      nationality: "finish",
    }], {});

    await queryInterface.bulkInsert('users', [{
      id: 400,
      username: "paul",
      password: bcrypt.hashSync("4321"),
      nickname: "paul",
      code: "4321",
      role: "teacher",
      nationality: "english",
    }], {});

    await queryInterface.bulkInsert('users', [{
      id: 500,
      username: "lotte",
      password: bcrypt.hashSync("4321"),
      nickname: "lotte",
      code: "4321",
      role: "teacher",
      nationality: "dutch",
    }], {});

    // Activity - Just 1 activity for march 2025

    const today = new Date();
    let tomorrow = today;
    tomorrow.setDate(today.getDate() + 1); //adds 1 day to the current date

    await queryInterface.bulkInsert('activities', [{
      startDate: today,
      endDate: tomorrow, 
      isStarted: true
    }], {});

    // Teams - 2 teams - Each team with 5 students

    await queryInterface.bulkInsert('teams', [{
      name: 'Lions'
    }], {});

    await queryInterface.bulkInsert('teams', [{
      name: 'Tigers'
    }], {});


    // InActivityStudentParticipation - 10 participations because there are 10 students

    await queryInterface.bulkInsert('inactivitystudentparticipations', [{
      id: 1110,
      teamId: 1,
      activityId: 1,
      studentId: 10
    }], {});

    await queryInterface.bulkInsert('inactivitystudentparticipations', [{
      id: 1120,
      teamId: 1,
      activityId: 1,
      studentId: 20
    }], {});

    await queryInterface.bulkInsert('inactivitystudentparticipations', [{
      id: 1130,
      teamId: 1,
      activityId: 1,
      studentId: 30
    }], {});

    await queryInterface.bulkInsert('inactivitystudentparticipations', [{
      id: 1140,
      teamId: 1,
      activityId: 1,
      studentId: 40
    }], {});

    await queryInterface.bulkInsert('inactivitystudentparticipations', [{
      id: 1150,
      teamId: 1,
      activityId: 1,
      studentId: 50
    }], {});

    await queryInterface.bulkInsert('inactivitystudentparticipations', [{
      id: 1211,
      teamId: 2,
      activityId: 1,
      studentId: 11
    }], {});

    await queryInterface.bulkInsert('inactivitystudentparticipations', [{
      id: 1221,
      teamId: 2,
      activityId: 1,
      studentId: 21
    }], {});

    await queryInterface.bulkInsert('inactivitystudentparticipations', [{
      id: 1231,
      teamId: 2,
      activityId: 1,
      studentId: 31
    }], {});

    await queryInterface.bulkInsert('inactivitystudentparticipations', [{
      id: 1241,
      teamId: 2,
      activityId: 1,
      studentId: 41
    }], {});

    await queryInterface.bulkInsert('inactivitystudentparticipations', [{
      id: 1251,
      teamId: 2,
      activityId: 1,
      studentId: 51
    }], {});

    // Challenge - 3 challenges because there are 3 scaperooms

    await queryInterface.bulkInsert('challenges', [{
      id: 1,
      name: 'match-the-pairs',
      type: 'match-the-pairs'
    }], {});

    await queryInterface.bulkInsert('challenges', [{
      id: 2,
      name: 'supervised',
      type: 'supervised'
    }], {});

    await queryInterface.bulkInsert('challenges', [{
      id: 3,
      name: 'security concepts',
      type: 'security concepts'
    }], {});

    // ChallengeItem - 9 challengeItems because each challenge has 3 items.

    await queryInterface.bulkInsert('challengeItems', [{
      id: 11,
      challengeId: '1',
      item: 'item question and answer',
      points: 1
    }], {});

    await queryInterface.bulkInsert('challengeItems', [{
      id: 21,
      challengeId: '2',
      item: 'item question and answer',
      points: 1
    }], {});

    await queryInterface.bulkInsert('challengeItems', [{
      id: 31,
      challengeId: '3',
      item: 'item question and answer',
      points: 1
    }], {});

    await queryInterface.bulkInsert('challengeItems', [{
      id: 12,
      challengeId: '1',
      item: 'item question and answer',
      points: 1
    }], {});

    await queryInterface.bulkInsert('challengeItems', [{
      id: 22,
      challengeId: '2',
      item: 'item question and answer',
      points: 1
    }], {});

    await queryInterface.bulkInsert('challengeItems', [{
      id: 32,
      challengeId: '3',
      item: 'item question and answer',
      points: 1
    }], {});

    await queryInterface.bulkInsert('challengeItems', [{
      id: 13,
      challengeId: '1',
      item: 'item question and answer',
      points: 1
    }], {});

    await queryInterface.bulkInsert('challengeItems', [{
      id: 23,
      challengeId: '2',
      item: 'item question and answer',
      points: 1
    }], {});

    await queryInterface.bulkInsert('challengeItems', [{
      id: 33,
      challengeId: '3',
      item: 'item question and answer',
      points: 1
    }], {});

    // Achievement - 30 achievements because each student does 3 challenges and there are 10 students.

    await queryInterface.bulkInsert('achievements', [{
      id: 11110,
      challengeId: '1',
      inActivityStudentParticipationId: 1110,
    }], {});

    await queryInterface.bulkInsert('achievements', [{
      id: 21110,
      challengeId: '2',
      inActivityStudentParticipationId: 1110,
    }], {});

    await queryInterface.bulkInsert('achievements', [{
      id: 31110,
      challengeId: '3',
      inActivityStudentParticipationId: 1110,
    }], {});

    await queryInterface.bulkInsert('achievements', [{
      id: 11120,
      challengeId: '1',
      inActivityStudentParticipationId: 1120,
    }], {});

    await queryInterface.bulkInsert('achievements', [{
      id: 21120,
      challengeId: '2',
      inActivityStudentParticipationId: 1120,
    }], {});

    await queryInterface.bulkInsert('achievements', [{
      id: 31120,
      challengeId: '3',
      inActivityStudentParticipationId: 1120,
    }], {});

    await queryInterface.bulkInsert('achievements', [{
      id: 11130,
      challengeId: '1',
      inActivityStudentParticipationId: 1130,
    }], {});

    await queryInterface.bulkInsert('achievements', [{
      id: 21130,
      challengeId: '2',
      inActivityStudentParticipationId: 1130,
    }], {});

    await queryInterface.bulkInsert('achievements', [{
      id: 31130,
      challengeId: '3',
      inActivityStudentParticipationId: 1130,
    }], {});

    await queryInterface.bulkInsert('achievements', [{
      id: 11140,
      challengeId: '1',
      inActivityStudentParticipationId: 1140,
    }], {});

    await queryInterface.bulkInsert('achievements', [{
      id: 21140,
      challengeId: '2',
      inActivityStudentParticipationId: 1140,
    }], {});

    await queryInterface.bulkInsert('achievements', [{
      id: 31140,
      challengeId: '3',
      inActivityStudentParticipationId: 1140,
    }], {});

    await queryInterface.bulkInsert('achievements', [{
      id: 11150,
      challengeId: '1',
      inActivityStudentParticipationId: 1150,
    }], {});

    await queryInterface.bulkInsert('achievements', [{
      id: 21150,
      challengeId: '2',
      inActivityStudentParticipationId: 1150,
    }], {});

    await queryInterface.bulkInsert('achievements', [{
      id: 31150,
      challengeId: '3',
      inActivityStudentParticipationId: 1150,
    }], {});

    await queryInterface.bulkInsert('achievements', [{
      id: 11211,
      challengeId: '1',
      inActivityStudentParticipationId: 1211,
    }], {});

    await queryInterface.bulkInsert('achievements', [{
      id: 21211,
      challengeId: '2',
      inActivityStudentParticipationId: 1211,
    }], {});

    await queryInterface.bulkInsert('achievements', [{
      id: 31211,
      challengeId: '3',
      inActivityStudentParticipationId: 1211,
    }], {});

    await queryInterface.bulkInsert('achievements', [{
      id: 11221,
      challengeId: '1',
      inActivityStudentParticipationId: 1221,
    }], {});

    await queryInterface.bulkInsert('achievements', [{
      id: 21221,
      challengeId: '2',
      inActivityStudentParticipationId: 1221,
    }], {});

    await queryInterface.bulkInsert('achievements', [{
      id: 31221,
      challengeId: '3',
      inActivityStudentParticipationId: 1221,
    }], {});

    await queryInterface.bulkInsert('achievements', [{
      id: 11231,
      challengeId: '1',
      inActivityStudentParticipationId: 1231,
    }], {});

    await queryInterface.bulkInsert('achievements', [{
      id: 21231,
      challengeId: '2',
      inActivityStudentParticipationId: 1231,
    }], {});

    await queryInterface.bulkInsert('achievements', [{
      id: 31231,
      challengeId: '3',
      inActivityStudentParticipationId: 1231,
    }], {});

    await queryInterface.bulkInsert('achievements', [{
      id: 11241,
      challengeId: '1',
      inActivityStudentParticipationId: 1241,
    }], {});

    await queryInterface.bulkInsert('achievements', [{
      id: 21241,
      challengeId: '2',
      inActivityStudentParticipationId: 1241,
    }], {});

    await queryInterface.bulkInsert('achievements', [{
      id: 31241,
      challengeId: '3',
      inActivityStudentParticipationId: 1241,
    }], {});

    await queryInterface.bulkInsert('achievements', [{
      id: 11251,
      challengeId: '1',
      inActivityStudentParticipationId: 1251,
    }], {});

    await queryInterface.bulkInsert('achievements', [{
      id: 21251,
      challengeId: '2',
      inActivityStudentParticipationId: 1251,
    }], {});

    await queryInterface.bulkInsert('achievements', [{
      id: 31251,
      challengeId: '3',
      inActivityStudentParticipationId: 1251,
    }], {});

    // AchievementItem - 90 achievementItems because there are 10 students. Eache student does 3 challenges and each challenge has 3 items

    await queryInterface.bulkInsert('achievementitems', [{
      achievementId: 11110,
      points: 1,
      challengeItemId: 11,
    }], {});

    await queryInterface.bulkInsert('achievementitems', [{
      achievementId: 11110,
      points: 1,
      challengeItemId: 12,
    }], {});

    await queryInterface.bulkInsert('achievementitems', [{
      achievementId: 11110,
      points: 1,
      challengeItemId: 13,
    }], {});

    await queryInterface.bulkInsert('achievementitems', [{
      achievementId: 21110,
      points: 1,
      challengeItemId: 21,
    }], {});

    await queryInterface.bulkInsert('achievementitems', [{
      achievementId: 21110,
      points: 1,
      challengeItemId: 22,
    }], {});

    await queryInterface.bulkInsert('achievementitems', [{
      achievementId: 21110,
      points: 1,
      challengeItemId: 23,
    }], {});

    await queryInterface.bulkInsert('achievementitems', [{
      achievementId: 31110,
      points: 1,
      challengeItemId: 31,
    }], {});

    await queryInterface.bulkInsert('achievementitems', [{
      achievementId: 31110,
      points: 1,
      challengeItemId: 32,
    }], {});

    await queryInterface.bulkInsert('achievementitems', [{
      achievementId: 31110,
      points: 1,
      challengeItemId: 33,
    }], {});

    await queryInterface.bulkInsert('achievementitems', [{
      achievementId: 11120,
      points: 1,
      challengeItemId: 11,
    }], {});

    await queryInterface.bulkInsert('achievementitems', [{
      achievementId: 11120,
      points: 1,
      challengeItemId: 12,
    }], {});

    await queryInterface.bulkInsert('achievementitems', [{
      achievementId: 11120,
      points: 1,
      challengeItemId: 13,
    }], {});

    await queryInterface.bulkInsert('achievementitems', [{
      achievementId: 21120,
      points: 1,
      challengeItemId: 21,
    }], {});

    await queryInterface.bulkInsert('achievementitems', [{
      achievementId: 21120,
      points: 1,
      challengeItemId: 22,
    }], {});

    await queryInterface.bulkInsert('achievementitems', [{
      achievementId: 21120,
      points: 1,
      challengeItemId: 23,
    }], {});

    await queryInterface.bulkInsert('achievementitems', [{
      achievementId: 31120,
      points: 1,
      challengeItemId: 31,
    }], {});

    await queryInterface.bulkInsert('achievementitems', [{
      achievementId: 31120,
      points: 1,
      challengeItemId: 32,
    }], {});

    await queryInterface.bulkInsert('achievementitems', [{
      achievementId: 31120,
      points: 1,
      challengeItemId: 33,
    }], {});

    await queryInterface.bulkInsert('achievementitems', [{
      achievementId: 11130,
      points: 1,
      challengeItemId: 11,
    }], {});

    await queryInterface.bulkInsert('achievementitems', [{
      achievementId: 11130,
      points: 1,
      challengeItemId: 12,
    }], {});

    await queryInterface.bulkInsert('achievementitems', [{
      achievementId: 11130,
      points: 1,
      challengeItemId: 13,
    }], {});

    await queryInterface.bulkInsert('achievementitems', [{
      achievementId: 21130,
      points: 1,
      challengeItemId: 21,
    }], {});

    await queryInterface.bulkInsert('achievementitems', [{
      achievementId: 21130,
      points: 1,
      challengeItemId: 22,
    }], {});

    await queryInterface.bulkInsert('achievementitems', [{
      achievementId: 21130,
      points: 1,
      challengeItemId: 23,
    }], {});

    await queryInterface.bulkInsert('achievementitems', [{
      achievementId: 31130,
      points: 1,
      challengeItemId: 31,
    }], {});

    await queryInterface.bulkInsert('achievementitems', [{
      achievementId: 31130,
      points: 1,
      challengeItemId: 32,
    }], {});

    await queryInterface.bulkInsert('achievementitems', [{
      achievementId: 31130,
      points: 1,
      challengeItemId: 33,
    }], {});

    await queryInterface.bulkInsert('achievementitems', [{
      achievementId: 11140,
      points: 1,
      challengeItemId: 11,
    }], {});

    await queryInterface.bulkInsert('achievementitems', [{
      achievementId: 11140,
      points: 1,
      challengeItemId: 12,
    }], {});

    await queryInterface.bulkInsert('achievementitems', [{
      achievementId: 11140,
      points: 1,
      challengeItemId: 13,
    }], {});

    await queryInterface.bulkInsert('achievementitems', [{
      achievementId: 21140,
      points: 1,
      challengeItemId: 21,
    }], {});

    await queryInterface.bulkInsert('achievementitems', [{
      achievementId: 21140,
      points: 1,
      challengeItemId: 22,
    }], {});

    await queryInterface.bulkInsert('achievementitems', [{
      achievementId: 21140,
      points: 1,
      challengeItemId: 23,
    }], {});

    await queryInterface.bulkInsert('achievementitems', [{
      achievementId: 31140,
      points: 1,
      challengeItemId: 31,
    }], {});

    await queryInterface.bulkInsert('achievementitems', [{
      achievementId: 31140,
      points: 1,
      challengeItemId: 32,
    }], {});

    await queryInterface.bulkInsert('achievementitems', [{
      achievementId: 31140,
      points: 1,
      challengeItemId: 33,
    }], {});

    await queryInterface.bulkInsert('achievementitems', [{
      achievementId: 11150,
      points: 1,
      challengeItemId: 11,
    }], {});

    await queryInterface.bulkInsert('achievementitems', [{
      achievementId: 11150,
      points: 1,
      challengeItemId: 12,
    }], {});

    await queryInterface.bulkInsert('achievementitems', [{
      achievementId: 11150,
      points: 1,
      challengeItemId: 13,
    }], {});

    await queryInterface.bulkInsert('achievementitems', [{
      achievementId: 21150,
      points: 1,
      challengeItemId: 21,
    }], {});

    await queryInterface.bulkInsert('achievementitems', [{
      achievementId: 21150,
      points: 1,
      challengeItemId: 22,
    }], {});

    await queryInterface.bulkInsert('achievementitems', [{
      achievementId: 21150,
      points: 1,
      challengeItemId: 23,
    }], {});

    await queryInterface.bulkInsert('achievementitems', [{
      achievementId: 31150,
      points: 1,
      challengeItemId: 31,
    }], {});

    await queryInterface.bulkInsert('achievementitems', [{
      achievementId: 31150,
      points: 1,
      challengeItemId: 32,
    }], {});

    await queryInterface.bulkInsert('achievementitems', [{
      achievementId: 31150,
      points: 1,
      challengeItemId: 33,
    }], {});

    await queryInterface.bulkInsert('achievementitems', [{
      achievementId: 11211,
      points: 1,
      challengeItemId: 11,
    }], {});

    await queryInterface.bulkInsert('achievementitems', [{
      achievementId: 11211,
      points: 1,
      challengeItemId: 12,
    }], {});

    await queryInterface.bulkInsert('achievementitems', [{
      achievementId: 11211,
      points: 1,
      challengeItemId: 13,
    }], {});

    await queryInterface.bulkInsert('achievementitems', [{
      achievementId: 21211,
      points: 1,
      challengeItemId: 21,
    }], {});

    await queryInterface.bulkInsert('achievementitems', [{
      achievementId: 21211,
      points: 1,
      challengeItemId: 22,
    }], {});

    await queryInterface.bulkInsert('achievementitems', [{
      achievementId: 21211,
      points: 1,
      challengeItemId: 23,
    }], {});

    await queryInterface.bulkInsert('achievementitems', [{
      achievementId: 31211,
      points: 1,
      challengeItemId: 31,
    }], {});

    await queryInterface.bulkInsert('achievementitems', [{
      achievementId: 31211,
      points: 1,
      challengeItemId: 32,
    }], {});

    await queryInterface.bulkInsert('achievementitems', [{
      achievementId: 31211,
      points: 1,
      challengeItemId: 33,
    }], {});

    await queryInterface.bulkInsert('achievementitems', [{
      achievementId: 11221,
      points: 1,
      challengeItemId: 11,
    }], {});

    await queryInterface.bulkInsert('achievementitems', [{
      achievementId: 11221,
      points: 1,
      challengeItemId: 12,
    }], {});

    await queryInterface.bulkInsert('achievementitems', [{
      achievementId: 11221,
      points: 1,
      challengeItemId: 13,
    }], {});

    await queryInterface.bulkInsert('achievementitems', [{
      achievementId: 21221,
      points: 1,
      challengeItemId: 21,
    }], {});

    await queryInterface.bulkInsert('achievementitems', [{
      achievementId: 21221,
      points: 1,
      challengeItemId: 22,
    }], {});

    await queryInterface.bulkInsert('achievementitems', [{
      achievementId: 21221,
      points: 1,
      challengeItemId: 23,
    }], {});

    await queryInterface.bulkInsert('achievementitems', [{
      achievementId: 31221,
      points: 1,
      challengeItemId: 31,
    }], {});

    await queryInterface.bulkInsert('achievementitems', [{
      achievementId: 31221,
      points: 1,
      challengeItemId: 32,
    }], {});

    await queryInterface.bulkInsert('achievementitems', [{
      achievementId: 31221,
      points: 1,
      challengeItemId: 33,
    }], {});

    await queryInterface.bulkInsert('achievementitems', [{
      achievementId: 11231,
      points: 1,
      challengeItemId: 11,
    }], {});

    await queryInterface.bulkInsert('achievementitems', [{
      achievementId: 11231,
      points: 1,
      challengeItemId: 12,
    }], {});

    await queryInterface.bulkInsert('achievementitems', [{
      achievementId: 11231,
      points: 1,
      challengeItemId: 13,
    }], {});

    await queryInterface.bulkInsert('achievementitems', [{
      achievementId: 21231,
      points: 1,
      challengeItemId: 21,
    }], {});

    await queryInterface.bulkInsert('achievementitems', [{
      achievementId: 21231,
      points: 1,
      challengeItemId: 22,
    }], {});

    await queryInterface.bulkInsert('achievementitems', [{
      achievementId: 21231,
      points: 1,
      challengeItemId: 23,
    }], {});

    await queryInterface.bulkInsert('achievementitems', [{
      achievementId: 31231,
      points: 1,
      challengeItemId: 31,
    }], {});

    await queryInterface.bulkInsert('achievementitems', [{
      achievementId: 31231,
      points: 1,
      challengeItemId: 32,
    }], {});

    await queryInterface.bulkInsert('achievementitems', [{
      achievementId: 31231,
      points: 1,
      challengeItemId: 33,
    }], {});

    await queryInterface.bulkInsert('achievementitems', [{
      achievementId: 11241,
      points: 1,
      challengeItemId: 11,
    }], {});

    await queryInterface.bulkInsert('achievementitems', [{
      achievementId: 11241,
      points: 1,
      challengeItemId: 12,
    }], {});

    await queryInterface.bulkInsert('achievementitems', [{
      achievementId: 11241,
      points: 1,
      challengeItemId: 13,
    }], {});

    await queryInterface.bulkInsert('achievementitems', [{
      achievementId: 21241,
      points: 1,
      challengeItemId: 21,
    }], {});

    await queryInterface.bulkInsert('achievementitems', [{
      achievementId: 21241,
      points: 1,
      challengeItemId: 22,
    }], {});

    await queryInterface.bulkInsert('achievementitems', [{
      achievementId: 21241,
      points: 1,
      challengeItemId: 23,
    }], {});

    await queryInterface.bulkInsert('achievementitems', [{
      achievementId: 31241,
      points: 1,
      challengeItemId: 31,
    }], {});

    await queryInterface.bulkInsert('achievementitems', [{
      achievementId: 31241,
      points: 1,
      challengeItemId: 32,
    }], {});

    await queryInterface.bulkInsert('achievementitems', [{
      achievementId: 31241,
      points: 1,
      challengeItemId: 33,
    }], {});

    await queryInterface.bulkInsert('achievementitems', [{
      achievementId: 11251,
      points: 1,
      challengeItemId: 11,
    }], {});

    await queryInterface.bulkInsert('achievementitems', [{
      achievementId: 11251,
      points: 1,
      challengeItemId: 12,
    }], {});

    await queryInterface.bulkInsert('achievementitems', [{
      achievementId: 11251,
      points: 1,
      challengeItemId: 13,
    }], {});

    await queryInterface.bulkInsert('achievementitems', [{
      achievementId: 21251,
      points: 1,
      challengeItemId: 21,
    }], {});

    await queryInterface.bulkInsert('achievementitems', [{
      achievementId: 21251,
      points: 1,
      challengeItemId: 22,
    }], {});

    await queryInterface.bulkInsert('achievementitems', [{
      achievementId: 21251,
      points: 1,
      challengeItemId: 23,
    }], {});

    await queryInterface.bulkInsert('achievementitems', [{
      achievementId: 31251,
      points: 1,
      challengeItemId: 31,
    }], {});

    await queryInterface.bulkInsert('achievementitems', [{
      achievementId: 31251,
      points: 1,
      challengeItemId: 32,
    }], {});

    await queryInterface.bulkInsert('achievementitems', [{
      achievementId: 31251,
      points: 1,
      challengeItemId: 33,
    }], {});

  },

  async down(queryInterface, Sequelize) {
    await queryInterface.bulkDelete('activities', null, {});
  }
};
