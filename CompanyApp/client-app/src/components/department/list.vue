<template>
  <div>
    <app-info-department v-for="department in departments" :department="department" :key="department.Id">
    </app-info-department>
  </div>
</template>

<script>
  import axios from 'axios';
  import InfoDepartment from './info.vue';

  export default {
    data() {
      return {
        departments: null,
      };
    },
    components: {
      'app-info-department': InfoDepartment,
    },
    methods: {
      getDepartments() {
        axios.get('http://localhost:50011/Departments/')
          .then((response) => {
            this.departments = response.data;
            console.log(response.data)
          })
          .catch(
            (error) => console.log(error)
          );
      }
    },
    created() {
      this.getDepartments();
    },
  }
</script>
