require('dotenv').config();
const bcrypt = require('bcryptjs');

const db = require("../models");
const User = db.user;

const utils = require("../utils");

const request = require('supertest')
const app = require('../server.js')

let ADMIN_USER_ID = 0;
let ADMIN_TOKEN = '';
let A_USER = {};
const A_USER_PASSWORD = '1111';
let A_USER_TOKEN = "";

beforeAll(async () => {
  const data = await User.findOne({ where: { username: process.env.ADMIN_USER } });
  ADMIN_USER_ID = data.id;
  ADMIN_TOKEN = utils.generateToken(data);

  const user = {
    username: 'juan',
    password: bcrypt.hashSync(A_USER_PASSWORD),
    nickname: 'juanito',
    code: '2222',
    role: 'STUDENT',
    nationality: 'spanish'
  };
  A_USER = await User.create(user);
  A_USER_TOKEN = utils.generateToken(A_USER);
});

describe('POST /api/users/signin', () => {
  it('should authenticate', async () => {
    const BASIC_AUTH_CODE = btoa(`${process.env.ADMIN_USER}:${process.env.ADMIN_PASSWORD}`);
    const res = await request(app)
      .post('/api/users/signin')
      .set('Authorization', `Basic ${BASIC_AUTH_CODE}`)

    expect(res.statusCode).toEqual(200)
    expect(res.body).toHaveProperty('user')
    expect(res.body).toHaveProperty('access_token')
  })
})

describe('POST /api/users/signin', () => {
  it('should NOT authenticate with fake password', async () => {
    const FAKE_PASSWORD = "a fake password";
    const BASIC_AUTH_CODE = btoa(`${process.env.ADMIN_USER}:${FAKE_PASSWORD}`);
    const res = await request(app)
      .post('/api/users/signin')
      .set('Authorization', `Basic ${BASIC_AUTH_CODE}`)

    expect(res.statusCode).toEqual(401) //Not Authorized
  })
})

describe('GET /api/users', () => {
  it('should show all users', async () => {
    const res = await request(app)
      .get('/api/users')
      .set('Authorization', `Bearer ${ADMIN_TOKEN}`)

    expect(res.statusCode).toEqual(200)
    expect(res.body[0]).toHaveProperty('username')
  })
})

describe('GET /api/users/:userId', () => {
  it('should show a user by userId', async () => {
    const res = await request(app)
      .get(`/api/users/${ADMIN_USER_ID}`)
      .set('Authorization', `Bearer ${ADMIN_TOKEN}`)

    expect(res.statusCode).toEqual(200)
    expect(res.body).toHaveProperty('username')
  })
})

describe('POST /api/users', () => {
  it('should NOT create a new user with a user Token', async () => {
    const payload = {
      username: 'pepe juan',
      password: '4000',
      nickname: 'juanillo',
      code: '4000',
      role: 'STUDENT',
      nationality: 'spanish'
    };
    const res = await request(app)
      .post('/api/users')
      .set('Authorization', `Bearer ${A_USER_TOKEN}`)
      .send(payload)

    expect(res.statusCode).toEqual(401) // Unathorized
  })
})

describe('POST /api/users', () => {
  it('should create a new user', async () => {
    const payload = {
      username: 'pepe juan',
      password: '4000',
      nickname: 'juanillo',
      code: '4000',
      role: 'STUDENT',
      nationality: 'spanish'
    };
    const res = await request(app)
      .post('/api/users')
      .set('Authorization', `Bearer ${ADMIN_TOKEN}`)
      .send(payload)

    expect(res.statusCode).toEqual(200)
    expect(res.body.user.username).toEqual('pepe juan')
    expect(res.body.user.nickname).toEqual('juanillo')
    expect(res.body.user.code).toEqual('4000')
    expect(res.body.user.role).toEqual('STUDENT')
    expect(res.body.user.nationality).toEqual('spanish')
    expect(res.body).toHaveProperty('access_token')
  })
})

describe('PUT /api/users/:userId', () => {

  const A_USER_NEW_PASSWORD = '3333';
  it('should NOT update a user with a user token', async () => {
    const payload = {
      username: 'john',
      password: A_USER_NEW_PASSWORD,
      nickname: 'johnny',
      code: '4444',
      role: 'TEACHER',
      nationality: 'english'
    };
    const res = await request(app)
      .put(`/api/users/${A_USER.id}`)
      .set('Authorization', `Bearer ${A_USER_TOKEN}`)
      .send(payload)

    expect(res.statusCode).toEqual(401)
  })
})

describe('PUT /api/users/:userId', () => {

  const A_USER_NEW_PASSWORD = '3333';
  it('should update a user', async () => {
    const payload = {
      username: 'john',
      password: A_USER_NEW_PASSWORD,
      nickname: 'johnny',
      code: '4444',
      role: 'TEACHER',
      nationality: 'english'
    };
    const res = await request(app)
      .put(`/api/users/${A_USER.id}`)
      .set('Authorization', `Bearer ${ADMIN_TOKEN}`)
      .send(payload)

    expect(res.statusCode).toEqual(200)
    expect(res.body.message).toEqual('User was updated successfully.')
  })


  it('should show the previously updated user with updated data', async () => {
    const res = await request(app)
      .get(`/api/users/${A_USER.id}`)
      .set('Authorization', `Bearer ${ADMIN_TOKEN}`)

    expect(res.statusCode).toEqual(200)
    expect(res.body.username).toEqual('john')
    expect(bcrypt.compareSync(A_USER_NEW_PASSWORD, res.body.password)).toEqual(true)
    expect(res.body.nickname).toEqual('johnny')
    expect(res.body.code).toEqual('4444')
    expect(res.body.role).toEqual('TEACHER')
    expect(res.body.nationality).toEqual('english')
  })
})

describe('DELETE /api/users/:userId', () => {
  it('should NOT delete a user by userId with a user token', async () => {
    const res = await request(app)
      .delete(`/api/users/${A_USER.id}`)
      .set('Authorization', `Bearer ${A_USER_TOKEN}`)
    expect(res.statusCode).toEqual(401)
  })
})

describe('DELETE /api/users/:userId', () => {
  it('should delete a user by userId', async () => {
    const res = await request(app)
      .delete(`/api/users/${A_USER.id}`)
      .set('Authorization', `Bearer ${ADMIN_TOKEN}`)
    expect(res.statusCode).toEqual(200)
    expect(res.body.message).toEqual('User was deleted successfully!')
  })
})


