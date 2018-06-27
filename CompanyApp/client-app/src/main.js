import Vue from 'vue'
import VueRouter from 'vue-router';
import App from './App.vue'

import Home from './components/home.vue';
import Employees from './components/employee/employees.vue';
import Departments from './components/department/departments.vue';
import Projects from './components/project/projects.vue';
import Roles from './components/role/roles.vue';


Vue.use(VueRouter);

const routes = [
  { path: '/', component: Home },
  { path: '/employees', component: Employees },
  { path: '/departments', component: Departments },
  { path: '/projects', component: Projects },
  { path: '/roles', component: Roles },
];

const router = new VueRouter({
  mode: 'history',
  routes: routes,
});


new Vue({
  el: '#app',
  router: router,
  //template: '<App />',
  //components: { App },
  render: h => h(App)
})
