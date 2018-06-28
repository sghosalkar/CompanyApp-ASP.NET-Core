import Vue from 'vue';
import Vuex from 'vuex';

import data from './utils/data';

Vue.use(Vuex);

export const store = new Vuex.Store({
  state: {
    departments: [],
    employees: [],
    projects: [],
    roles: [],
    data: data,
  }
});
