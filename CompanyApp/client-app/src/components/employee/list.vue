<template>
  <div>
    <app-info-employee v-for="employee in employees" :employee="employee" :key="employee.Id">
    </app-info-employee>
  </div>
</template>
<script>
  import axios from 'axios';
  import InfoEmployee from './info.vue';

  export default {
    data() {
      return {
        employees: null,
      };
    },
    components: {
      'app-info-employee': InfoEmployee,
    },
    methods: {
      getEmployees() {
        axios.get('http://localhost:50011/Employees/GetAll')
          .then((response) => {
            this.employees = response.data;
          })
          .catch(
          (error) => console.log(error)
          );
      },
    },
    created() {
      this.getEmployees();
    },
  }
</script>
