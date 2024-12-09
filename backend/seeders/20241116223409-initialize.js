'use strict';

require('dotenv').config();
const bcrypt = require('bcryptjs');

/** @type {import('sequelize-cli').Migration} */
module.exports = {
  async up(queryInterface, Sequelize) {

    await queryInterface.bulkInsert('users', [{
      username: process.env.ADMIN_USER,
      password: bcrypt.hashSync(process.env.ADMIN_PASSWORD),
      nickname: "admin",
      code: "1234",
      role: "admin",
      nationality: "spanish",
      createdAt: "2024-11-16",
      updatedAt: "2024-11-16",
    }], {});

    await queryInterface.bulkInsert('users', [{
      username: "paul",
      password: bcrypt.hashSync("1111"),
      nickname: "paul",
      code: "1111",
      role: "teacher",
      nationality: "english",
      createdAt: "2024-11-16",
      updatedAt: "2024-11-16",
    }], {});

    await queryInterface.bulkInsert('users', [{
      username: "tiburcio",
      password: bcrypt.hashSync("2222"),
      nickname: "tiburcio",
      code: "2222",
      role: "teacher",
      nationality: "spanish",
      createdAt: "2024-11-16",
      updatedAt: "2024-11-16",
    }], {});
  },

  async down(queryInterface, Sequelize) {
    await queryInterface.bulkDelete('users', null, {});
  }
};

// npx sequelize-cli db:seed:all
