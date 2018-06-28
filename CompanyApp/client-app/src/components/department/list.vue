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
        entity: null,
        departments: null,
      };
    },
    components: {
      'app-info-department': InfoDepartment,
    },
    methods: {
      async getDepartments() {
        const getAll = this.$store.state.data.getAll;
        this.departments = await getAll(this.entity);
        console.log(this.departments);
      }
    },
    mounted() {
      this.entity = this.$store.state.data.Departments;
      this.getDepartments();
    },
  }
</script>
