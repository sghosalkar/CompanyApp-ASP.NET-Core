import axios from 'axios';

const Departments = 'Departments';
const Employees = 'Employees';
const Projects = 'Projects';
const Roles = 'Roles';

const baseUrl = 'http://localhost:50011/';

async function getAll(entity) {
  const url = baseUrl + entity + '/GetAll';
  return await getData(url, null);
}

async function getById(entity, params) {
  const url = baseUrl + entity + '/Details';
  return await getData(url, params);
}

function add(entity, data) {
  const url = baseUrl + entity + '/Create';
  return postData(url, data);
}

function remove(entity, params) {
  const url = baseUrl + entity + '/Delete';
  return deleteData(url, params);
}

async function getData(url, params) {
  try {
    const response = await axios.get(url, params);
    return response.data;
  }
  catch (error) {
    console.error(error);
  }
}

function postData(url, data) {
  var res = null;
  console.log(data);
  axios.post(url, data)
    .then((response) => {
      console.log(response);
      res = response.data;
    })
    .catch(
    (error) => console.log(error)
  );
  return res;
}

function deleteData(url, params) {
  var res = null;
  axios.delete(url, params)
    .then((response) => {
      console.log(response);
      res = response.data;
    })
    .catch(
      (error) => console.log(error)
    );
  return res;
}

export default {
  Departments,
  Employees,
  Projects,
  Roles,
  getAll,
  getById,
  add,
  remove
};
