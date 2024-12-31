'use strict';

require('dotenv').config();
const bcrypt = require('bcryptjs');

/** @type {import('sequelize-cli').Migration} */
module.exports = {
  async up(queryInterface, Sequelize) {

    await queryInterface.bulkInsert('users', [{
      id: 1,
      username: process.env.ADMIN_USER,
      password: bcrypt.hashSync(process.env.ADMIN_PASSWORD),
      nickname: "admin",
      code: "9999",
      role: "admin",
      nationality: "spanish",
      createdAt: new Date(),
      updatedAt: new Date()
    }], {});

    await queryInterface.bulkInsert('users', [{
      id: 2,
      username: "guest",
      password: bcrypt.hashSync("9999"),
      nickname: "guest",
      code: "9999",
      role: "guest",
      nationality: "spanish",
      createdAt: new Date(),
      updatedAt: new Date()
    }], {});
    
  },

  async down(queryInterface, Sequelize) {
    await queryInterface.bulkDelete('users', null, {});
  }
};

// npx sequelize-cli db:seed:all
