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
  },

  async down(queryInterface, Sequelize) {
    await queryInterface.bulkDelete('users', null, {});
  }
};

// npx sequelize-cli db:seed:all
