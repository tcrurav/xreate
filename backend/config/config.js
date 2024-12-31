require('dotenv').config();

module.exports = {
  development: {
    username: process.env.MYSQL_USER_DEV,
    password: process.env.MYSQL_PASSWORD_DEV,
    database: process.env.MYSQL_DATABASE_DEV,
    host: process.env.DB_HOST_DEV,
    dialect: 'mysql'
  },
  test: {
    username: process.env.MYSQL_USER_TEST,
    password: process.env.MYSQL_PASSWORD_TEST,
    database: process.env.MYSQL_DATABASE_TEST,
    host: process.env.DB_HOST_TEST,
    dialect: 'mysql'
  },
  production: {
    username: process.env.MYSQL_USER_PRO,
    password: process.env.MYSQL_PASSWORD_PRO,
    database: process.env.MYSQL_DATABASE_PRO,
    host: process.env.DB_HOST_PRO,
    dialect: 'mysql'
  }
}